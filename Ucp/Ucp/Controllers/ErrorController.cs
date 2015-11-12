using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ucp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }

        // GET: Not found
        public ActionResult NotFoundPage()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}