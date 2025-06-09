namespace gs_sensolux.Domain.Entity
{
    public class Sensor
    {
        public int Id { get; private set; }
        public int ProdutoId { get; private set; }
        public string Tipo { get; private set; }
        public string Descricao { get; private set; }
        public string Modelo { get; private set; }
        public string Status { get; private set; }

        public Produto Produto { get; private set; }

        protected Sensor() { }

        public Sensor(string tipo, string modelo, string descricao)
        {
            SetTipo(tipo);
            SetModelo(modelo);
            SetDescricao(descricao);
            Ativar();
        }


        public void SetTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Tipo do sensor é obrigatório.");
            Tipo = tipo;
        }

        public void SetModelo(string modelo)
        {
            Modelo = modelo ?? "Genérico";
        }

        public void SetDescricao(string descricao)
        {
            Descricao = descricao ?? string.Empty;
        }

        public void Ativar()
        {
            Status = "Ativo";
        }

        public void Desativar()
        {
            Status = "Inativo";
        }
    }

}
