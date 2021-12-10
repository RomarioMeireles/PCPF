using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class UtilizadorController : BaseController
    {
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        private readonly IUtilizadorService _IUtilizadorService;

        public UtilizadorController(IUtilizadorRepository iUtilizadorRepository, IUtilizadorService iUtilizadorService,
            INotificador notificador) : base(notificador)
        {
            _IUtilizadorRepository = iUtilizadorRepository;
            _IUtilizadorService = iUtilizadorService;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IUtilizadorRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Utilizador utilizador)
        {
            var validacao = ExecutarValidacao(new UserValidation(), utilizador);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(utilizador);
            }
            await _IUtilizadorService.Adicionar(utilizador);
            return RedirectToAction("Conta","Login");
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var user = await _IUtilizadorRepository.ObterPorId(id);

            if (user == null)
                return BadRequest();
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Utilizador utilizador)
        {
            var validacao = ExecutarValidacao(new UserValidation(), utilizador);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(utilizador);
            }
            await _IUtilizadorService.Atualizar(utilizador);

            return RedirectToAction("Conta","Login");
        }
    }
}