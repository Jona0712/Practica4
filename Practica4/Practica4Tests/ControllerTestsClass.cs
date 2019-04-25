using NUnit.Framework;
using Practica4.Controllers;
using System.Web.Mvc;
using Practica4.Models;

namespace Practica4Tests
{
    [TestFixture]
    public class ControllerTestsClass
    {

        [Test]
        public void TestUserLogin()
        {
            var obj = new UsuarioController();
            var result = obj.Login() as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }
    }
}
