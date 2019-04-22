using Practica4.Models;
using System.Linq;
using System.Web.Mvc;
namespace Practica4.Controllers
{
    public class RolController:Controller
	{
	   private banco_practica_4Entities db = new banco_practica_4Entities();
	   
	     //
        // GET: /rol/

        public ActionResult Index()
        {
            return View(db.rol.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
	}
}
