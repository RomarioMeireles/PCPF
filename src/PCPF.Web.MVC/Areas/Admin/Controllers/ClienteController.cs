using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using PCPF.Web.MVC.Extensions;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Autorizacao("0")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _IClienteRepository;

        public ClienteController(IClienteRepository iClienteRepository)
        {
            _IClienteRepository = iClienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _IClienteRepository.ObterTodos();
            return View(lista);
        }
    }
}
