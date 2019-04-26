using Practica4.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Practica4.Models;

namespace Practica4.Controllers.Tests
{
    [TestClass()]
    public class CreditoControllerTests
    {
        [TestMethod()]
        public void HistorialTest()
        {
            CreditoController cc = new CreditoController();
            var result = cc.Historial() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            CreditoController cc = new CreditoController();
            var result = cc.Details(2, true) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            CreditoController cc = new CreditoController();
            credito cr = new credito() {
                Monto = 1200,
                Descripcion = "porque si",
                cuenta = 1000,
                estado = "s"
            };
            var result = cc.Create(cr) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
    }
}