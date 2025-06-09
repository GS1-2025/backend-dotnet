using gs_sensolux.Application.DTOs.Request;

namespace gs_sensolux.Application.DTOs.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string Status { get; set; }
        public double Preco { get; set; }
        public ItensPedidoResponse Itens { get; set; }
    }
}
