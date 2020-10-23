using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    public class PostsControllerTest
    {
        [TestMethod]
        public void CreateViewTest()
        {
            var controller = new PostsController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void To_view_Posts_page_the_user_need_to_be_authorized()
        {
            var action = new PostsController().Action(c => c.Index());
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_Create_page_the_user_need_to_be_authorized()
        {
            var action = new PostsController().Action(c => c.Create());
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_AddComment_page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new PostsController().Action(c => c.AddComment(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_Delete_page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new PostsController().Action(c => c.Delete(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_Details_page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new PostsController().Action(c => c.Details(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_Add_like__page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new PostsController().Action(c => c.AddLike(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Posts_Edit__page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new PostsController().Action(c => c.Edit(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        // Comentarios Tests:
        [TestMethod]
        public void ComentariosCreateViewTest()
        {
            int qualquernumero = int.MaxValue;
            var controller = new ComentariosController();
            var result = controller.Create(qualquernumero) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void To_view_Comentarios_page_the_user_need_to_be_authorized()
        {
            var action = new ComentariosController().Action(c => c.Index());
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Comentarios_Edit__page_the_user_need_to_be_authorized()
        {
            int qualquernumero = int.MaxValue;
            var action = new ComentariosController().Action(c => c.Edit(qualquernumero));
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }
    }
}
