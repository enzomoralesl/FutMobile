using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FutMobile.Context;
using FutMobile.Models;
using Microsoft.AspNet.Identity;

namespace FutMobile.Controllers
{
    [Authorize]
    public class CampeonatosController : Controller
    {
        private ContextoCampeonato db = new ContextoCampeonato();

        // GET: Campeonatos
        public ActionResult Index()
        {
            return View(db.Campeonato.ToList());
        }

        // GET: Campeonatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonato.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        public ActionResult AwaitingApproval()
        {
            return View();
        }

        // GET: Campeonatos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campeonatos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(campeonato.ImageFile.FileName);
                string extension = Path.GetExtension(campeonato.ImageFile.FileName);
                fileName = fileName + extension;
                campeonato.SimboloCampPath = "../Utilities/LogosCamps/" + fileName;

                fileName = Path.Combine(Server.MapPath("../Utilities/LogosCamps/"), fileName);

                campeonato.ImageFile.SaveAs(fileName);

                if (!User.IsInRole("Administrator")) campeonato.NomeCamp = "Sugestao - " + campeonato.NomeCamp + " - by " + User.Identity.Name;

                db.Campeonato.Add(campeonato);
                db.SaveChanges();
                if (User.IsInRole("Administrator")) return RedirectToAction("Index");
                else return RedirectToAction("AwaitingApproval");
            }

            return View(campeonato);
        }

        // GET: Campeonatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonato.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        // POST: Campeonatos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Administrator"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(campeonato.ImageFile.FileName);
                    string extension = Path.GetExtension(campeonato.ImageFile.FileName);
                    fileName = fileName + extension;
                    campeonato.SimboloCampPath = "../../Utilities/LogosCamps/" + fileName;

                    fileName = Path.Combine(Server.MapPath("../../Utilities/LogosCamps/"), fileName);

                    campeonato.ImageFile.SaveAs(fileName);

                    db.Entry(campeonato).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(campeonato.ImageFile.FileName);
                    string extension = Path.GetExtension(campeonato.ImageFile.FileName);
                    fileName = fileName + extension;
                    campeonato.SimboloCampPath = "../../Utilities/LogosCamps/" + fileName;

                    fileName = Path.Combine(Server.MapPath("../../Utilities/LogosCamps/"), fileName);

                    campeonato.ImageFile.SaveAs(fileName);

                    IdentityMessage message = new IdentityMessage();

                    string SugestaoResumo = campeonato.ResumoCamp.Replace(Environment.NewLine, "<br />");
                    string SugestaoArtilheiros = campeonato.PrincipaisArtilheiros.Replace(Environment.NewLine, "<br />");

                    message.Body = "Caminho do novo logo: " + campeonato.SimboloCampPath + "<br />Primeira Edicao: " + campeonato.PrimeiraEdicao + "<br />Resumo: " + SugestaoResumo + "<br />Principais Artilheiros: " + SugestaoArtilheiros;
                    message.Destination = "marcelopts151@gmail.com";
                    message.Subject = "Sugestão de alteração no campeonato " + campeonato.NomeCamp + " - de " + User.Identity.Name;

                    EmailService emailService = new EmailService();

                    await emailService.SendAsync(message);

                    return RedirectToAction("AwaitingApproval");
                }
            }
            return View(campeonato);
        }

        // GET: Campeonatos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonato.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Administrator")) return View(campeonato);

            else
            {
                return RedirectToAction("EspecificaMotivo", "Campeonatos", new { campeonato = campeonato.NomeCamp });
            }
        }

        // GET
        public ActionResult EspecificaMotivo(string campeonato)
        {
            MotivoModel motivo = new MotivoModel();
            motivo.Nome = campeonato;

            return View(motivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EspecificaMotivo(MotivoModel motivo)
        {
            if (ModelState.IsValid)
            {
                IdentityMessage message = new IdentityMessage();

                string MotivoFormatado = motivo.MotivoDaRemocao.Replace(Environment.NewLine, "<br />");

                message.Body = "Sugiro remoção deste campeonato.<br />Motivo: " + motivo.MotivoDaRemocao;
                message.Destination = "marcelopts151@gmail.com";
                message.Subject = "Sugestão de remoção no campeonato " + motivo.Nome + " - de " + User.Identity.Name;

                EmailService emailService = new EmailService();

                await emailService.SendAsync(message);


                return RedirectToAction("AwaitingApproval");
            }

            else return View(motivo);
        }

        // POST: Campeonatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campeonato campeonato = db.Campeonato.Find(id);
            db.Campeonato.Remove(campeonato);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
