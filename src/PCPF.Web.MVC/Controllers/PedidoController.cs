using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Notificacoes;
using PCPF.Web.MVC.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class PedidoController :  BaseController
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IProdutoRepository _IProdutoRepository;
        private readonly IPedidoService _IPedidoService;
        private readonly IPagamentoService _IPagamentoService;
        public PedidoController(IPedidoRepository iPedidoRepository, IProdutoRepository iProdutoRepository, IPedidoService IPedidoService, IPagamentoService IPagamentoService, INotificador notificador):base(notificador)
        {
            _IPedidoRepository = iPedidoRepository;
            _IProdutoRepository = iProdutoRepository;
            _IPedidoService = IPedidoService;
            _IPagamentoService = IPagamentoService;
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
        [Autorizacao("1")]
        public async Task<IActionResult> EfectuarPedido()
        {
            var id = HttpContext.Session.GetString("userName");
            var pedidoRascunho = await _IPedidoRepository.ObterPedidoRascunhoPorUserName(id);

            _IPedidoService.CriarPedido(pedidoRascunho);

            return RedirectToAction("MeusPedidos");
        }
        [Autorizacao("1")]
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
        [Autorizacao("1")]
        public async Task<IActionResult> EfectuarPagamento(int id)
        {
            var pedido = await _IPedidoRepository.ObterPedidoItemPorIdPedido(id);
            return View(pedido);
        }
        [HttpPost]
        [Autorizacao("1")]
        public async Task<IActionResult> EfectuarPagamento(Pagamento pagamento, IFormFile comprovativo, int id)
        {
            pagamento.Id = 0;
            var docPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(comprovativo, docPrefixo))
            {
                return View(pagamento);
            }

            var combinedPath = string.Concat(docPrefixo, comprovativo.FileName);
            pagamento.Comprovativo = combinedPath;
            await _IPagamentoService.Adicionar(pagamento, id);
            //Enviar SMS
            TempData["Sucesso"] = "O comprovativo de pagamento foi enviado com sucesso!";
            return RedirectToAction("MeusPedidos");
        }
        private async Task<bool> UploadArquivo(IFormFile arquivo, string docPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/comprovativos", docPrefixo + arquivo.FileName);

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
        public async Task<IActionResult> PedidoDetalhes(int id)
        {
            var pedido = await _IPedidoRepository.ObterPedidoItemPorIdPedido(id);
            return View(pedido);
        }
        [HttpPost]
        [Autorizacao("1")]
        public async Task<IActionResult> CancelarPedido(int PedidoId, string observacao)
        {
            await _IPedidoService.Cancelar(PedidoId, observacao, false);
            return RedirectToAction("PedidoDetalhes", new { id= PedidoId });
        }
    }
}
