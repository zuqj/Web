using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoginCheckFilterAttribute() { IsChecked=true});
        }
    }


    public class LoginCheckFilterAttribute : ActionFilterAttribute  //注意继承：ActionFilterAttribute
    {
        /// <summary>
        /// 是否校验，默认为true
        /// </summary>
        public bool IsChecked { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //校验用户是否已登录
            if (IsChecked)
            {
                if (filterContext.HttpContext.Session["LoginUID"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/In");
                }
            }
        }
    }

}
