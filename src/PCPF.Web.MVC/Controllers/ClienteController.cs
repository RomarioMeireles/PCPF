using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Domain.Interfaces.IServices;
using PCPF.Domain.Model;
using PCPF.Domain.Model.Validation;
using PCPF.Domain.Notificacoes;
using System.Threading.Tasks;


namespace PCPF.Web.MVC.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IClienteRepository _IClienteRepository;
        private readonly IClienteService _IClienteService;
        private readonly IUtilizadorRepository _IUtilizadorRepository;
        private readonly IUtilizadorService _IUtilizadorService;
        public ClienteController(IUtilizadorRepository iUtilizadorRepository, IUtilizadorService iUtilizadorService,
            IClienteRepository iClienteRepository, IClienteService iClienteService,
            INotificador notificador) : base(notificador)
        {
            _IClienteRepository = iClienteRepository;
            _IClienteService = iClienteService;
            _IUtilizadorRepository = iUtilizadorRepository;
            _IUtilizadorService = iUtilizadorService;
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

        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente, string Password)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            _IClienteService.AdicionarCliente(cliente, Password);
            if (!OperacaoValida())
            {
                return View(cliente);
            }
            TempData["Sucesso"] = "Cadastro efectuado com sucesso!";
            //return RedirectToAction("Lista");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Actualizar(int id)
        {
            var cliente = await _IClienteRepository.ObterPorId(id);

            if (cliente == null)
                return BadRequest();
            return View(cliente);
        }

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

        [HttpGet]
        public async Task<ActionResult> Detalhes(int id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Conta");
            }
            var a = await _IClienteRepository.ObterPorId(id);
            if (a == null)
                return BadRequest();

            return View(a);
        }
    }
}