using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs_sensolux.Domain.Entity
{
    public class Email
    {
        public long Id { get; set; }

        public string Endereco { get; set; } = null!;

        public bool Ativo { get; set; }

        public Email() { }

        public Email(string endereco, bool ativo)
        {
            Endereco = endereco;
            Ativo = ativo;
        }

        public override string ToString()
        {
            return Endereco;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void AlterarEndereco(string novoEndereco)
        {
            if (string.IsNullOrWhiteSpace(novoEndereco))
                throw new ArgumentException("Endereço de e-mail inválido.");
            Endereco = novoEndereco;
        }
    }
}
