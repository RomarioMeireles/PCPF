using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IProdutoRepository _IProdutoRepository;
        public PedidoController(IPedidoRepository iPedidoRepository, IProdutoRepository iProdutoRepository)
        {
            _IPedidoRepository = iPedidoRepository;
            _IProdutoRepository = iProdutoRepository;
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

                var pedidoRescunho = await _IPedidoRepository.ObterPedidoRascunhoPorSessaoId(id);

                pedidoRescunho.ToList().ForEach(a => a.Imagem = _IProdutoRepository.ObterImagem(a.ProdutoId).Result);

                return View(pedidoRescunho);
            }
        }
    }
}
