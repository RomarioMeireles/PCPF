using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PCPF.Domain.Interfaces;
using PCPF.Web.MVC.Models;

namespace PCPF.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        public HomeController(ILogger<HomeController> logger, IUtilizadorRepository IUtilizadorRepository)
        {
            _logger = logger;
            _IUtilizadorRepository = IUtilizadorRepository;
        }

        public IActionResult Cliente()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexAdmin()
        {
            return View();
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

        public async Task<IActionResult> Login(string userName, string password)
        {
            var utilizador = await _IUtilizadorRepository.Buscar(a => a.UserName == userName && a.Password == Infra.CrossCuting.Seguranca.Criptografia.CriptografarSenha(password));
            if(utilizador.Count() > 0)
            {
                string returnUrl = string.Empty;
                var utilizadorSelecionado = utilizador.FirstOrDefault();
                switch (utilizadorSelecionado.Perfil)
                {
                    case Domain.Model.ValueObjects.Perfil.Administrador:
                        returnUrl = "/Admin/Utilizador/Cadastrar";
                        break;
                    case Domain.Model.ValueObjects.Perfil.Cliente:
                        returnUrl = "/Home/Index";
                        break;
                }

                HttpContext.Session.SetInt32("userId", utilizadorSelecionado.Id);
                HttpContext.Session.SetString("userName", utilizadorSelecionado.UserName);
                HttpContext.Session.SetString("nome", utilizadorSelecionado.Nome);

                return Redirect(returnUrl);
            }
            TempData["ErroLogin"] = "Credencias inválidas!";
            return RedirectToAction("Index");
        }
    }
}
