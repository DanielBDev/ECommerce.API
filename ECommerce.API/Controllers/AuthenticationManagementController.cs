using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ECommerce.API.Configuration;
using ECommerce.API.Models.DTOs.Request;
using ECommerce.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ECommerce.API.Models;
using Enum;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthenticationManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<AuthenticationManagementController> _logger;

        public AuthenticationManagementController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtConfig> jwtConfig,
            ILogger<AuthenticationManagementController> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] UserRegistrationRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);

                    if (existingUser != null)
                    {
                        return await Task.FromResult(new ResponseModel(ResponseCode.Error, "El Email ya esta en uso.", null));
                    }
                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        return await Task.FromResult(new ResponseModel(ResponseCode.Error, "El rol no existe", null));
                    }
                    var user = new IdentityUser()
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var tempUser = await _userManager.FindByEmailAsync(model.Email);
                        await _userManager.AddToRoleAsync(tempUser, model.Role);

                        return await Task.FromResult(new ResponseModel(ResponseCode.OK ,"Usuario registrado con éxito.", null));
                    }

                    return await Task.FromResult(new ResponseModel(ResponseCode.Error,"",result.Errors.Select(x => x.Description).ToArray()));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Carga invalida.", null));

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }            
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] UserLoginRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
                        var user = new UserDTO(appUser.UserName, appUser.Email, role);
                        user.Token = GenerateToken(appUser, role);

                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                    }
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Email o Contraseña incorrectos.", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Carga invalida.", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                List<UserDTO> allUserDTO = new List<UserDTO>();
                var users = _userManager.Users.ToList();

                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    allUserDTO.Add( new UserDTO(user.UserName, user.Email, role));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", allUserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x => x.Name).ToList();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize()]
        [HttpPost("AddRole")]
        public async Task<object> AddRole([FromBody] UserRoleDto model)
        {
            try
            {
                if (model==null || model.Role == "")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Faltan parametros.", null));
                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "El rol ya existe.", null));
                }

                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Rol añadido con éxito.", null));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Algo salió mal, por favor intente nuevamente.", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        #region Private Methods
        private string GenerateToken(IdentityUser user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        #endregion

    }
}
