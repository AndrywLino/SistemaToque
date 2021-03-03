using SistemaToque.Models;
using SistemaToque.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaToque.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string dir = "C:/Users/andrywafonso/Desktop/Raspberry toque/criado/";
            dir += "teste.csv";
            List<ToqueModel> toqueteste = new List<ToqueModel>();

            toqueteste.Add(new ToqueModel() { Arquivo = "teste123", Canal = 3, DiaSemana = "Sabado", Hora = "11:00:00", IsAtivo = 0, NivelEnsino = 5 });
            toqueteste.Add(new ToqueModel() { Arquivo = "teste", Canal = 2, DiaSemana = "Domingo", Hora = "11:30:00", IsAtivo = 1, NivelEnsino = 6 });
            toqueteste.Add(new ToqueModel() { Arquivo = "teste123456", Canal = 1, DiaSemana = "Sexta-Feira", Hora = "12:15:00", IsAtivo = 2, NivelEnsino = 7 });

            ServiceCSV.WriteCSVFileToque(dir, toqueteste);

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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Toque()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Teste()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}