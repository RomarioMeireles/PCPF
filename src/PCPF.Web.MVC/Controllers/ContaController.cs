using Microsoft.AspNetCore.Mvc;
using PCPF.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPF.Web.MVC.Controllers
{
    public class ContaController : Controller
    {
     
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            return View();
        }

       
    }
}
