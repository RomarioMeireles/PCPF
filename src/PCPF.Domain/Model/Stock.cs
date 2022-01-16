using System;

namespace PCPF.Domain.Model
{
    public class Stock:Entity
    {
        public Stock(){}
        public Stock(decimal quantidade, int produtoId, string numeroLote, DateTime? dataValidade, int utilizadorId)
        {
            Quantidade = quantidade;
            ProdutoId = produtoId;
            NumeroLote = numeroLote;
            DataValidade = dataValidade;
            UtilizadorId = utilizadorId;
        }

        public decimal Quantidade { get; set; }
        public int ProdutoId { get; private set; }
        public string NumeroLote { get; set; }
        public Nullable<DateTime> DataValidade { get; set; } = Convert.ToDateTime("1753-01-01");
        public int UtilizadorId { get; private set; }
        public Utilizador Utilizador { get; set; }
        public Produto Produto { get; set; }

        public void SetProdutoId(int id)
        {
            ProdutoId = id;
        }
    }
}
