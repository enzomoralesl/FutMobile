using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;
using FutMobile.Models;
using System.Threading.Tasks;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        /* ---------------------------On Hold (Needs Mocking)
        [TestMethod]
        public async Task LoginViewTest()
        {
            var controller = new AccountController();
            LoginViewModel modelo = new LoginViewModel();
            modelo.Password = "Teste1!";
            modelo.RememberMe = true;
            modelo.UserName = "Usuario";
            var result = await controller.Login(modelo, "") as ViewResult;

            result.ViewData.Model.Should().BeSameAs(modelo);
        }
        */

        [TestMethod]
        public void To_view_Login_page_the_user_dont_need_to_be_authorized()
        {
            var action = new AccountController().Action(c => c.Login("http://url.de.teste.com/"));
            var result = action.GetAuthorizationResult();

            //Usuário nao precisa estar autorizado para acessar esta página
            result.Should().BeNull();
        }

        [TestMethod]
        public void RegisterViewTest()
        {
            var controller = new AccountController();
            var result = controller.Register() as ViewResult;
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void To_view_Register_page_the_user_dont_need_to_be_authorized()
        {
            var action = new AccountController().Action(c => c.Register());
            var result = action.GetAuthorizationResult();

            //Usuário nao precisa estar autorizado para acessar esta página
            result.Should().BeNull();
        }
    }
}