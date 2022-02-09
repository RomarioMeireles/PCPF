using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    [Route("Catalogo")]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _IProdutoRepository;
        private readonly IPedidoService _IPedidoService;
        public CatalogoController(IProdutoRepository iProdutoRepository, IPedidoService iPedidoService)
        {
            _IProdutoRepository = iProdutoRepository;
            _IPedidoService = iPedidoService;
        }
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _IProdutoRepository.ObterTodos();
            return View(produtos);
        }
        [Route("detalhes-produto/{id:int}")]
        public async Task<IActionResult> ObterProduto(int id)
        {
            var produto = await _IProdutoRepository.ObterPorId(id);
            return View(produto);
        }
        [Route("AdicionarItemPedido")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemPedido(int produtoId, int quantidade)
        {
            var produto = await _IProdutoRepository.ObterPorId(produtoId);

            var pedidoRascunho = new PedidoRascunho()
            {
                Descricao = produto.Descricao,
                ProdutoId = produtoId,
                Quantidade = quantidade,
                Valor = produto.Valor,
                Imagem=produto.Imagem
            };
            //sessão do utilizador
            if (HttpContext.Session.GetString("userName") != null)
            {
                pedidoRascunho.UserName = HttpContext.Session.GetString("userName");
            }
            else
            {
                if(HttpContext.Session.GetString("anonimo") == null)
                {
                    HttpContext.Session.SetString("anonimo", HttpContext.Session.Id);
                }
                pedidoRascunho.SessionId = HttpContext.Session.GetString("anonimo");
            }
            
            await _IPedidoService.AdicionarPedidoRascunho(pedidoRascunho);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("pesquisar-produtos")]
        public async Task<IActionResult> PesquisarProduto(string query)
        {
            if (string.IsNullOrEmpty(query))
                return RedirectToAction("Index");
            var produtos = await _IProdutoRepository.Buscar(a => a.Descricao.Contains(query));
            return View("Index", produtos);
        }
    }
}
