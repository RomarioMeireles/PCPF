using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class Produto:Entity
    {
        public Produto(){}

        public Produto(string descricao, string imagem, string codigoBarras, string observacao, int quantidadeMinima, decimal valor, string utilizadorId)
        {
            Descricao = descricao;
            Imagem = imagem;
            CodigoBarras = codigoBarras;
            Observacao = observacao;
            QuantidadeMinima = quantidadeMinima;
            Valor = valor;
            UtilizadorId = utilizadorId;
        }

        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string CodigoBarras { get; set; }
        public string Observacao { get; set; }
        public int QuantidadeMinima { get; set; }
        public decimal Valor { get; set; }
        public string UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }
        public ICollection<PedidoItem> ItensPedido { get; set; }
        public ICollection<ProdutoFornecedor> produtos { get; set; }
        //public ICollection<Stocks> Stocks { get; set; }
    }
}
