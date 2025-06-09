namespace gs_sensolux.Application.DTOs.Request
{
    public class CreateSensorRequest
    {
        public string Tipo { get; set; } = null!;      
        public string? Modelo { get; set; }            
        public string? Descricao { get; set; }         
        public string? Status { get; set; }
    }
}
