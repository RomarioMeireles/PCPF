using Microsoft.AspNetCore.Http;
using System;

namespace PCPF.Web.MVC.Models
{
    public class ProdutoViewModel
    {
        public string Descricao { get; set; }
        public IFormFile Imagem { get; set; }
        public string CodigoBarras { get; set; }
        public string Observacao { get; set; }
        public int QuantidadeMinima { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public int ProdutoId { get; private set; }
        public string NumeroLote { get; set; }
        public Nullable<DateTime> DataValidade { get; set; } = Convert.ToDateTime("1753-01-01");
    }
}
