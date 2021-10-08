using System;
using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class Utilizador
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime DataRegisto { get; set; }
        public ICollection<Fornecedor> Fornercedores { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<ProdutoFornecedor> produtoFornecedors { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
