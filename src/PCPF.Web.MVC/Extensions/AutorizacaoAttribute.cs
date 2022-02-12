using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PCPF.Domain.Model.ValueObjects;

namespace PCPF.Web.MVC.Extensions
{
    public class AutorizacaoAttribute: ActionFilterAttribute
    {
        public AutorizacaoAttribute(string perfil)
        {
            Perfil = perfil;
        }

        public string Perfil { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("userId")))
            {
                context.HttpContext.Response.Redirect("/Home/Index");
            }
            else
            {
                if (!string.IsNullOrEmpty(Perfil))
                {
                    if (Perfil != context.HttpContext.Session.GetString("perfil"))
                    {
                        context.HttpContext.Response.Redirect("/Home/SemAcesso");
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
