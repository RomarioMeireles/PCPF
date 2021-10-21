using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class PedidoService : BaseService, IPedidoService
    {
        private readonly IPedidoRepository _IPedidoRepository;

        public PedidoService(IPedidoRepository IPedidoRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IPedidoRepository = IPedidoRepository;
        }
        public async Task Adicionar(Pedido entity)
        {
            if (!ExecutarValidacao(new PedidoValidation(), entity)) return;

            await _IPedidoRepository.Adicionar(entity);
        }

        public async Task Atualizar(Pedido entity)
        {
            if (!ExecutarValidacao(new PedidoValidation(), entity)) return;

            await _IPedidoRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IPedidoRepository.Remover(id);
        }
    }
}
