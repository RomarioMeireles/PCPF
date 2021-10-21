using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class StockService : BaseService, IStockService
    {
        private readonly IStockRepository _IStockRepository;

        public StockService(IStockRepository IStockRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IStockRepository = IStockRepository;
        }
        public async Task Adicionar(Stock entity)
        {
            if (!ExecutarValidacao(new StockValidation(), entity)) return;
            
            await _IStockRepository.Adicionar(entity);
        }

        public async Task Atualizar(Stock entity)
        {
            if (!ExecutarValidacao(new StockValidation(), entity)) return;

            await _IStockRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IStockRepository.Remover(id);
        }
    }
}
