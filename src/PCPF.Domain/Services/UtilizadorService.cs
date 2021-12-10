using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class UtilizadorService : BaseService, IUtilizadorService
    {
        private readonly IUtilizadorRepository _IUtilizadorRepository;

        public UtilizadorService(IUtilizadorRepository IUtilizadorRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IUtilizadorRepository = IUtilizadorRepository;
        }
        public async Task Adicionar(Utilizador entity)
        {
            if (!ExecutarValidacao(new UserValidation(), entity)) return;

            await _IUtilizadorRepository.Adicionar(entity);
        }

        public async Task Atualizar(Utilizador entity)
        {
            if (!ExecutarValidacao(new UserValidation(), entity)) return;

            await _IUtilizadorRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IUtilizadorRepository.Remover(id);
        }
    }
}