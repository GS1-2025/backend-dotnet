namespace gs_sensolux.Domain.Entity
{
    public class Endereco
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public string Cep { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Rua { get; private set; }

        public Usuario Usuario { get; private set; }

        protected Endereco() { }

        public Endereco(string cep, string estado, string cidade, string bairro, string rua, int UsuarioId)
        {
            SetCep(cep);
            SetEstado(estado);
            SetCidade(cidade);
            SetBairro(bairro);
            SetRua(rua);
            SetUsuarioId(UsuarioId);
        }

        public void SetCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
                throw new ArgumentException("CEP inválido.");
            Cep = cep;
        }

        public void SetEstado(string estado)
        {
            Estado = estado ?? "N/A";
        }

        public void SetCidade(string cidade)
        {
            Cidade = cidade ?? "N/A";
        }

        public void SetBairro(string bairro)
        {
            Bairro = bairro ?? "N/A";
        }

        public void SetRua(string rua)
        {
            Rua = rua ?? "N/A";
        }
        public void SetUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");
            UsuarioId = usuarioId;
        }

    }


}
