using System.Linq;
using System.Web.Mvc;
using Practica4.Models;

namespace Practica4.Controllers
{
    public class MovimientoController:Controller
	{
	   private banco_practica_4Entities db = new banco_practica_4Entities();
	   
	     //
        // GET: /movimiento/

        public ActionResult Index()
        {
            return View(db.movimiento.ToList());
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Create([Bind(Include = "Monto,mov,fecha,cuentaUno,cuentaDos")] movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.movimiento.Add(movimiento);
                db.SaveChanges();
                return true;
            }

            //ViewBag.usua = new SelectList(db.usuario, "codigo", "nombre", cuenta.usua);
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
	}
}
