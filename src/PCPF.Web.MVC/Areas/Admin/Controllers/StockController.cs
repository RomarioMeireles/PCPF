using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using PCPF.Web.MVC.Extensions;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Autorizacao("0")]
    public class StockController : BaseController
    {
        private readonly IStockRepository _IStockRepository;
        private readonly IStockService _IStockService;
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        private readonly IProdutoRepository _IProdutoRepository;

        public StockController(IStockRepository iStockRepository, IStockService iStockService,
            IProdutoRepository iProdutoRepository, IUtilizadorRepository iUtilizadorRepository,
             INotificador notificador) : base(notificador)
        {
            _IStockService = iStockService;
            _IStockRepository = iStockRepository;
            _IProdutoRepository = iProdutoRepository;
            _IUtilizadorRepository = iUtilizadorRepository;
        }
        public async Task<IActionResult> Lista()
        {
            var lista = await _IStockRepository.ObterTodos();
            return View(lista);
        }

        public async Task<JsonResult> CreditarStock(int id, int quantidade)
        {
            await _IStockService.CreditarStock(id, quantidade);
            return Json("Stock creditado com sucesso.");
        }

        public async Task<IActionResult> ProdutoStockBaixo()
        {
            var produtos = await _IStockRepository.ObterStockBaixo();
            return View(produtos);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id", "Descricao");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Stock stock)
        {
            var validacao = ExecutarValidacao(new StockValidation(), stock);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(stock);
            }
            await _IStockService.Adicionar(stock);

            ViewBag.UtilizadorId = new SelectList(await _IUtilizadorRepository.ObterTodos(), "Id", "UserName");
            ViewBag.ProdutoId = new SelectList(await _IProdutoRepository.ObterTodos(), "Id", "Descricao");

            return RedirectToAction("Lista","Stock", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var a = await _IStockRepository.ObterPorId(id);

            if (a == null)
                return BadRequest();
            return View(a);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Stock stock)
        {
            var validacao = ExecutarValidacao(new StockValidation(), stock);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(stock);
            }
            await _IStockService.Atualizar(stock);

            return RedirectToAction("Lista", "Stock", new { area = "Admin" });
        }
    }
}