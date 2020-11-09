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
    public class TimesController : Controller
    {
        private ContextoTime db = new ContextoTime();

        // GET: Times
        public ActionResult Index()
        {
            return View(db.Time.ToList());
        }

        // GET: Times/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Time.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        public ActionResult AwaitingApproval()
        {
            return View();
        }

        // GET: Times/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Times/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Time time)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(time.ImageFile.FileName);
                string extension = Path.GetExtension(time.ImageFile.FileName);
                fileName = fileName + extension;
                time.BrasaoPath = "../Utilities/LogosTimes/" + fileName;

                fileName = Path.Combine(Server.MapPath("../Utilities/LogosTimes/"), fileName);

                time.ImageFile.SaveAs(fileName);

                if (!User.IsInRole("Administrator")) time.NomeTime = "Sugestao - " + time.NomeTime + " - by " + User.Identity.Name;

                db.Time.Add(time);
                db.SaveChanges();
                if (User.IsInRole("Administrator")) return RedirectToAction("Index");
                else return RedirectToAction("AwaitingApproval");
            }

            return View(time);
        }

        // GET: Times/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Time.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        // POST: Times/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Time time)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Administrator"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(time.ImageFile.FileName);
                    string extension = Path.GetExtension(time.ImageFile.FileName);
                    fileName = fileName + extension;
                    time.BrasaoPath = "../../Utilities/LogosTimes/" + fileName;

                    fileName = Path.Combine(Server.MapPath("../../Utilities/LogosTimes/"), fileName);

                    time.ImageFile.SaveAs(fileName);

                    db.Entry(time).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(time.ImageFile.FileName);
                    string extension = Path.GetExtension(time.ImageFile.FileName);
                    fileName = fileName + extension;
                    time.BrasaoPath = "../../Utilities/LogosTimes/" + fileName;

                    fileName = Path.Combine(Server.MapPath("../../Utilities/LogosTimes/"), fileName);

                    time.ImageFile.SaveAs(fileName);

                    IdentityMessage message = new IdentityMessage();

                    string SugestaoResumo = time.Resumo.Replace(Environment.NewLine, "<br />");
                    string SugestaoResumoTitulos = time.ResumoTitulos.Replace(Environment.NewLine, "<br />");

                    message.Body = "Caminho do novo escudo: " + time.BrasaoPath + "<br />Fundação: " + @String.Format("{0:dd/MM/yyyy}", time.Fundacao) + "<br />Resumo: " + SugestaoResumo + "<br />Resumo dos títulos: " + SugestaoResumoTitulos + "<br />Número de Mundiais: " + time.Mundial + "<br />Número de Libertadores: " + time.Libertadores + "<br />Número de Campeonatos Brasileiros: " + time.Brasileiro + "<br />Número de Copa do Brasil: "+ time.CopaDoBrasil + "<br />Número de campeonatos estaduais: " + time.Estadual;
                    message.Destination = "marcelopts151@gmail.com";
                    message.Subject = "Sugestão de alteração no time " + time.NomeTime + " - de " + User.Identity.Name;

                    EmailService emailService = new EmailService();

                    await emailService.SendAsync(message);

                    return RedirectToAction("AwaitingApproval");
                }
            }
            return View(time);
        }

        // GET: Times/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Time.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Administrator")) return View(time);

            else
            {
                return RedirectToAction("EspecificaMotivo", "Times", new { time = time.NomeTime });
            }
        }

        // GET
        public ActionResult EspecificaMotivo(string time)
        {
            MotivoModel motivo = new MotivoModel();
            motivo.Nome = time;

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
                message.Subject = "Sugestão de remoção no time " + motivo.Nome + " - de " + User.Identity.Name;

                EmailService emailService = new EmailService();

                await emailService.SendAsync(message);


                return RedirectToAction("AwaitingApproval");
            }

            else return View(motivo);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Time time = db.Time.Find(id);
            db.Time.Remove(time);
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
