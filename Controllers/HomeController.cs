using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web.Models;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable dt=SqlDBHelp.GetTable("select userid,username,password,email from ComUser where userid=1000");
            Models.User user = new User();
            if (dt != null) {
                user.UserId = Convert.ToInt32(dt.Rows[0][0]);
                user.UserName = dt.Rows[0][1].ToString();
                user.PassWord= dt.Rows[0][2].ToString();
                user.Email= dt.Rows[0][3].ToString();
            }
            return View(user);
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}