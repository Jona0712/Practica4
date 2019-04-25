using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Practica4.Models;

namespace Practica4.Controllers
{
    public class UsuarioController : Controller
    {
        private banco_practica_4Entities db = new banco_practica_4Entities();

        public ActionResult AdminInd()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.codigo = usu.codigo.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        //Get 
        public ActionResult Transferencia()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                cuenta cu = db.cuenta.Where(s => s.usua == usu.codigo).First();
                ViewBag.saldo = cu.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transferencia(Transferencia model)
        {
            if (ModelState.IsValid)
            {
                cuenta c = db.cuenta.Find(model.cuenta2);
                if (c != null)
                {
                    cuenta saldo = db.cuenta.Find(model.cuenta1);
                    if (model.monto <= saldo.Saldo)
                    {
                        c.Saldo = c.Saldo + model.monto;
                        saldo.Saldo = saldo.Saldo - model.monto;
                        db.Entry(c).State = EntityState.Modified;
                        db.Entry(saldo).State = EntityState.Modified;
                        db.SaveChanges();

                        MovimientoController mc = new MovimientoController();
                        movimiento movi = new movimiento();
                        movimiento m2 = new movimiento();
                        movi.Monto = model.monto;
                        movi.mov = "T";
                        movi.fecha = DateTime.Now;
                        movi.cuentaUno = model.cuenta1;
                        movi.cuentaDos = model.cuenta2;
                        m2.Monto = model.monto;
                        m2.mov = "R";
                        m2.fecha = DateTime.Now;
                        m2.cuentaUno = model.cuenta2;
                        m2.cuentaDos = model.cuenta1;

                        mc.Create(movi);
                        mc.Create(m2);

                        return RedirectToAction("TransExito");
                    }
                    else
                    {
                        ViewBag.msg = "SF";//sin fondos
                    }
                }
                else
                {
                    ViewBag.msg = "CI";//cuenta incorrecta
                }

                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                cuenta cu = db.cuenta.Where(s => s.usua == usu.codigo).First();
                ViewBag.saldo = cu.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult TransExito()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                cuenta cu = db.cuenta.Where(s => s.usua == usu.codigo).First();
                ViewBag.saldo = cu.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        //
        // GET: /usuario/

        public ActionResult Index()
        {
            //return View(db.usuario.ToList());
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET:
        public ActionResult Usuarios()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.codigo = usu.codigo.ToString();
                return View(db.usuario.ToList());
            }
            return RedirectToAction("Login");
        }

        // GET: usuarios
        public ActionResult Info(int codigo)
        {
            if (codigo > 0)
            {
                usuario usu = db.usuario.Find(codigo);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //get
        public ActionResult Login()
        {
            if (Session["codigo"] != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                usuario usuario = db.usuario.Find(model.codigo);
                if (usuario != null && model.usua == usuario.usua && model.pass == usuario.pass)
                {
                    Session["codigo"] = usuario.codigo;

                    if (usuario.rol == 2)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("AdminInd");
                    }
                }
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["codigo"] = null;
            return RedirectToAction("Login");
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            usuario.rol = 2;
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                CuentaController cuenCon = new CuentaController();
                cuenta cuenti = new cuenta();
                cuenti.Saldo = 1000;
                int codi = db.usuario.Where(i => i.usua == usuario.usua).First().codigo;
                cuenti.usua = codi;
                MovimientoController mcc = new MovimientoController();
                movimiento movi = new movimiento();

                if (cuenCon.Create(cuenti)) { 
                    
                    movi.Monto = 1000;
                    movi.mov = "I";
                    movi.fecha = DateTime.Now;

                    usuario usu = db.usuario.Find(codi);
                    movi.cuentaUno = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero;
                    movi.cuentaDos = null;

                    mcc.Create(movi);

                    return RedirectToAction("Info", new { codigo = codi });
                }
            }

            return View(usuario);
        }

        // GET: consulta saldo de cuenta
        public ActionResult Consultar()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                cuenta cuentica = db.cuenta.Where(i => i.usua == usu.codigo).First();
                ViewBag.cuenta = cuentica.Numero.ToString();
                ViewBag.saldo = cuentica.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET: crear admin
        public ActionResult AdmCreate()
        {
            if (Session["codigo"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        // POST: usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdmCreate([Bind(Include = "nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            usuario.rol = 1;
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();

                return RedirectToAction("AdminInd");
            }

            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["codigo"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            cuenta cuen = db.cuenta.Where(i => i.usua == usuario.codigo).First();
            List<debito> deb = db.debito.Where(a => a.cuenta == cuen.Numero).ToList();
            List<credito> cre = db.credito.Where(r => r.cuenta == cuen.Numero).ToList();

            if (deb != null)
            {
                foreach (var item in deb)
                {
                    db.debito.Remove(item);
                }
            }
            if (cre != null)
            {
                foreach (var item in cre)
                {
                    db.credito.Remove(item);
                }
            }

            db.cuenta.Remove(cuen);
            db.usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("AdminInd");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}