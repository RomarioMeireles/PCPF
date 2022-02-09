using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using System.Linq;

namespace PCPF.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IClienteRepository _IClienteRepository;
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IStockRepository _IStockRepository;

        public DashboardController(IClienteRepository iClienteRepository, IPedidoRepository iPedidoRepository, IStockRepository iStockRepository)
        {
            _IClienteRepository = iClienteRepository;
            _IPedidoRepository = iPedidoRepository;
            _IStockRepository = iStockRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Cliente = _IClienteRepository.ObterTodos().Result.Count();
            ViewBag.Stock = _IStockRepository.ObterStockBaixo().Result.Count();
            ViewBag.PedidoNovo = _IPedidoRepository.Buscar(a=> a.StatusPedido==Domain.Model.ValueObjects.StatusPedido.Analise).Result.Count();
            ViewBag.PedidoCancelado = _IPedidoRepository.Buscar(a => a.StatusPedido == Domain.Model.ValueObjects.StatusPedido.Cancelado).Result.Count();
            return View();
        }
    }
}
