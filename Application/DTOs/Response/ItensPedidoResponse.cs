namespace gs_sensolux.Application.DTOs.Response
{
    public class ItensPedidoResponse
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public int Quantidade { get; set; }
        public List<int> ProdutoIds { get; set; }
    }
}
