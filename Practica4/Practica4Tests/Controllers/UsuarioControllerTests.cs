using Practica4.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica4.Models;
using System.Web.Mvc;

namespace Practica4.Controllers.Tests
{
    [TestClass()]
    public class UsuarioControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            UsuarioController uc = new UsuarioController();
            Login log = new Login()
            {
                codigo = 1,
                usua = "jonna123",
                pass = "1234"
            };

            var result = uc.Login(log) as ViewResult;

            Assert.AreEqual("AdminInd", result.ViewName);
        }

        [TestMethod()]
        public void TransferenciaTest()
        {
            UsuarioController uc = new UsuarioController();
            Transferencia trans = new Transferencia()
            {
                monto = 100,
                cuenta1 = 1000,
                cuenta2 = 1001
            };

            var result = uc.Transferencia(trans) as ViewResult;

            Assert.AreEqual("TransExito", result.ViewName);
        }

        [TestMethod()]
        public void LogoutTest()
        {
            UsuarioController uc = new UsuarioController();
            var result = uc.Logout() as ViewResult;

            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            UsuarioController uc = new UsuarioController();
            usuario crear = new usuario()
            {
                nombre = "Pedro",
                apellido = "Martinez",
                correo = "pedro@gmail.com",
                usua = "peter123",
                pass = "1234"
            };

            var result = uc.Create(crear) as ViewResult;

            Assert.AreEqual("Info", result.ViewName);
        }

        [TestMethod()]
        public void AdmCreateTest()
        {
            UsuarioController uc = new UsuarioController();
            usuario crear = new usuario()
            {
                nombre = "Jose",
                apellido = "Herrera",
                correo = "jose@gmail.com",
                usua = "jose123",
                pass = "1234"
            };

            var result = uc.AdmCreate(crear) as ViewResult;

            Assert.AreEqual("AdminInd", result.ViewName);
        }

        [TestMethod()]
        public void EditTest()
        {
            UsuarioController uc = new UsuarioController();
            usuario crear = new usuario()
            {
                codigo = 3,
                nombre = "Jose",
                apellido = "Herrera",
                correo = "jose@gmail.com",
                usua = "jose123",
                pass = "1234"
            };

            var result = uc.Edit(crear) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            UsuarioController uc = new UsuarioController();

            var result = uc.Delete(2) as ViewResult;

            Assert.AreEqual("AdminInd", result.ViewName);
        }
    }
}