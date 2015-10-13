using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CRMController : Controller
    {
        //
        // GET: /CRM/
        public ActionResult Index()
        {
            ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();
            return View(obj.GetData("Hello"));
        }
	}
}