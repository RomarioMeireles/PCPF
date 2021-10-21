using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class PedidoItemService : BaseService, IPedidoItemService
    {
        private readonly IPedidoItemRepository _IPedidoItemRepository;

        public PedidoItemService(IPedidoItemRepository IPedidoItemRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IPedidoItemRepository = IPedidoItemRepository;
        }
        public async Task Adicionar(PedidoItem entity)
        {
            if (!ExecutarValidacao(new PedidoItemValidation(), entity)) return;

            await _IPedidoItemRepository.Adicionar(entity);
        }

        public async Task Atualizar(PedidoItem entity)
        {
            if (!ExecutarValidacao(new PedidoItemValidation(), entity)) return;

            await _IPedidoItemRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IPedidoItemRepository.Remover(id);
        }
    }
}
