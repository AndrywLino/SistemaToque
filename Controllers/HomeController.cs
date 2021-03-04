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

        [HttpPost]
        public ActionResult EditarToque(ToqueModel toque)
        {
            List<ToqueModel> toques = LerToquesCSV();
            List<ToqueExportModel> toquesE = new List<ToqueExportModel>();

            int i = 0;

            foreach (var item in toques)
            {
                ToqueExportModel it = new ToqueExportModel();
                it.Arquivo = item.Arquivo;
                it.Nome = item.Nome;
                it.Hora = item.Hora;
                it.Canal = item.Canal;
                it.IsSegunda = item.IsSegunda;
                it.IsTerca = item.IsTerca;
                it.IsQuarta = item.IsQuarta;
                it.IsQuinta = item.IsQuinta;
                it.IsSexta = item.IsSexta;
                it.IsSabado = item.IsSabado;
                it.IsDomingo = item.IsDomingo;
                it.IsAtivo = item.NivelEnsino;

                toquesE.Add(it);
            }
            foreach (var item in toques)
            {
                if (item.Arquivo == toque.Arquivo)
                {
                    toquesE[i].Arquivo = toque.Arquivo;
                    toquesE[i].Nome = toque.Nome;
                    toquesE[i].Hora = toque.Hora;
                    toquesE[i].Canal = toque.Canal;
                    toquesE[i].IsSegunda = toque.IsSegunda;
                    toquesE[i].IsTerca = toque.IsTerca;
                    toquesE[i].IsQuarta = toque.IsQuarta;
                    toquesE[i].IsQuinta = toque.IsQuinta;
                    toquesE[i].IsSexta = toque.IsSexta;
                    toquesE[i].IsSabado = toque.IsSabado;
                    toquesE[i].IsDomingo = toque.IsDomingo;
                    toquesE[i].IsAtivo = toque.NivelEnsino;

                    break;
                }
                i++;
            }
            string dir = Path.Combine(Server.MapPath("~/CSV/toque.csv"));
            ServiceCSV.WriteCSVFileToque(dir, toquesE);
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarToque(ToqueExportModel toque)
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditarToque(string arquivo)
        {
            string arq = arquivo;
            List<ToqueModel> toques = LerToquesCSV();
            ToqueModel toque = new ToqueModel();
            foreach (var item in toques)
            {
                if (arq == item.Arquivo)
                {
                    toque = item;
                }
            }
            return View(toque);
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