using System;
using System.Collections.Generic;

namespace gs_sensolux.Domain.Entity
{
    public class Produto
    {
        public int Id { get; private set; }
        public int? ItemPedidoId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double PrecoUnitario { get; private set; }

        public ICollection<Sensor> Sensores { get; private set; }

        protected Produto()
        {
            Sensores = new List<Sensor>();
        }

        public Produto(string nome, string descricao, double precoUnitario)
        {
            SetNome(nome);
            SetDescricao(descricao);
            SetPreco(precoUnitario);
            Sensores = new List<Sensor>();
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto é obrigatório.");
            Nome = nome;
        }

        public void SetDescricao(string descricao)
        {
            Descricao = descricao ?? string.Empty;
        }

        public void SetPreco(double preco)
        {
            if (preco <= 0)
                throw new ArgumentException("Preço unitário deve ser maior que zero.");
            PrecoUnitario = preco;
        }

        public void AdicionarSensor(Sensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("Sensor inválido.");
            Sensores.Add(sensor);
        }

        public void RemoverSensor(Sensor sensor)
        {
            if (sensor == null || !Sensores.Contains(sensor))
                throw new ArgumentException("Sensor não encontrado.");
            Sensores.Remove(sensor);
        }

        public void Atualizar(string novoNome, string novaDescricao, double novoPreco)
        {
            SetNome(novoNome);
            SetDescricao(novaDescricao);
            SetPreco(novoPreco);
        }
    }
}
