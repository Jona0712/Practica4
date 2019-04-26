using Practica4.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Practica4.Models;
using System;

namespace Practica4.Controllers.Tests
{
    [TestClass()]
    public class MovimientoControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            MovimientoController movi = new MovimientoController();
            var result = movi.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            MovimientoController movi = new MovimientoController();
            movimiento mov = new movimiento() {
                Monto = 1200,
                mov = "T",
                fecha = DateTime.Now,
                cuentaUno = 1000,
                cuentaDos = 1001
            };
            var result = movi.Create(mov);

            Assert.IsNotNull(result);
        }
    }
}