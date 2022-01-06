using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/Produto")]
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
        [Route("lista-de-produtos")]
        public async Task<ActionResult> Lista()
        {
            var lista = await _IProdutoRepository.ObterTodos();
            return View(lista);
        }

        [Route("novo-produto")]
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<ActionResult> Cadastrar(Produto produto)
        {
            produto.UtilizadorId = (int) HttpContext.Session.GetInt32("userId");

            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            
            await _IProdutoService.Adicionar(produto);

            if (!OperacaoValida())
            {
                return View(produto);
            }
            TempData["Sucesso"] = "Operação executada com sucesso!";
            return RedirectToAction("Lista");
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