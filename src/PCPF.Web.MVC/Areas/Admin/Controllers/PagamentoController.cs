using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagamentoController : BaseController
    {
        private readonly IPagamentoRepository _IPagamentoRepository;
        private readonly IPagamentoService _IPagamentoService;
        private readonly IPedidoRepository _IPedidoRepository;

        public PagamentoController(IPagamentoRepository iPagamentoRepository, IPagamentoService iPagamentoService,
           IPedidoRepository iPedidoRepository, INotificador notificador) : base(notificador)
        {
            _IPagamentoRepository = iPagamentoRepository;
            _IPagamentoService = iPagamentoService;
            _IPedidoRepository = iPedidoRepository;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IPagamentoRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.PedidoId = new SelectList(await _IPedidoRepository.ObterTodos(), "Id");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Pagamento pagamento)
        {
            var validacao = ExecutarValidacao(new PagamentoValidation(), pagamento);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pagamento);
            }
            await _IPagamentoService.Adicionar(pagamento);

            ViewBag.PedidoId = new SelectList(await _IPedidoRepository.ObterTodos(), "Id");

            return RedirectToAction("Lista");
        }

        //[HttpGet]
        //public async Task<ActionResult> Actualizar(int id)
        //{
        //    var a = await _IPagamentoRepository.ObterPorId(id);

        //    if (a == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        return View(a);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Pagamento pagamento)
        {
            var validacao = ExecutarValidacao(new PagamentoValidation(), pagamento);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pagamento);
            }
            await _IPagamentoService.Atualizar(pagamento);

            return RedirectToAction("Lista");
        }
    }
}

