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
    public class LanchesController : Controller
    {
        #region .: Global :.
        
        private ContextDB db = new ContextDB();

        #endregion

        #region .: Actions :.
        
        public ActionResult Index()
        {
            return View(db.Lanches.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = db.Lanches.Find(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLanche,LancheNome")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Lanches.Add(lanche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lanche);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = db.Lanches.Find(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLanche,LancheNome")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lanche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lanche);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = db.Lanches.Find(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lanche lanche = db.Lanches.Find(id);
            db.Lanches.Remove(lanche);
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
