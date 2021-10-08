namespace PCPF.Domain.Model
{
    public class PedidoItem:Entity
    {
        public PedidoItem(){}

        public PedidoItem(int pedidoId, int produtoId, int quantidade, decimal valor, decimal desconto)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Valor = valor;
            Desconto = desconto;
        }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
    }
}
