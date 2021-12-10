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
    public class PedidoItemController : BaseController
    {
        private readonly IPedidoItemRepository _IPedidoItemRepository;
        private readonly IPedidoItemService _IPedidoItemService;
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IProdutoRepository _IProdutoRepository;

        public PedidoItemController(IPedidoItemRepository iPedidoItemRepository, IPedidoItemService iPedidoItemService,
          IProdutoRepository iProdutoRepository, IPedidoRepository iPedidoRepository, INotificador notificador) : base(notificador)
        {
            _IPedidoItemRepository = iPedidoItemRepository;
            _IPedidoItemService = iPedidoItemService;
            _IPedidoRepository = iPedidoRepository;
            _IProdutoRepository = iProdutoRepository;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IPedidoItemRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.PedidoId = new SelectList(await _IPedidoRepository.ObterTodos(), "Id");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(PedidoItem pedidoItem)
        {
            var validacao = ExecutarValidacao(new PedidoItemValidation(), pedidoItem);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pedidoItem);
            }
            await _IPedidoItemService.Adicionar(pedidoItem);


            ViewBag.PedidoId = new SelectList(await _IPedidoRepository.ObterTodos(), "Id");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id");
            return RedirectToAction("Lista");
        }

        //[HttpGet]
        //public async Task<ActionResult> Actualizar(int id)
        //{
        //    var a = await _IPedidoItemRepository.ObterPorId(id);

        //    if (a == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        return View(a);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(PedidoItem pedidoItem)
        {
            var validacao = ExecutarValidacao(new PedidoItemValidation(), pedidoItem);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(pedidoItem);
            }
            await _IPedidoItemService.Atualizar(pedidoItem);

            return RedirectToAction("Lista");
        }
    }
}