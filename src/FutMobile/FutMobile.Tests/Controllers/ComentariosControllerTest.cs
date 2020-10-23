using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    class ComentariosControllerTest
    {
        [TestMethod]
        public void CreateViewTest()
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
