using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Web.MVC.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        public readonly IProdutoRepository _IProdutoRepository;
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IPedidoService _IPedidoService;
        public HomeController(ILogger<HomeController> logger, IUtilizadorRepository IUtilizadorRepository, IProdutoRepository IProdutoRepository, IPedidoRepository IPedidoRepository, IPedidoService IPedidoService)
        {
            _logger = logger;
            _IUtilizadorRepository = IUtilizadorRepository;
            _IProdutoRepository = IProdutoRepository;
            _IPedidoRepository = IPedidoRepository;
            _IPedidoService = IPedidoService;
        }

        public IActionResult Cliente()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var produtos = await _IProdutoRepository.ObterTodos();
            return View(produtos);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SemAcesso()
        {
            return View("SemAcesso",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message="Não tem acesso ao recurso." });
        }

        public async Task<IActionResult> Login(string userName, string password)
        {
            var utilizador = await _IUtilizadorRepository.Buscar(a => a.UserName == userName && a.Password == Infra.CrossCuting.Seguranca.Criptografia.CriptografarSenha(password));
            if(utilizador.Count() > 0)
            {
                string returnUrl = string.Empty;
                var utilizadorSelecionado = utilizador.FirstOrDefault();

                HttpContext.Session.SetInt32("userId", utilizadorSelecionado.Id);
                HttpContext.Session.SetString("userName", utilizadorSelecionado.UserName);
                HttpContext.Session.SetString("nome", utilizadorSelecionado.Nome);
                HttpContext.Session.SetString("perfil", utilizadorSelecionado.Perfil.ToString());

                switch (utilizadorSelecionado.Perfil)
                {
                    case Domain.Model.ValueObjects.Perfil.Administrador:
                        returnUrl = "/Admin/Dashboard/Index";
                        break;
                    case Domain.Model.ValueObjects.Perfil.Cliente:
                        if(TempData["returnUrl"]!=null)
                        {
                            returnUrl = TempData["returnUrl"].ToString();
                            var pedido = await _IPedidoRepository.ObterPedidoRascunhoPorSessaoId(HttpContext.Session.GetString("anonimo"));
                            pedido.ToList().ForEach(a => a.UserName = HttpContext.Session.GetString("userName"));
                            await _IPedidoService.ActualizarPedidoRascunho(pedido);
                        }
                        else
                        {
                            returnUrl = "/Home/Index";
                        }
                        break;
                }

                return Redirect(returnUrl);
            }
            TempData["ErroLogin"] = "Credencias inválidas!";
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("nome");
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
