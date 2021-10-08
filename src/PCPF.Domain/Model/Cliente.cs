using System.Collections.Generic;

namespace PCPF.Domain.Model
{
    public class Cliente:Entity
    {
        public Cliente(){}

        public Cliente(string nome, string telefone, string documentoIdentificacao, string email)
        {
            Nome = nome;
            Telefone = telefone;
            DocumentoIdentificacao = documentoIdentificacao;
            Email = email;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string DocumentoIdentificacao { get; set; }
        public string Email { get; set; }

        public ICollection<Pedido> ItensPedido { get; set; }
    }
}
