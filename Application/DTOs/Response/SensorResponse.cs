namespace gs_sensolux.Application.DTOs.Response
{
    public class SensorResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Tipo { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
