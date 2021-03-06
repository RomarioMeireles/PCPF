using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    [Route("Catalogo")]
    public class CatalogoController : BaseController
    {
        private readonly IProdutoRepository _IProdutoRepository;
        private readonly IPedidoService _IPedidoService;
        private readonly IStockRepository _stockRepository;
        public CatalogoController(IProdutoRepository iProdutoRepository, IPedidoService iPedidoService, IStockRepository IStockRepository, INotificador notificador):base(notificador)
        {
            _IProdutoRepository = iProdutoRepository;
            _IPedidoService = iPedidoService;
            _stockRepository = IStockRepository;
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
            var stock = _stockRepository.ObterQuantideProduto(id);
            ViewBag.Quantidade = stock;
            return View(produto);
        }
        [Route("AdicionarItemPedido")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemPedido(int produtoId, int quantidade)
        {
            var produto = await _IProdutoRepository.ObterPorId(produtoId);

            if(quantidade==0)
            {
                quantidade = 1;
            }
            var stock = _stockRepository.ObterQuantideProduto(produtoId);

            if (quantidade > stock)
            {
                TempData["Error"] = "Quantidade em Stock insuficiente.";
                return Redirect($"/Catalogo/detalhes-produto/{produtoId}");
            }

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
