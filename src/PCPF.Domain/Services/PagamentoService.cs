using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class PagamentoService : BaseService, IPagamentoService
    {
        private readonly IPagamentoRepository _IPagamentoRepository;
        private readonly IPedidoRepository _IPedidoRepository;
        public PagamentoService(IPagamentoRepository IPagamentoRepository, IPedidoRepository IPedidoRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IPagamentoRepository = IPagamentoRepository;
            _IPedidoRepository = IPedidoRepository;
        }
        public async Task Adicionar(Pagamento entity, int idPedido)
        {
            if (!ExecutarValidacao(new PagamentoValidation(), entity)) return;

            var pedido = await _IPedidoRepository.ObterPorId(idPedido);
            pedido.StatusPedido = Model.ValueObjects.StatusPedido.Processamento;
            entity.PedidoId = idPedido;
            await _IPagamentoRepository.Adicionar(entity, pedido);
        }

        public async Task Atualizar(Pagamento entity)
        {
            if (!ExecutarValidacao(new PagamentoValidation(), entity)) return;

            await _IPagamentoRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IPagamentoRepository.Remover(id);
        }
    }
}
