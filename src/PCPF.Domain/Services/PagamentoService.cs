using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Linq;
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
        public Task Adicionar(Pagamento entity, int idPedido)
        {
            var total = _IPedidoRepository.ObterPedidoItemPorIdPedido(idPedido);
            entity.ValotTotal = total.Result.Sum(a => a.Valor * a.Quantidade);

            if (!ExecutarValidacao(new PagamentoValidation(), entity)) return Task.FromResult(false);

            var pedido = _IPedidoRepository.ObterPorId(idPedido).Result;
            pedido.StatusPedido = Model.ValueObjects.StatusPedido.Processamento;
            entity.PedidoId = idPedido;
            entity.Observacao = pedido.Observacao;
           
            return _IPagamentoRepository.Adicionar(entity, pedido);
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
