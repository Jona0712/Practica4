using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica4.Models;

namespace Practica4.Controllers.Tests
{
    [TestClass()]
    public class CuentaControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            CuentaController cc = new CuentaController();
            cuenta mov = new cuenta()
            {
                Numero = 1005,
                Saldo = 1000,
                usua = 3
            };
            var result = cc.Create(mov);

            Assert.IsNotNull(result);
        }
    }
}