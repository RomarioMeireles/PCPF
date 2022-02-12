using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using PCPF.Web.MVC.Extensions;
using System;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Autorizacao("0")]
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
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(Fornecedor fornecedor)
        {
            if (!ModelState.IsValid) return View(fornecedor);

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            fornecedor.UtilizadorId = userId;
            await _IFornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida())
            {
                return View(fornecedor);
            }
            TempData["Sucesso"] = "Operação executada com sucesso!";
            return Redirect("/Admin/Fornecedor/Lista");
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var a = await _IFornecedorRepository.ObterPorId(id);

            if (a == null)
                return BadRequest();
            return View(a);
        }

        [HttpPost]
        public async Task<ActionResult> Actualizar(Fornecedor fornecedor)
        {
            if (!ModelState.IsValid) return View(fornecedor);
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            fornecedor.UtilizadorId = userId;
            await _IFornecedorService.Atualizar(fornecedor);

            if (!OperacaoValida())
            {
                return View(fornecedor);
            }
            TempData["Sucesso"] = "Operação executada com sucesso!";

            return Redirect("/Admin/Fornecedor/Lista");
        }
    }
}
