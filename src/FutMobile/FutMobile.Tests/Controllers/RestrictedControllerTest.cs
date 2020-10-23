using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    public class RestrictedControllerTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            var controller = new RestrictedController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void AjudaViewTest()
        {
            var controller = new RestrictedController();
            var result = controller.Ajuda() as ViewResult;
            Assert.AreEqual("Ajuda", result.ViewName);
        }

        [TestMethod]
        public void To_view_Index_Restricted_page_the_user_need_to_be_authorized()
        {
            var action = new RestrictedController().Action(c => c.Index());
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void To_view_Ajuda_Restricted_page_the_user_need_to_be_authorized()
        {
            var action = new RestrictedController().Action(c => c.Ajuda());
            var result = action.GetAuthorizationResult();

            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }
    }
}