using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [LoginCheckFilterAttribute(IsChecked = false)]
        public ActionResult In()
        {
            return View("Login");
        }
    }
}