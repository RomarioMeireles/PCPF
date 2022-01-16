namespace PCPF.Web.MVC.Models
{
    public class CarrinhoViewModel
    {
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get; set; }
    }
}
