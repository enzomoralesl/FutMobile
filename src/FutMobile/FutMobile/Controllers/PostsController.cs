using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FutMobile.Context;
using FutMobile.Models;
using Microsoft.AspNet.Identity;

namespace FutMobile.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private Contexto db = new Contexto();

        public ContextoComentario dbCom = new ContextoComentario();

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Post.ToList());
        }

        /*
        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        */

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            TempData["IDPost"] = id;

            return View(dbCom.Comentario.ToList());
        }

        // Add/Remove Like
        public ActionResult AddLike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                List<string> ListaDeLikes = post.Likes.Split(',').ToList();
                for(int i=0; i < ListaDeLikes.Count; i++)
                {
                    if (ListaDeLikes[i] == User.Identity.Name)
                    {
                        if(post.Likes == ListaDeLikes[i])
                        {
                            post.Likes = post.Likes.Replace(ListaDeLikes[i], "");
                            db.Entry(post).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
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
                            post.Likes = post.Likes.Replace(toRemove, "");
                            db.Entry(post).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }

                if(post.Likes == "") post.Likes = User.Identity.Name;
                else post.Likes = post.Likes + "," + User.Identity.Name;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,UserLogin,Categoria,Data,Titulo,Msg,Nome")] Post post)
        {
            if (ModelState.IsValid)
            {
                string mensagemeditada = post.Msg.Replace(Environment.NewLine, "<br />");

                post.Msg = mensagemeditada;

                post.UserLogin = User.Identity.Name; // Adiciona o login, e na view mostra o nome
                post.Data = DateTime.Now;
                post.Nome = ((ClaimsIdentity)User.Identity).FindFirst("FullName").ToString().Replace("FullName: ", "");
                post.Likes = "";
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            post.Msg = post.Msg.Replace("<br />", Environment.NewLine);

            return View(post);
        }

        public ActionResult AddComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Create", "Comentarios", new { id = id});
        }

        // POST: Posts/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Categoria,Titulo,Msg")] Post post)
        {
            if (ModelState.IsValid)
            {
                string mensagemeditada = post.Msg.Replace(Environment.NewLine, "<br />");

                post.Msg = mensagemeditada;

                var currentPost = db.Post.Find(post.Codigo);
                currentPost.Categoria = post.Categoria;
                currentPost.Titulo = post.Titulo;
                currentPost.Msg = post.Msg;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
