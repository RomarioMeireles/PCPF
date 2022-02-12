using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Controllers;
using PCPF.Web.MVC.Extensions;
using PCPF.Web.MVC.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/Produto")]
    [Autorizacao("0")]
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
        public async Task<ActionResult> Cadastrar(ProdutoViewModel pr)
        {
            if (!ModelState.IsValid) return View(pr);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(pr.Imagem, imgPrefixo))
            {
                return View(pr);
            }

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));

            var combinedPath = string.Concat(imgPrefixo, pr.Imagem.FileName);

            var produto = new Produto(pr.Descricao, combinedPath, pr.CodigoBarras, pr.Observacao, pr.QuantidadeMinima, pr.Valor, userId);
            var stock = new Stock(pr.Quantidade, 0, pr.NumeroLote, pr.DataValidade, userId);
            
            _IProdutoService.Adicionar(produto, stock);

            if (!OperacaoValida())
            {
                return View(pr);
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

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}