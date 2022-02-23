using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
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
        public IActionResult Cadastrar()
        {
            return View();
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoViewModel pr)
        {
            if (pr.Imagem == null)
            {
                Notificar("Selecione a imagem.");
                return View(pr);
            }
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
        [Route("actualizar-produto/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var a = await _IProdutoRepository.ObterPorId(id);
            return View(a);
        }
        [Route("actualizar-produto/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> Actualizar(Produto produto, IFormFile ImagemNova)
        {
            if (!ModelState.IsValid) return View(produto);

            var oldProduto = await _IProdutoRepository.ObterPorId(produto.Id);

            produto.Imagem = oldProduto.Imagem;
            produto.UtilizadorId = oldProduto.UtilizadorId;

            if(ImagemNova != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(ImagemNova, imgPrefixo))
                {
                    return View(produto);
                }

                var combinedPath = string.Concat(imgPrefixo, ImagemNova.FileName);
                produto.Imagem = combinedPath;
            }

            await _IProdutoService.Atualizar(produto);

            if (!OperacaoValida())
            {
                return View(produto);
            }
            TempData["Sucesso"] = "Operação executada com sucesso!";
            return Redirect("/Admin/Produto/lista-de-produtos");
        }
        [Route("inactivar")]
        public async Task<JsonResult> Inactivar(int id)
        {
            await _IProdutoService.Remover(id);
            return Json("Produto removido com sucesso.");
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