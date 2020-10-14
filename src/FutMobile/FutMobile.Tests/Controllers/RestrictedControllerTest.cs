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
    }
}