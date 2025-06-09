namespace gs_sensolux.Domain.Entity
{
    public class Pedido
    {
        public int Id { get; private set; }
        public DateTime DataPedido { get; private set; }
        public double Preco { get; private set; }
        public string Status { get; private set; }

        public ItensPedido Itens { get; private set; }

        protected Pedido()
        {
        }

        public Pedido(DateTime dataPedido, string status = "Em Aberto")
        {
            if (dataPedido == default)
                throw new ArgumentException("Data do pedido é inválida.");

            DataPedido = dataPedido;
            Status = status;
            Preco = 0;
        }

        public void AdicionarItem(ItensPedido item)
        {
            if (item == null)
                throw new ArgumentNullException("Item do pedido inválido.");

            Itens = item;
            RecalcularPreco();
        }

        public void RecalcularPreco()
        {
            if (Itens == null)
            {
                Preco = 0;
            }
            else
            {
                Preco = Itens.CalcularSubtotal();
            }
        }

        public void Finalizar()
        {
            if (Itens == null)
                throw new InvalidOperationException("Pedido sem item não pode ser finalizado.");

            Status = "Finalizado";
            RecalcularPreco();
        }

        public void Cancelar()
        {
            Status = "Cancelado";
        }

        public void Atualizar(DateTime novaData, string novoStatus)
        {
            if (novaData == default)
                throw new ArgumentException("Data inválida.");

            if (string.IsNullOrWhiteSpace(novoStatus))
                throw new ArgumentException("Status inválido.");

            DataPedido = novaData;
            Status = novoStatus;
        }
    }
}
