using Practica4.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Practica4.Models;

namespace Practica4.Controllers.Tests
{
    [TestClass()]
    public class DebitoControllerTests
    {
        [TestMethod()]
        public void AsignaTest()
        {
            DebitoController dc = new DebitoController();
            var result = dc.Asigna("2") as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DebitoController dc = new DebitoController();
            debito deb = new debito() {
                Monto = 200,
                Descripcion = "porque quiero",
                cuenta = 1000
            };
            var result = dc.Create(deb) as ViewResult;

            Assert.AreEqual("AdminInd", result.ViewName);
        }
    }
}