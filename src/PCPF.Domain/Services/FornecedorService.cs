using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _IFornecedorRepository;

        public FornecedorService(IFornecedorRepository IFornecedorRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IFornecedorRepository = IFornecedorRepository;
        }
        public async Task Adicionar(Fornecedor entity)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), entity)) return;
            if (_IFornecedorRepository.Buscar(c => c.Telefone == entity.Telefone && c.Email == entity.Email).Result.Count() > 0)
            {
                Notificar("O Fornecedor indicado já se encontra na base de dados!");
                return;
            }
            await _IFornecedorRepository.Adicionar(entity);
        }

        public async Task Atualizar(Fornecedor entity)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), entity)) return;

            await _IFornecedorRepository.Atualizar(entity);
        }

        public async Task Remover(int id)
        {
            await _IFornecedorRepository.Remover(id);
        }
    }
}
