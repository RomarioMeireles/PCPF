using System.ComponentModel.DataAnnotations.Schema;

namespace PCPF.Domain.Model
{
    public class PedidoRascunho
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string Imagem { get; set; }
    }
}
