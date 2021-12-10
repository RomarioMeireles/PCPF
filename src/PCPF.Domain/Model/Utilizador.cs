using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class Utilizador: Entity
    {
        public Utilizador(){}
        public Utilizador(string userName, string password)
        {
            UserName = userName;
            Password = password;            
        }

        public string UserName { get; set; }
        public string Password { get; set; }
       
        public ICollection<Fornecedor> Fornercedores { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<ProdutoFornecedor> produtoFornecedors { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
