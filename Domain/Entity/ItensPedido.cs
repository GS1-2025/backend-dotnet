namespace gs_sensolux.Domain.Entity
{
    public class ItensPedido
    {
        public int Id { get; private set; }
        public int PedidoId { get; private set; }
        public int UsuarioId { get; private set; }
        public int Quantidade { get; private set; }

        public Pedido Pedido { get; private set; }
        public Usuario Usuario { get; private set; }
        public ICollection<Produto> Produtos { get; private set; }

        protected ItensPedido() { }

        public ItensPedido(int usuarioId, int quantidade)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que 0.");

            UsuarioId = usuarioId;
            Quantidade = quantidade;
            Produtos = new List<Produto>();
        }

        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException("Produto inválido.");
            Produtos.Add(produto);
        }

        public void RemoverProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException("Produto inválido.");
            Produtos.Remove(produto);
        }

        public void LimparProdutos()
        {
            Produtos.Clear();
        }

        public void AtualizarQuantidade(int novaQuantidade)
        {
            if (novaQuantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que 0.");
            Quantidade = novaQuantidade;
        }

        public void AtualizarUsuarioId(int novoUsuarioId)
        {
            if (novoUsuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");
            UsuarioId = novoUsuarioId;
        }

        public double CalcularSubtotal()
        {
            return Produtos.Sum(p => p.PrecoUnitario) * Quantidade;
        }
    }
}
