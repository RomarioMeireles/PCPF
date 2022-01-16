using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Extensions
{
    public class CarrinhoViewComponent: ViewComponent
    {
        private readonly IPedidoRepository _IPedidoRepository;
        Tuple<int, decimal> total;
        public CarrinhoViewComponent(IPedidoRepository iPedidoRepository)
        {
            _IPedidoRepository = iPedidoRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string id = string.Empty;
            
            if (HttpContext.Session.GetString("userName") != null)
            {
                id = HttpContext.Session.GetString("userName");
                var carItens = await _IPedidoRepository.ObterPedidoRascunhoPorUserName(id);

                total = new Tuple<int, decimal>(carItens.Count(), carItens.Sum(a => a.Valor * a.Quantidade));
                //{
                //}; = carItens.Sum(a => a.Valor * a.Quantidade);
            }
            else
            {
                id = HttpContext.Session.GetString("anonimo");
                var carItens = await _IPedidoRepository.ObterPedidoRascunhoPorSessaoId(id);

                total = new Tuple<int, decimal>(carItens.Count(), carItens.Sum(a => a.Valor * a.Quantidade));
                //total = carItens.Sum(a => a.Valor * a.Quantidade);
            }

            return View(total);
        }
    }
}
