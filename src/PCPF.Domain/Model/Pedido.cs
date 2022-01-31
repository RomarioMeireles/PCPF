using PCPF.Domain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCPF.Domain.Model
{
    public class Pedido:Entity
    {
        public Pedido(){}

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime IniciadoEm { get; set; }
        public Nullable<DateTime> FinalizadoEm { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public string Observacao { get; set; }
        public long Referencia { get; set; }
        public List<PedidoItem> ItensPedido { get; set; }
        public ICollection<Pagamento> Pagamentos { get; set; }
        [NotMapped]
        public decimal Total { get; set; }
    }
}
