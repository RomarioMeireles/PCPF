using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.ValueObjects;
using PCPF.Domain.Notificacoes;
using PCPF.Infra.CrossCuting.Seguranca;
using PCPF.Web.MVC.Controllers;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Utilizador")]
    public class UtilizadorController : BaseController
    {
        public readonly IUtilizadorRepository _IUtilizadorRepository;
        public readonly IUtilizadorService _IUtilizadorService;

        public UtilizadorController(IUtilizadorRepository iUtilizadorRepository, IUtilizadorService iUtilizadorService, INotificador notificador):base(notificador)
        {
            _IUtilizadorRepository = iUtilizadorRepository;
            _IUtilizadorService = iUtilizadorService;
        }
        [Route("lista-de-utilizadores")]
        public async Task<IActionResult> Index()
        {
            var utilizadores = await _IUtilizadorRepository.ObterTodos();
            return View(utilizadores);
        }
        [Route("Cadastrar")]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Utilizador utilizador)
        {
            utilizador.Perfil = Perfil.Administrador;
            

            if (!ModelState.IsValid)
            {
                return View(utilizador);
            }

            var resultadoValidacaoPassword = PasswordRequiriment.ValidatePassword(utilizador.Password);
            if (!resultadoValidacaoPassword.Item1)
            {
                ModelState.AddModelError(string.Empty, resultadoValidacaoPassword.Item2);
                return View(utilizador);
            }

            utilizador.ToHashPassword();
            await _IUtilizadorService.Adicionar(utilizador);

            if (!OperacaoValida())
            {
                return View(utilizador);
            }
            TempData["Sucesso"] = "Operação executada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
