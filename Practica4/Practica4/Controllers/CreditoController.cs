using Practica4.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Practica4.Controllers
{
    public class CreditoController : Controller
    {
        string a;
        private banco_practica_4Entities db = new banco_practica_4Entities();

        //
        // GET: /credito/

        public ActionResult Index()
        {
            if (Session["codigo"] != null)
            {
                a = "s";
                var credito = db.credito.Where(i => i.estado == a);
                return View(credito.ToList());
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET:
        public ActionResult Historial()
        {
            if (Session["codigo"] != null)
            {
                ViewBag.aceptado = "a";
                ViewBag.pendiente = "s";
                ViewBag.recha = "r";
                //var credito = db.credito.Where(i => i.estado == a);
                return View(db.credito.ToList());
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: creditoes/Details/5
        public ActionResult Details(int? id, bool si)
        {
            if (Session["codigo"] != null)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                credito credi = db.credito.Find(id);
                if (credi == null)
                {
                    return HttpNotFound();
                }
                if (si)
                {
                    cuenta cuentica = db.cuenta.Find(credi.cuenta);
                    if (cuentica == null)
                    {
                        return HttpNotFound();
                    }
                    cuentica.Saldo = cuentica.Saldo + credi.Monto;
                    a = "a"; //aceptado
                    credi.estado = a;
                    db.Entry(cuentica).State = EntityState.Modified;
                    db.Entry(credi).State = EntityState.Modified;
                    db.SaveChanges();

                    MovimientoController mc = new MovimientoController();
                    movimiento movi = new movimiento();
                    movi.Monto = credi.Monto;
                    movi.mov = "C";
                    movi.fecha = DateTime.Now;
                    movi.cuentaUno = cuentica.Numero;
                    movi.cuentaDos = null;

                    if (mc.Create(movi))
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    a = "r"; //rechazada
                    credi.estado = a;
                    db.Entry(credi).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: creditoes/Create
        public ActionResult Create()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: creditoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Monto,Descripcion,estado,cuenta")] credito credito)
        {
            a = "s"; // 's' sin resolver
            credito.estado = a;
            if (ModelState.IsValid)
            {
                db.credito.Add(credito);
                db.SaveChanges();
                return RedirectToAction("Index", "Usuario");
            }
            return RedirectToAction("Index");
        }

        // GET: creditoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero", credito.cuenta);
            return View(credito);
        }

        // POST: creditoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,Monto,Descripcion,estado,cuenta")] credito credito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero", credito.cuenta);
            return View(credito);
        }

        // GET: creditoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            return View(credito);
        }

        // POST: creditoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            credito credito = db.credito.Find(id);
            db.credito.Remove(credito);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
