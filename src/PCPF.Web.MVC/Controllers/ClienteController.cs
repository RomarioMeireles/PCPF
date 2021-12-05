using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IClienteRepository _IClienteRepository;
        private readonly IClienteService _IClienteService;

        public ClienteController(IClienteRepository iClienteRepository, IClienteService iClienteService,
            INotificador notificador) : base(notificador)
        {
            _IClienteRepository = iClienteRepository;
            _IClienteService = iClienteService;
        }
        public async Task<ActionResult> Lista()
        {
            var lista = await _IClienteRepository.ObterTodos();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Cliente cliente)
        {
            var validacao = ExecutarValidacao(new ClienteValidation(), cliente);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(cliente);
            }
            await _IClienteService.Adicionar(cliente);
            return RedirectToAction("Lista");
        }

       // [HttpGet]
        //public async Task<ActionResult> Actualizar(int id)
        //{
        //    var cliente = await _IClienteRepository.ObterPorId(id);

        //    if (cliente == null)
        //       // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    return View(cliente);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Actualizar(Cliente cliente)
        {
            var validacao = ExecutarValidacao(new ClienteValidation(), cliente);
            if (!validacao)
            {
                var erro = ObterMensagensErro();
                foreach (var item in erro)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(cliente);
            }
            await _IClienteService.Atualizar(cliente);

            return RedirectToAction("Lista");
        }
    }
}