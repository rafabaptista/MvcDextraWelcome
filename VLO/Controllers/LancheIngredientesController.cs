using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class LancheIngredientesController : Controller
    {
        #region .: Global :.
        
        private ContextDB db = new ContextDB();

        #endregion

        #region .: Actions :.
        
        public ActionResult Index()
        {
            var ingredientes = db.Ingredientes.Select(ing => new
            {
                IdIngrediente = ing.IdIngrediente,
                IngredienteNome = ing.IngredienteNome
            }).ToList();

            var lanches = db.Lanches.Select(lan => new
            {
                IdLanche = lan.IdLanche,
                LancheNome = lan.LancheNome
            }).ToList();

            ViewBag.Lanches = new SelectList(lanches, "IdLanche", "LancheNome");
            ViewBag.Ingredientes = new MultiSelectList(ingredientes, "IdIngrediente", "IngredienteNome");

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Lanche")] int? IdLanche, [Bind(Include = "IdIngrediente")] List<int> lstIdIngrediente)
        {
            if (ModelState.IsValid)
            {
                if (lstIdIngrediente != null)
                {
                    lstIdIngrediente.ForEach(delegate (int idIngrediente)
                    {
                        db.LancheIngredientes.Add(new LancheIngrediente { IdLanche = (int)IdLanche, IdIngrediente = idIngrediente });
                    });

                    
                }
                else
                {
                    db.LancheIngredientes.Add(new LancheIngrediente { IdLanche = (int)IdLanche, IdIngrediente = 0 });
                }

                db.SaveChanges();
                
                return RedirectToAction("Index", "Cardapio");
            }
            else
            {
                ViewBag.TheResult = false;
                ViewBag.ErrorMessage = "Verificar Campos";
            }

            return View();
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