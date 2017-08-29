using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class IngredientesController : Controller
    {
        #region .: global :.
        
        private ContextDB db = new ContextDB();

        #endregion

        #region .: Actions :.
        
        // GET: Ingredientes
        public ActionResult Index()
        {
            return View(db.Ingredientes.ToList());
        }

        // GET: Ingredientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingredientes.Find(id);
            if (ingrediente == null)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIngrediente,IngredienteNome,Valor")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                db.Ingredientes.Add(ingrediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingrediente);
        }

        // GET: Ingredientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingredientes.Find(id);
            if (ingrediente == null)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingredientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIngrediente,IngredienteNome,Valor")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingrediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingredientes.Find(id);
            if (ingrediente == null)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingrediente ingrediente = db.Ingredientes.Find(id);
            db.Ingredientes.Remove(ingrediente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region .: Methods :.
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
