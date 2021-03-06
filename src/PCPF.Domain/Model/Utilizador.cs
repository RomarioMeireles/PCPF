using PCPF.Domain.Model.ValueObjects;
using PCPF.Infra.CrossCuting.Seguranca;
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

        public Utilizador(string nome, string userName, string password, Perfil perfil)
        {
            Nome = nome;
            UserName = userName;
            Password = password;
            Perfil = perfil;
        }

        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Perfil Perfil { get; set; }

        public ICollection<Fornecedor> Fornercedores { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<ProdutoFornecedor> produtoFornecedors { get; set; }
        public ICollection<Stock> Stocks { get; set; }

        public string ToHashPassword()
        {
            var passHash = Criptografia.CriptografarSenha(Password);
            Password = passHash;
            return passHash;
        }
    }
}
