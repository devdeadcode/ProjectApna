using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StripeIntegrationApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Charge");
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

        public ActionResult Charge(string stripetoken, string stripeEmail)
        {
            string apiKey = "sk_test_AKWEjlI9ncrRklNfd8x6PerU";
            var stripeClient = new Stripe.StripeClient(apiKey);

            dynamic response = stripeClient.CreateChargeWithToken(10, stripetoken, "usd", stripeEmail);
            dynamic user = stripeClient.CreateCustomer(null, null, stripeEmail, null, null);
            dynamic users = stripeClient.ListCustomers(3,3);

            //if (response.IsError == false && response.Paid)
            //{
            //    // success
            //}

            return View("Charge");
        }
    }
}