using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ucp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Перенаправление в случае ошибок
        protected static string ErrorPage = "~/Error/Error";
        protected static string NotFoundPage = "~/Error/NotFoundPage";
        
        public RedirectResult RedirectToNotFoundPage
        {
            get
            {
                return Redirect(NotFoundPage);
            }
        }
                
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            filterContext.Result = Redirect(ErrorPage);
        }
    }
}