using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ECommerce.API.Configuration;
using ECommerce.API.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using ECommerce.API.Services.Implementation;

using ECommerce.API.Services.Interface;

namespace ECommerce.API
{
    public class Startup
    {
        private readonly string _loginOrigin = "_localorigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            //SQL Database Conection  
            services.AddDbContext<AplicationDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => { })
                .AddEntityFrameworkStores<AplicationDbContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                var issuer = Configuration["JwtConfig:Issuer"];
                var audience = Configuration["JwtConfig:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy(_loginOrigin, builder => builder.AllowAnyOrigin()
                                                                   .AllowAnyHeader()
                                                                   .AllowAnyMethod());
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.API", Version = "v1" });
            });            

            //Project Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IProviderService, ProviderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(_loginOrigin);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
