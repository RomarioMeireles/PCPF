using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _IClienteRepository;

        public ClienteService(IClienteRepository IClienteRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IClienteRepository = IClienteRepository;
        }
        public async Task Adicionar(Cliente entity)
        {
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;
            if (_IClienteRepository.Buscar(c => c.Telefone == entity.Telefone && c.Email == entity.Email).Result.Count() > 0)
            {
                Notificar("O cliente indicado já se encontra na base de dados!");
                return;
            }
            await _IClienteRepository.Adicionar(entity);
        }

        public async Task Atualizar(Cliente entity)
        {
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;

            await _IClienteRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IClienteRepository.Remover(id);
        }
    }
}
