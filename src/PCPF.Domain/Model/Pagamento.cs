namespace PCPF.Domain.Model
{
    public class Pagamento:Entity
    {
        public Pagamento(){}
        public Pagamento(int pedidoId, decimal valotTotal, string observacao)
        {
            PedidoId = pedidoId;
            ValotTotal = valotTotal;
            Observacao = observacao;
        }

        public int PedidoId { get; set; }
        public decimal ValotTotal { get; set; }
        public string Observacao { get; set; }
        public string Comprovativo { get; set; }
        public Pedido Pedido { get; set; }
    }
}
