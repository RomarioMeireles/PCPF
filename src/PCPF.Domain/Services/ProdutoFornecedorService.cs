
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
    public class ProdutoFornecedorService : BaseService, IProdutoFornecedorService
    {
        private readonly IProdutoFornecedorRepository _IProdutoFornecedorRepository;

        public ProdutoFornecedorService(IProdutoFornecedorRepository IProdutoFornecedorRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IProdutoFornecedorRepository = IProdutoFornecedorRepository;
        }
        public async Task Adicionar(ProdutoFornecedor entity)
        {
            if (!ExecutarValidacao(new ProdutoFornecedorValidation(), entity)) return;
            if (_IProdutoFornecedorRepository.Buscar(c => c.FornecedorId == entity.FornecedorId && c.ProdutoId == entity.ProdutoId).Result.Count() > 0)
            {
                Notificar("Já existe um registo deste produto ligado a este fornecedor na base de dados!");
                return;
            }
            await _IProdutoFornecedorRepository.Adicionar(entity);
        }

        public async Task Atualizar(ProdutoFornecedor entity)
        {
            if (!ExecutarValidacao(new ProdutoFornecedorValidation(), entity)) return;

            await _IProdutoFornecedorRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IProdutoFornecedorRepository.Remover(id);
        }
    }
}