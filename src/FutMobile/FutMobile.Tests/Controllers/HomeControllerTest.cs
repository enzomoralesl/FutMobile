using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void AboutViewTest()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void To_view_About_page_the_user_must_be_authorized()
        {
            var action = new HomeController().Action(c => c.About());
            var result = action.GetAuthorizationResult();
            
            //Usuário precisa estar autorizado para acessar esta página
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ContactViewTest()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.AreEqual("Contact", result.ViewName);
        }
    }
}