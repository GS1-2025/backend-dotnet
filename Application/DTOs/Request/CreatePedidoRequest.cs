namespace gs_sensolux.Application.DTOs.Request
{
    public class CreatePedidoRequest
    {
        public DateTime DataPedido { get; set; }
        public CreateItensPedidoRequest Itens { get; set; } = new();
    }
}
