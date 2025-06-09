namespace gs_sensolux.Application.DTOs.Request
{
    public class CreateItensPedidoRequest
    {
        public int PedidoId { get; set; }       
        public int UsuarioId { get; set; }
        public int Quantidade { get; set; }
        public List<int> ProdutoIds { get; set; }


    }
   
}
