namespace gs_sensolux.Application.DTOs.Response
{
    public class EnderecoResponse
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int UsuarioId { get; set; }
    }
}
