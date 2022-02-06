using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Model.ValueObjects;
using PCPF.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Domain.Services
{
   public class PedidoService : BaseService, IPedidoService
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly ISMSGatewayFacade _ISMSGatewayFacade;
        private readonly IClienteRepository _IClienteRepository;
        public PedidoService(IPedidoRepository IPedidoRepository, ISMSGatewayFacade ISMSGatewayFacade, IClienteRepository IClienteRepository, INotificador iNotificador) : base(iNotificador)
        {
            _IPedidoRepository = IPedidoRepository;
            _ISMSGatewayFacade = ISMSGatewayFacade;
            _IClienteRepository = IClienteRepository;
        }

        public async Task ActualizarPedidoRascunho(IEnumerable<PedidoRascunho> pedido)
        {
            await _IPedidoRepository.ActualizarPedidoRascunho(pedido);
        }

        public async Task Adicionar(Pedido entity)
        {
            if (!ExecutarValidacao(new PedidoValidation(), entity)) return;

            await _IPedidoRepository.Adicionar(entity);
        }

        public async Task AdicionarPedidoRascunho(PedidoRascunho pedidoRascunho)
        {
            await _IPedidoRepository.AdicionarPedidoRascunho(pedidoRascunho);
        }

        public async Task Atualizar(Pedido entity)
        {
            if (!ExecutarValidacao(new PedidoValidation(), entity)) return;

            await _IPedidoRepository.Atualizar(entity);
        }

        public async Task Cancelar(int id, string observacao, bool sms)
        {
            var pedido = await _IPedidoRepository.Buscar(a => a.Id == id);
            var pedidoSelecionado = pedido.FirstOrDefault();
            pedidoSelecionado.StatusPedido = StatusPedido.Cancelado;
            pedidoSelecionado.Observacao = observacao;
            await _IPedidoRepository.Atualizar(pedidoSelecionado);
            if (sms)
            {
                var clienteTelefone = await _IClienteRepository.ObterPorId(pedidoSelecionado.ClienteId);

                await _ISMSGatewayFacade.Enviar(clienteTelefone.Telefone, $"Caro cliente o seu pedido foi cancelado por: {observacao}");
            }
        }

        public void CriarPedido(IEnumerable<PedidoRascunho> pedidoRascunhos)
        {
            var pedido = new Pedido() 
            {
                ClienteId=0,
                DataRegisto=DateTime.Now,
                IniciadoEm=DateTime.Now,
                Referencia=Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss")),
                StatusPedido=StatusPedido.Analise
            };
            var itens = new List<PedidoItem>();
            foreach(var item in pedidoRascunhos)
            {
                var pedidoItem = new PedidoItem()
                {
                    DataRegisto = DateTime.Now,
                    Desconto = 0,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Status = true,
                    Valor = item.Valor
                };
                itens.Add(pedidoItem);
            }
            pedido.ItensPedido = itens;

            _IPedidoRepository.CriarPedido(pedidoRascunhos, pedido);
        }

        public async Task Remover(int id)
        {
            await _IPedidoRepository.Remover(id);
        }

        public async Task RemoverItemRascunho(int id)
        {
            await _IPedidoRepository.RemoverItemRascunho(id);
        }
    }
}
