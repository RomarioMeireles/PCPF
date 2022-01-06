using Microsoft.AspNetCore.Mvc.Razor;

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
    }
}
