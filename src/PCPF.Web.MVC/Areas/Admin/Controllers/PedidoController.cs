using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PedidoController : BaseController
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IPedidoService _IPedidoService;
        private readonly IClienteRepository _IClienteRepository;

        public PedidoController(IPedidoRepository iPedidoRepository, IPedidoService iPedidoService,
           IClienteRepository iClienteRepository, INotificador notificador) : base(notificador)
        {
            _IPedidoRepository = iPedidoRepository;
            _IPedidoService = iPedidoService;
            _IClienteRepository = iClienteRepository;
        }
        public async Task<IActionResult> Lista()
        {
            var lista = await _IPedidoRepository.ObterTodos();
            lista.ToList().ForEach(a => a.Total = a.ItensPedido.Where(c=>c.Status==true).Sum(b=>b.Valor * b.Quantidade));
            return View(lista);
        }
        public async Task<IActionResult> Detalhes(int id)
        {
            var pedido = await _IPedidoRepository.ObterPedidoItemPorIdPedido(id);
            return View(pedido);
        }
        [HttpPost]
        public async Task<IActionResult> CancelarPedido(int PedidoId, string observacao)
        {
            await _IPedidoService.Cancelar(PedidoId, observacao, true);
            //Enviar SMS ao cliente
            return Redirect($"/Admin/Pedido/Detalhes/{PedidoId}");
        }
        public async Task<JsonResult> ConcluirPedido()
        {
            //Persistir em base de dados
            //Enviar SMS
            return Json("");
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.ClienteId = new SelectList(await _IClienteRepository.ObterTodos(), "Id", "Nome");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Pedido pedido)
        {
            var validacao = ExecutarValidacao(new PedidoValidation(), pedido);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pedido);
            }
            await _IPedidoService.Adicionar(pedido);

            ViewBag.ClienteId = new SelectList(await _IClienteRepository.ObterTodos(), "Id", "Nome");

            return RedirectToAction("Lista");
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var a = await _IPedidoRepository.ObterPorId(id);

            if (a == null)
                return BadRequest();
            return View(a);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Pedido pedido)
        {
            var validacao = ExecutarValidacao(new PedidoValidation(), pedido);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pedido);
            }
            await _IPedidoService.Atualizar(pedido);

            return RedirectToAction("Lista");
        }
    }
}