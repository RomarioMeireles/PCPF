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
    public class ProdutoFornecedorController : BaseController
    {
        private readonly IProdutoFornecedorRepository _IProdutoFornecedorRepository;
        private readonly IProdutoFornecedorService _IProdutoFornecedorService;
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        private readonly IFornecedorRepository _IFornecedorRepository;
        private readonly IProdutoRepository _IProdutoRepository;

        public ProdutoFornecedorController(IProdutoFornecedorRepository iProdutoFornecedorRepository,
            IProdutoFornecedorService iProdutoFornecedorService, IProdutoRepository iProdutoRepository,
            IUtilizadorRepository iUtilizadorRepository, IFornecedorRepository iFornecedorRepository,
            INotificador notificador) : base(notificador)
        {
            _IProdutoFornecedorRepository = iProdutoFornecedorRepository;
            _IProdutoFornecedorService = iProdutoFornecedorService;
            _IProdutoRepository = iProdutoRepository;
            _IUtilizadorRepository = iUtilizadorRepository;
            _IFornecedorRepository = iFornecedorRepository;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IProdutoFornecedorRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id", "Descricao");
            ViewBag.FornecedorId = new SelectList(await _IFornecedorRepository.ObterTodos(), "Id");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(ProdutoFornecedor produtoFornecedor)
        {
            var validacao = ExecutarValidacao(new ProdutoFornecedorValidation(), produtoFornecedor);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(produtoFornecedor);
            }
            await _IProdutoFornecedorService.Adicionar(produtoFornecedor);

            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id", "Descricao");
            ViewBag.FornecedorId = new SelectList(await _IFornecedorRepository.ObterTodos(), "Id");

            return RedirectToAction("Lista");
        }

        //[HttpGet]
        //public async Task<ActionResult> Actualizar(int id)
        //{
        //    var a = await _IProdutoFornecedorRepository.ObterPorId(id);

        //    if (a == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        return View(a);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(ProdutoFornecedor produtoFornecedor)
        {
            var validacao = ExecutarValidacao(new ProdutoFornecedorValidation(), produtoFornecedor);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(produtoFornecedor);
            }
            await _IProdutoFornecedorService.Atualizar(produtoFornecedor);

            return RedirectToAction("Lista");
        }
    }
}
