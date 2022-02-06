using Microsoft.AspNetCore.Mvc.Razor;
using PCPF.Domain.Model.ValueObjects;

namespace PCPF.Web.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static (string, string) Status(this RazorPage page, bool status)
        {
            if (status)
            {
                return ("Activo", "green");
            }
            return ("Inactivo", "red");
        }
        public static string StatusPedido(this RazorPage page, StatusPedido status)
        {
            var statusResult = string.Empty;
            switch (status)
            {
                case Domain.Model.ValueObjects.StatusPedido.Analise:
                    statusResult = $"<b style='color: orange'>{status.ToString()}</b>";
                    break;
                case Domain.Model.ValueObjects.StatusPedido.Processamento:
                    statusResult = $"<b style='color: blue'>{status.ToString()}</b>";
                    break;
                case Domain.Model.ValueObjects.StatusPedido.Finalizado:
                    statusResult = $"<b style='color: green'>{status.ToString()}</b>";
                    break;
                case Domain.Model.ValueObjects.StatusPedido.Cancelado:
                    statusResult = $"<b style='color: red'>{status.ToString()}</b>";
                    break;
                default:
                    break;
            }
            return statusResult;
        }
    }
}
