using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppPrueba.Models;

namespace AppPrueba.Controllers
{
    public class registrosController : Controller
    {
        private TecnoHardDBEntities db = new TecnoHardDBEntities();

        // GET: registros
        public ActionResult Index()
        {
            var registros = db.registros.Include(r => r.categorias).Include(r => r.productos).Include(r => r.proveedores);
            return View(registros.ToList());
        }

        // GET: registros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registros registros = db.registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            return View(registros);
        }

        // GET: registros/Create
        public ActionResult Create()
        {
            ViewBag.IdCate = new SelectList(db.categorias, "IdCate", "NombreCate");
            ViewBag.IdProd = new SelectList(db.productos, "IdProd", "NombreProd");
            ViewBag.IdProv = new SelectList(db.proveedores, "IdProv", "nombreProv");
            return View();
        }

        // POST: registros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReg,IdProd,IdCate,IdProv")] registros registros)
        {
            if (ModelState.IsValid)
            {
                db.registros.Add(registros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCate = new SelectList(db.categorias, "IdCate", "NombreCate", registros.IdCate);
            ViewBag.IdProd = new SelectList(db.productos, "IdProd", "NombreProd", registros.IdProd);
            ViewBag.IdProv = new SelectList(db.proveedores, "IdProv", "nombreProv", registros.IdProv);
            return View(registros);
        }

        // GET: registros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registros registros = db.registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCate = new SelectList(db.categorias, "IdCate", "NombreCate", registros.IdCate);
            ViewBag.IdProd = new SelectList(db.productos, "IdProd", "NombreProd", registros.IdProd);
            ViewBag.IdProv = new SelectList(db.proveedores, "IdProv", "nombreProv", registros.IdProv);
            return View(registros);
        }

        // POST: registros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReg,IdProd,IdCate,IdProv")] registros registros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCate = new SelectList(db.categorias, "IdCate", "NombreCate", registros.IdCate);
            ViewBag.IdProd = new SelectList(db.productos, "IdProd", "NombreProd", registros.IdProd);
            ViewBag.IdProv = new SelectList(db.proveedores, "IdProv", "nombreProv", registros.IdProv);
            return View(registros);
        }

        // GET: registros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registros registros = db.registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            return View(registros);
        }

        // POST: registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registros registros = db.registros.Find(id);
            db.registros.Remove(registros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
