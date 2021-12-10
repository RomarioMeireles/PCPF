using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class Fornecedor:Entity
    {
        public Fornecedor(){}

        public Fornecedor(string denominacaoFiscal, string telefone, string email, string endereco, int utilizadorId)
        {
            DenominacaoFiscal = denominacaoFiscal;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            UtilizadorId = utilizadorId;
        }

        public string DenominacaoFiscal { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public int UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }
        public ICollection<ProdutoFornecedor> produtos { get; set; }
    }
}
