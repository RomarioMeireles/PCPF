using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IProdutoRepository _IProdutoRepository;
        private readonly IPedidoService _IPedidoService;
        public PedidoController(IPedidoRepository iPedidoRepository, IProdutoRepository iProdutoRepository, IPedidoService IPedidoService)
        {
            _IPedidoRepository = iPedidoRepository;
            _IProdutoRepository = iProdutoRepository;
            _IPedidoService = IPedidoService;
        }

        public async Task<IActionResult> Checkout()
        {
            string id = string.Empty;
            if (HttpContext.Session.GetString("userName") != null)
            {
                id = HttpContext.Session.GetString("userName");
                var pedidoRascunho = await _IPedidoRepository.ObterPedidoRascunhoPorUserName(id);

                return View(pedidoRascunho);
            }
            else
            {
                if (HttpContext.Session.GetString("anonimo") == null)
                {
                    HttpContext.Session.SetString("anonimo", HttpContext.Session.Id);
                }
                id = HttpContext.Session.GetString("anonimo");

                TempData["returnUrl"] = "/Pedido/Checkout";
                TempData.Keep("returnUrl");

                var pedidoRescunho = await _IPedidoRepository.ObterPedidoRascunhoPorSessaoId(id);

                pedidoRescunho.ToList().ForEach(a => a.Imagem = _IProdutoRepository.ObterImagem(a.ProdutoId).Result);

                return View(pedidoRescunho);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EfectuarPedido()
        {
            var id = HttpContext.Session.GetString("userName");
            var pedidoRascunho = await _IPedidoRepository.ObterPedidoRascunhoPorUserName(id);

            _IPedidoService.CriarPedido(pedidoRascunho);

            return RedirectToAction("MeusPedidos");
        }
        public async Task<IActionResult> MeusPedidos()
        {
            var pedidos = await _IPedidoRepository.ObterPedidoPorUserName(HttpContext.Session.GetString("userName"));
            pedidos.ToList().ForEach(a => a.Total = (a.ItensPedido.Sum(b => b.Quantidade * b.Valor)));
            return View(pedidos);
        }
        public async Task<IActionResult> RemoverItemRascunho(int id)
        {
            await _IPedidoService.RemoverItemRascunho(id);
            return RedirectToAction("Checkout");
        }

    }
}
