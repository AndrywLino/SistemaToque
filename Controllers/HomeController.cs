using SistemaToque.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaToque.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ServiceCSV.ReadCSVFileToque("C:/Users/andrywafonso/Desktop/Raspberry toque/toque.csv");
            return View();
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

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}