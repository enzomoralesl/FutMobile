using FutMobile.Context;
using FutMobile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FutMobile.Controllers
{
    public class PostagensController : ApiController
    {
        private Contexto db = new Contexto();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Postagens
        [HttpGet]
        [Route("api/postagens/{token}")]
        public List<Post> Get([FromUri] string token)
        {
            try
            {
                var user = UserManager.Users.First(x => x.SecurityStamp == token);

                var posts = db.Post.ToList();
                return posts;
            }

            catch
            {
                var posts = db.Post.ToList();
                posts.Clear();
                Post post = new Post();
                post.Codigo = 0;
                post.Categoria = "Chave API inválida!";
                post.Likes = "Chave API inválida!";
                post.Msg = "Chave API inválida!";
                post.Nome = "Chave API inválida!";
                post.UserLogin = "Chave API inválida!";
                post.Titulo = "Chave API inválida!";
                posts.Add(post);
                return posts;
            }
        }

        [HttpPost]
        [Route("api/postagens/nova/{token}/{titulo}/{categoria}/{mensagem}")]
        public HttpResponseMessage CriarNovaPostagem([FromUri] string token, [FromUri] string titulo, [FromUri] string categoria, [FromUri] string mensagem)
        {
            try
            {
                var user = UserManager.Users.First(x => x.SecurityStamp == token);

                Post MyPost = new Post();

                MyPost.Titulo = titulo;
                MyPost.Categoria = categoria;
                MyPost.Msg = mensagem;
                MyPost.UserLogin = user.Login;
                MyPost.Nome = user.UserName;
                MyPost.Likes = "";
                MyPost.Data = DateTime.Now;
                db.Post.Add(MyPost);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Postagem criada com sucesso!");
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.Created, "Chave de API Inválida");
            }
        }

        [HttpPut]
        [Route("api/postagens/editar/{token}/{codigo}/{titulo}/{categoria}/{mensagem}")]
        public HttpResponseMessage EditarPostagem([FromUri] string token, [FromUri] int codigo, [FromUri] string titulo, [FromUri] string categoria, [FromUri] string mensagem)
        {
            try
            {
                var user = UserManager.Users.First(x => x.SecurityStamp == token);

                Post currentPost = db.Post.Find(codigo);

                if (currentPost != null)
                {
                    if (user.UserName == currentPost.Nome || UserManager.IsInRole(user.Id, "Administrator"))
                    {
                        currentPost.Categoria = categoria;
                        currentPost.Titulo = titulo;
                        currentPost.Msg = mensagem;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.Created, "Postagem editada com sucesso!");
                    }

                    else return Request.CreateErrorResponse(HttpStatusCode.Created, "Usuario não autorizado a editar o post");
                }
                else return Request.CreateErrorResponse(HttpStatusCode.Created, "Post não existe");
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.Created, "Chave de API Inválida");
            }
        }

        [HttpDelete]
        [Route("api/postagens/remover/{token}/{codigo}")]
        public HttpResponseMessage DeletarPostagem([FromUri] string token, [FromUri] int codigo)
        {
            try
            {
                var user = UserManager.Users.First(x => x.SecurityStamp == token);

                Post post = db.Post.Find(codigo);

                if (post != null)
                {
                    if (user.UserName == post.Nome || UserManager.IsInRole(user.Id, "Administrator"))
                    {
                        db.Post.Remove(post);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.Created, "Postagem removida com sucesso!");
                    }

                    else return Request.CreateErrorResponse(HttpStatusCode.Created, "Usuario não autorizado a remvoer este post");
                }
                else return Request.CreateErrorResponse(HttpStatusCode.Created, "Post não existe");
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.Created, "Chave de API Inválida");
            }
        }

        // ADD LIKE TO DO!
        // COMENTAR TO DO!
    }
}