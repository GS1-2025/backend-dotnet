using System.ComponentModel.DataAnnotations.Schema;

namespace gs_sensolux.Domain.Entity
{

    public class Usuario
    {
        
        public int Id { get; private set; }

  
        public string Nome { get; private set; } = null!;


        public string Senha { get; private set; } = null!;

 
        public string Role { get; private set; } = null!;


        public long EmailId { get; private set; }

        public Email Email { get; private set; } = null!;

        public ICollection<ItensPedido> ItensPedido { get; private set; }
        public ICollection<Endereco> Enderecos { get; private set; }

        protected Usuario()
        {
            ItensPedido = new List<ItensPedido>();
            Enderecos = new List<Endereco>();
        }

        public Usuario(string nome, string senha, string role, Email email)
        {
            SetNome(nome);
            SetSenha(senha);
            SetRole(role);
            Email = email ?? throw new ArgumentNullException(nameof(email));
            EmailId = (long)email.Id;

            ItensPedido = new List<ItensPedido>();
            Enderecos = new List<Endereco>();
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do usuário é obrigatório.");
            Nome = nome;
        }

        public void SetSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha é obrigatória.");
            Senha = senha;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role é obrigatória.");
            Role = role;
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
                throw new ArgumentNullException(nameof(endereco));
            Enderecos.Add(endereco);
        }
    }
}
