using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class ProdutoFornecedor:Entity
    {
        public ProdutoFornecedor(){}

        public ProdutoFornecedor(int produtoId, int fornecedorId, string utilizadorId)
        {
            ProdutoId = produtoId;
            FornecedorId = fornecedorId;
            UtilizadorId = utilizadorId;
        }

        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public string UtilizadorId { get; set; }
        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Utilizador Utilizador { get; set; }
        //public ICollection<ProdutoFornecedor> ItensProdutoFornecedor { get; set; }
    }
}
