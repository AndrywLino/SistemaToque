﻿using SistemaToque.Models;
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

            ServiceCSV.WriteCSVFileToque(dir, toqueteste);

            //ServiceCSV.ReadCSVFileToque("C:/Users/andrywafonso/Desktop/Raspberry toque/toque.csv");
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserModel user)
        {
            string userName = user.UserName.ToUpper();
            string password = user.Password;

            if (userName == "SUPERVISOR")
            {
                if (password == "ceinet123")
                {
                    user.Status = true;
                    return RedirectToAction("Toques", user.Status);
                }
                else
                {
                    user.Status = false;
                    ViewBag.UsuarioInvalido = "";
                    ViewBag.SenhaInvalido = "Senha Invalida";
                    return View("Login", user);
                }
            }
            else
            {
                user.Status = false;
                ViewBag.UsuarioInvalido = "Usuario Invalido";
                ViewBag.SenhaInvalido = "";
                return View("Login", user);
            }
        }

        public ActionResult Login(UserModel user)
        {
            ViewBag.UsuarioInvalido = "";
            ViewBag.SenhaInvalido = "";
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Toque(bool logado = false)
        {
            logado = true;
            if (logado)
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            else
                return View("Login");
        }

        public ActionResult Toques(bool logado = false)
        {
            logado = true;
            if (logado)
            {
                List<ToqueModel> model = LerToquesCSV();

                return View(model);
            }
            else
                return View("Login");
        }

        private List<ToqueModel> LerToquesCSV()
        {
            string path = Path.Combine(Server.MapPath("~/CSV/toque.csv"));
            List<ToqueModel> planilha = ServiceCSV.ReadCSVFileToque(path);
            foreach (var pla in planilha)
            {
                if (pla.IsSegunda == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Segunda ";
                }
                if (pla.IsTerca == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Terça ";
                }
                if (pla.IsQuarta == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Quarta ";
                }
                if (pla.IsQuinta == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Quinta ";
                }
                if (pla.IsSexta == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Sexta ";
                }
                if (pla.IsSabado == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Sabado ";
                }
                if (pla.IsDomingo == 1)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Domingo ";
                }
                if(pla.Canal == 1)
                {
                    pla.TxCanal = "P2";
                }
                if (pla.Canal == 2)
                {
                    pla.TxCanal = "HDMI";
                }
                if (pla.NivelEnsino == 1)
                {
                    pla.TxEnsino = "Fund 1";
                }
                if (pla.NivelEnsino == 2)
                {
                    pla.TxEnsino = "Fund2";
                }
                if (pla.NivelEnsino == 3)
                {
                    pla.TxEnsino = "EM";
                }

                pla.DiaSemana = pla.DiaSemana.Substring(2);
            }
            return planilha;
        }

    }
}