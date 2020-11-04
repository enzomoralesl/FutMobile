using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FutMobile.Context;
using FutMobile.Models;

namespace FutMobile.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private ContextoComentario db = new ContextoComentario();

        // GET: Comentarios
        public ActionResult Index()
        {
            return View(db.Comentario.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }

            TempData["PostID"] = id;

            return View(comentario);
        }

        public ActionResult BackToComents(int? id)
        {
            var currentComment = db.Comentario.Find(id);
            return RedirectToAction("Details", "Posts", new { id = currentComment.CodigoDoPost });
        }

        // GET: Comentarios/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["PostID"] = id;

            return View();
        }

        // POST: Comentarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoCom,CodigoDoPost,UserLoginCom,DataCom,MsgCom,NomeCom,LikesCom")] Comentario comentario, int id)
        {
            if (ModelState.IsValid)
            {
                comentario.DataCom = DateTime.Now;
                comentario.UserLoginCom = User.Identity.Name;
                comentario.NomeCom = ((ClaimsIdentity)User.Identity).FindFirst("FullName").ToString().Replace("FullName: ", "");
                comentario.LikesCom = "";
                db.Comentario.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index", "Posts", new { id = comentario.CodigoDoPost });
            }

            return View(comentario);
        }

        // Add/Remove Like
        public ActionResult AddLike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario post = db.Comentario.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                List<string> ListaDeLikes = post.LikesCom.Split(',').ToList();
                for (int i = 0; i < ListaDeLikes.Count; i++)
                {
                    if (ListaDeLikes[i] == User.Identity.Name)
                    {
                        if (post.LikesCom == ListaDeLikes[i])
                        {
                            post.LikesCom = post.LikesCom.Replace(ListaDeLikes[i], "");
                            db.Entry(post).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index", "Posts", new { id = post.CodigoDoPost });
                        }
                        else
                        {
                            string toRemove;
                            if (i == 0)
                            {
                                toRemove = ListaDeLikes[i] + ",";
                            }
                            else
                            {
                                toRemove = "," + ListaDeLikes[i];
                            }
                            post.LikesCom = post.LikesCom.Replace(toRemove, "");
                            db.Entry(post).State = EntityState.Modified;
                            db.SaveChanges();
                            return  RedirectToAction("Index", "Posts", new { id = post.CodigoDoPost });
                        }
                    }
                }

                if (post.LikesCom == "") post.LikesCom = User.Identity.Name;
                else post.LikesCom = post.LikesCom + "," + User.Identity.Name;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Posts", new { id = post.CodigoDoPost });
            }
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }

            TempData["PostID"] = id;

            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoCom,MsgCom")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                var currentComment = db.Comentario.Find(comentario.CodigoCom);
                currentComment.MsgCom = comentario.MsgCom;
                db.SaveChanges();
                return RedirectToAction("Index", "Posts", new { id = currentComment.CodigoDoPost });
            }
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentario.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }

            TempData["PostID"] = id;

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentario.Find(id);
            db.Comentario.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index", "Posts", new { id = comentario.CodigoDoPost });
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
