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
    public class FornecedorController : BaseController
    {
        private readonly IFornecedorRepository _IFornecedorRepository;
        private readonly IFornecedorService _IFornecedorService;
        private readonly IUtilizadorRepository _IUtilizadorRepository;

        public FornecedorController(IFornecedorRepository iFornecedorRepository, IFornecedorService iFornecedorService,
           IUtilizadorRepository iUtilizadorRepository, INotificador notificador) : base(notificador)
        {
            _IFornecedorRepository = iFornecedorRepository;
            _IFornecedorService = iFornecedorService;
            _IUtilizadorRepository = iUtilizadorRepository;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IFornecedorRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Fornecedor fornecedor)
        {
            var validacao = ExecutarValidacao(new FornecedorValidation(), fornecedor);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(fornecedor);
            }
            await _IFornecedorService.Adicionar(fornecedor);

            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");

            return RedirectToAction("Lista"/*, "Fornecedor", new { area = "Admin" }*/);
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var a = await _IFornecedorRepository.ObterPorId(id);

            if (a == null)
                return BadRequest();
            return View(a);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Fornecedor fornecedor)
        {
            var validacao = ExecutarValidacao(new FornecedorValidation(), fornecedor);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(fornecedor);
            }
            await _IFornecedorService.Atualizar(fornecedor);

            return RedirectToAction("Lista", "Fornecedor", new { area = "Admin" });
        }
    }
}
