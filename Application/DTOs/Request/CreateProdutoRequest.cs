namespace gs_sensolux.Application.DTOs.Request
{
    public class CreateProdutoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
    }
}
