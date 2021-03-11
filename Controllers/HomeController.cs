using SistemaToque.Models;
using SistemaToque.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                    ViewBag.SenhaInvalido = "Senha Inválida";
                    return View("Login", user);
                }
            }
            else
            {
                user.Status = false;
                ViewBag.UsuarioInvalido = "Usuário Inválido";
                ViewBag.SenhaInvalido = "";
                return View("Login", user);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditarToque(ToqueModel toque)
        {
            if ((toque.IsSegunda) ||
                (toque.IsTerca) ||
                (toque.IsQuarta) ||
                (toque.IsQuinta) ||
                (toque.IsSexta) ||
                (toque.IsSabado) ||
                (toque.IsDomingo))
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
                    it.IsAtivo = item.IsAtivo;
                    it.NivelEnsino = item.NivelEnsino;
                    it.UltimoToque = item.UltimoToque;
                    it.StartSegs = item.StartSegs;

                    toquesE.Add(it);
                }
                string arquivoId = "";
                foreach (var item in toques)
                {
                    if (item.Arquivo == toque.Arquivo)
                    {
                        toquesE[i].Arquivo = toque.Arquivo;
                        toquesE[i].Nome = toque.Nome;
                        toquesE[i].Hora = toque.Hora;
                        toquesE[i].IsSegunda = toque.IsSegunda;
                        toquesE[i].IsTerca = toque.IsTerca;
                        toquesE[i].IsQuarta = toque.IsQuarta;
                        toquesE[i].IsQuinta = toque.IsQuinta;
                        toquesE[i].IsSexta = toque.IsSexta;
                        toquesE[i].IsSabado = toque.IsSabado;
                        toquesE[i].IsDomingo = toque.IsDomingo;
                        toquesE[i].IsAtivo = toque.IsAtivo;
                        toquesE[i].NivelEnsino = toque.NivelEnsino;
                        toquesE[i].StartSegs = toque.StartSegs;
                        toquesE[i].UltimoToque = toque.UltimoToque;

                        arquivoId = toque.Arquivo;

                        if (toque.NivelEnsino == 2 || toque.NivelEnsino == 3)
                            toquesE[i].Canal = 1;
                        else
                            toquesE[i].Canal = 2;

                        break;
                    }
                    i++;
                }

                string pathMusica = "";

                foreach (var file in toque.fileupload)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var nameType = file.FileName.ToString().Split('.');
                        pathMusica = Path.Combine(Server.MapPath("~/Musicas"), (arquivoId.ToString() + "." + nameType[1]));

                        file.SaveAs(pathMusica);

                    }
                }

                string dir = Path.Combine(Server.MapPath("~/CSV/toque.csv"));
                ServiceCSV.WriteCSVFileToque(dir, toquesE);

                await FTPService.UploadFile(dir);
                await FTPService.UploadFile(pathMusica);

                return RedirectToAction("Toques", true);
            }
            else
            {
                return RedirectToAction("EditarToque", toque);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Toque(CadastroModel cadastro)
        {
            List<ToqueModel> toques = LerToquesCSV();
            List<ToqueExportModel> toquesE = new List<ToqueExportModel>();
            if ((cadastro.IsSegunda) ||
                (cadastro.IsTerca) ||
                (cadastro.IsQuarta) ||
                (cadastro.IsQuinta) ||
                (cadastro.IsSexta) ||
                (cadastro.IsSabado) ||
                (cadastro.IsDomingo))
            {
                int arquivoId = 0;

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
                    it.IsAtivo = item.IsAtivo;
                    it.NivelEnsino = item.NivelEnsino;
                    it.UltimoToque = item.UltimoToque;
                    it.StartSegs = item.StartSegs;

                    arquivoId = Convert.ToInt32(it.Arquivo);
                    toquesE.Add(it);
                }

                arquivoId += 1;

                ToqueExportModel toque = new ToqueExportModel();
                toque.Arquivo = arquivoId.ToString();
                toque.Nome = cadastro.Nome;
                toque.Hora = cadastro.Hora;
                toque.IsAtivo = true;
                toque.NivelEnsino = cadastro.Ensino;
                toque.IsSegunda = cadastro.IsSegunda;
                toque.IsTerca = cadastro.IsTerca;
                toque.IsQuarta = cadastro.IsQuarta;
                toque.IsQuinta = cadastro.IsQuinta;
                toque.IsSexta = cadastro.IsSexta;
                toque.IsSabado = cadastro.IsSabado;
                toque.IsDomingo = cadastro.IsDomingo;
                toque.UltimoToque = null;
                toque.StartSegs = cadastro.StartSegs;

                if (cadastro.Ensino == 2 || cadastro.Ensino == 3)
                    toque.Canal = 1;
                else
                    toque.Canal = 2;

                toquesE.Add(toque);

                string pathMusica = "";

                foreach (var file in cadastro.fileupload)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var nameType = file.FileName.ToString().Split('.');
                        pathMusica = Path.Combine(Server.MapPath("~/Musicas"), (arquivoId.ToString() + "." + nameType[1]));

                        file.SaveAs(pathMusica);

                    }
                }

                string dir = Path.Combine(Server.MapPath("~/CSV/toque.csv"));
                ServiceCSV.WriteCSVFileToque(dir, toquesE);

                await FTPService.UploadFile(dir);
                await FTPService.UploadFile(pathMusica);

                return RedirectToAction("Toques", true);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult RedirectCadastro()
        {
            return RedirectToAction("Toque", true);
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
                    break;
                }
            }

            ViewBag.StartSegs = toque.StartSegs;
            ViewBag.Ensino = toque.NivelEnsino;

            return View(toque);
        }

        public async Task<ActionResult> Login(UserModel user)
        {
            await SyncRasp();
            ViewBag.UsuarioInvalido = "";
            ViewBag.SenhaInvalido = "";
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Detalhes(string arquivo)
        {
            string arq = arquivo;
            List<ToqueModel> toques = LerToquesCSV();
            ToqueModel toque = new ToqueModel();
            foreach (var item in toques)
            {
                if (arq == item.Arquivo)
                {
                    toque = item;
                    break;
                }
            }

            return View(toque);

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
                if (pla.IsSegunda)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Segunda ";
                }
                if (pla.IsTerca)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Terça ";
                }
                if (pla.IsQuarta)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Quarta ";
                }
                if (pla.IsQuinta)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Quinta ";
                }
                if (pla.IsSexta)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Sexta ";
                }
                if (pla.IsSabado)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Sabado ";
                }
                if (pla.IsDomingo)
                {
                    pla.DiaSemana = pla.DiaSemana + "- Domingo ";
                }
                if (!pla.IsSegunda && !pla.IsTerca && !pla.IsQuarta && !pla.IsQuinta && !pla.IsSexta && !pla.IsSabado && !pla.IsDomingo)
                {
                    pla.DiaSemana = "- Nenhum dia selecionado";
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

        private async Task<int> SyncRasp()
        {
            string dir = Path.Combine(Server.MapPath("~/CSV/"));
            await FTPService.DownloadFile(dir);
            return 1;
        }

    }
}