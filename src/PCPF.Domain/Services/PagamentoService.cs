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

        public PagamentoService(IPagamentoRepository IPagamentoRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IPagamentoRepository = IPagamentoRepository;
        }
        public async Task Adicionar(Pagamento entity)
        {
            if (!ExecutarValidacao(new PagamentoValidation(), entity)) return;
            
            await _IPagamentoRepository.Adicionar(entity);
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
