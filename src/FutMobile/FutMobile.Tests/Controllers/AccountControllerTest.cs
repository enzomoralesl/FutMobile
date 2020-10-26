using System.Web.Mvc;
using FutMobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xania.AspNet.Simulator;
using FluentAssertions;
using FutMobile.Models;
using System.Threading.Tasks;
using Moq;
using System.Web.Routing;
using System.Web;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Security.Claims;
using System.Collections.Generic;

namespace FutMobile.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
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

        [TestMethod]
        public async Task Test_if_not_registered_user_returns_model()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            ApplicationUserManager userManager = new ApplicationUserManager(userStoreMock.Object);
            //IAuthenticationManager authManager = AuthenticationManager.
            Mock<IAuthenticationManager> authMock = new Mock<IAuthenticationManager>();
            //Mock<IUserPasswordStore<TUser>> userPasswordStore = new Mock<IUserPasswordStore<TUser>>();
            //userPasswordStore.Setup(s => s.CreateAsync(It.IsAny<TUser>(), It.IsAny<CancellationToken>()))
            //.Returns(Task.FromResult(IdentityResult.Success));
            ApplicationSignInManager manager = new ApplicationSignInManager(userManager, authMock.Object);

            AccountController Controller = new AccountController(userManager, manager);
            Mock<AccountController> ControllerMock = new Mock<AccountController>();

            Mock<ApplicationUserManager> UserManagerMock = new Mock<ApplicationUserManager>();
            Mock<ApplicationSignInManager> AppSignInManagerMock = new Mock<ApplicationSignInManager>();

            LoginViewModel vm = new LoginViewModel()
            {
                UserName = "AnyUser",
                Password = "123456789",
                RememberMe = true
            };

            var result = await Controller.Login(vm, "http://google.com/") as ViewResult;

            // Deve retornar o modelo, pois o usuario não existe no sistema
            Assert.AreEqual(vm, result.ViewData.Model);
        }

        [TestMethod]
        public void Can_Register_A_New_User()
        {
            //Arrange
            var dummyUser = new ApplicationUser() { Id = "anyId1", UserName = "Ma151", Email = "qualqueremail@gmail.com", Login = "MarceloSilva" };
            var mockStore = new Mock<IUserStore<ApplicationUser>>();

            var claimStore = new Mock<IUserClaimStore<ApplicationUser>>();

            var userManager = new UserManager<ApplicationUser>(mockStore.Object);

            mockStore.Setup(x => x.CreateAsync(dummyUser))
                        .Returns(Task.FromResult(IdentityResult.Success));

            mockStore.Setup(x => x.FindByNameAsync(dummyUser.UserName))
                        .Returns(Task.FromResult(dummyUser));

            claimStore.Setup(x => x.AddClaimAsync(dummyUser, new Claim("FullName", dummyUser.Login)))
                .Returns(Task.FromResult(dummyUser));

            //Act
            Task <IdentityResult> tt = (Task<IdentityResult>)mockStore.Object.CreateAsync(dummyUser);
            var user = userManager.FindByName("Ma151");

            //Assert
            Assert.AreEqual("Ma151", user.UserName);
        }
    }
}