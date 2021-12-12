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
using System.IO;


namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoRepository _IProdutoRepository;
        private readonly IProdutoService _IProdutoService;
        private readonly IUtilizadorRepository _IUtilizadorRepository;

        public ProdutoController(IProdutoRepository iProdutoRepository, IProdutoService iProdutoService,
           IUtilizadorRepository iUtilizadorRepository, INotificador notificador) : base(notificador)
        {
            _IProdutoRepository = iProdutoRepository;
            _IProdutoService = iProdutoService;
            _IUtilizadorRepository = iUtilizadorRepository;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IProdutoRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Produto produto)
        {
            var validacao = ExecutarValidacao(new ProdutoValidation(), produto);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(produto);
            }
            await _IProdutoService.Adicionar(produto);

            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");

            return RedirectToAction("Lista","Produto", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var a = await _IProdutoRepository.ObterPorId(id);

            if (a == null)
                return BadRequest();
            return View(a);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Produto produto)
        {
            var validacao = ExecutarValidacao(new ProdutoValidation(), produto);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(produto);
            }
            await _IProdutoService.Atualizar(produto);

            return RedirectToAction("Lista","Produto", new { area = "Admin" });
        }
    }
}