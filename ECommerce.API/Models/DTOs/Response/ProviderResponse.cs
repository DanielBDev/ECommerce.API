namespace ECommerce.API.Models.DTOs.Response
{
    public class ProviderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Cuil { get; set; }
        public bool State { get; set; }
    }
}
