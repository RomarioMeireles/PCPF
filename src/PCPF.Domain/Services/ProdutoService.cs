using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _IProdutoRepository;

        public ProdutoService(IProdutoRepository IProdutoRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IProdutoRepository = IProdutoRepository;
        }
        public async Task Adicionar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity)) return;
            if (_IProdutoRepository.Buscar(c => c.Descricao == entity.Descricao && c.CodigoBarras == entity.CodigoBarras).Result.Count() > 0)
            {
                Notificar("O produto indicado já se encontra na base de dados!");
                return;
            }
            await _IProdutoRepository.Adicionar(entity);
        }

        public void Adicionar(Produto entity, Stock stock)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity)) return;
            if (_IProdutoRepository.Buscar(c => c.Descricao == entity.Descricao && c.CodigoBarras == entity.CodigoBarras).Result.Count() > 0)
            {
                Notificar("O produto indicado já se encontra na base de dados!");
                return;
            }
            if (!ExecutarValidacao(new StockValidation(), stock)) return;

            _IProdutoRepository.Adicionar(entity, stock);
        }

        public async Task Atualizar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity)) return;

            await _IProdutoRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            var produto = await _IProdutoRepository.ObterPorId(id);
            produto.Status = false;
            await _IProdutoRepository.Atualizar(produto);
        }
    }
}