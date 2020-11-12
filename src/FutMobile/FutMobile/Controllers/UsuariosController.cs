using FutMobile.Context;
using FutMobile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace FutMobile.Controllers
{
    public class UsuariosController : ApiController
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

        public class ModeloUsuario
        {
            public ModeloUsuario() { }

            public string Email { get; set; }

            public string UserName { get; set; }

            public string Login { get; set; }
        }

        // GET: Postagens
        [HttpGet]
        [Route("api/usuarios/{token}")]
        public List<ModeloUsuario> Get([FromUri] string token)
        {
            ApplicationUser user = null;
            try
            {
                user = UserManager.Users.First(x => x.SecurityStamp == token);

                var users = UserManager.Users.ToList();

                List<ModeloUsuario> listadeusuarios = new List<ModeloUsuario>();

                foreach(var usuario in users)
                {
                    ModeloUsuario novo = new ModeloUsuario();
                    novo.Email = usuario.Email;
                    novo.Login = usuario.Login;
                    novo.UserName = usuario.UserName;
                    listadeusuarios.Add(novo);
                }

                var inrole = UserManager.IsInRole(user.Id, "Administrator");

                if (inrole) return listadeusuarios;

                else
                {
                    List<ModeloUsuario> listadeusuarios2 = new List<ModeloUsuario>();

                    ModeloUsuario novo2 = new ModeloUsuario();
                    novo2.Email = "Usuario não autorizado";
                    novo2.Login = "Usuario não autorizado";
                    novo2.UserName = "Usuario não autorizado";
                    listadeusuarios2.Add(novo2);

                    return listadeusuarios2;
                }
            }

            catch
            {
                List<ModeloUsuario> listadeusuarios = new List<ModeloUsuario>();

                ModeloUsuario novo = new ModeloUsuario();
                novo.Email = "Chave de API Inválida";
                novo.Login = "Chave de API Inválida";
                novo.UserName = "Chave de API Inválida";
                listadeusuarios.Add(novo);

                return listadeusuarios;
            }
        }
    }
}