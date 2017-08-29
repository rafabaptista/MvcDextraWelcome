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
        private ContextDB db = new ContextDB();

        // GET: Lanches
        public ActionResult Index()
        {
            return View(db.Lanches.ToList());
        }

        // GET: Lanches/Details/5
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

        // GET: Lanches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lanches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Lanches/Edit/5
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

        // POST: Lanches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Lanches/Delete/5
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

        // POST: Lanches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lanche lanche = db.Lanches.Find(id);
            db.Lanches.Remove(lanche);
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
