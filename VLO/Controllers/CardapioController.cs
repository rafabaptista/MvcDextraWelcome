using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class CardapioController : Controller
    {
        #region .: Global :.
        
        VLO.Models.ContextDB db = new Models.ContextDB();

        #endregion

        #region .: Actions :.
        
        // GET: Cardapio
        public ActionResult Index()
        {
            List<Cardapio> lstCardapio = GetCardapioWithPrice(GetAllCardapio());
            
            return View(lstCardapio);
        }
        
        // GET: Cardapio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cardapio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cardapio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Pedido(int? id)
        {
            Cardapio cardapio;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == id select li.IdIngrediente).ToList();

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            if (lstIdIngredientes.Count > 0)
            {
                cardapio = new Cardapio { IdLanche = (int)id, LancheNome = (from l in db.Lanches where l.IdLanche == id select l.LancheNome).FirstOrDefault() };
                if (cardapio == null)
                {
                    return HttpNotFound();
                }
            }
            else
                return HttpNotFound();

            ViewBag.Ingredientes = listIngredientes;
            Session["Ingredientes"] = listIngredientes;
            ViewBag.Cardapio = cardapio;

            return View(cardapio);
        }
        
        // POST: Ingredientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pedido([Bind(Include = "IdIngrediente,IngredienteNome")] int? id, int? IdIngrediente, string IngredienteNome)
        {
            Cardapio cardapio;
            
            List<Ingrediente> listIngredientes = (List < Ingrediente > )Session["Ingredientes"];


            Ingrediente ingrediente = (from i in db.Ingredientes where i.IdIngrediente == IdIngrediente select i).FirstOrDefault();

            listIngredientes.Add(ingrediente);


            if (listIngredientes.Count > 0)
            {
                cardapio = new Cardapio { IdLanche = (int)id, LancheNome = (from l in db.Lanches where l.IdLanche == id select l.LancheNome).FirstOrDefault() };
                if (cardapio == null)
                {
                    return HttpNotFound();
                }
            }
            else
                return HttpNotFound();

            ViewBag.Ingredientes = listIngredientes;
            Session["Ingredientes"] = listIngredientes;
            ViewBag.Cardapio = cardapio;
            
            return View(cardapio);
        }

        // GET: Cardapio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        // POST: Cardapio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region .: Methods :.

        private List<Cardapio> GetAllCardapio()
        {
            List<Cardapio> lstCardapio = new List<Cardapio>();

            var query = (from li in db.LancheIngredientes
                         join l in db.Lanches on li.IdLanche equals l.IdLanche
                         join i in db.Ingredientes on li.IdIngrediente equals i.IdIngrediente
                         let IdLancheIngrediente = li.IdLancheIngrediente
                         let IdLanche = li.IdLanche
                         let LancheNome = l.LancheNome
                         let IdIngrediente = li.IdIngrediente
                         let IngredienteNome = i.IngredienteNome
                         let Valor = i.Valor
                         select new { IdLancheIngrediente, IdLanche, LancheNome, IdIngrediente, IngredienteNome, Valor }).ToList().OrderBy(x => x.IdLanche); ;

            foreach (var obj in query)
            {
                lstCardapio.Add(new Cardapio
                {
                    IdLancheIngrediente = obj.IdLancheIngrediente,
                    IdLanche = obj.IdLanche,
                    LancheNome = obj.LancheNome,
                    IdIngrediente = obj.IdIngrediente,
                    IngredienteNome = obj.IngredienteNome,
                    Valor = obj.Valor
                });

            }

            return lstCardapio;
        }

        private List<Cardapio> GetCardapioWithPrice(List<Cardapio> lstCardapio)
        {
            decimal lastIdLanche = 0, countCardapio = 0, allcardapio = lstCardapio.Count;
            string lastIngredienteNome = string.Empty;
            Cardapio cardapioReturn = new Cardapio();
            List<Cardapio> lstCardapioReturn = new List<Cardapio>();

            lstCardapio.ForEach(delegate (Cardapio cardapio)
            {
                if (lastIdLanche != cardapio.IdLanche)
                {
                    if (countCardapio > 0)
                        lstCardapioReturn.Add(cardapioReturn);

                    cardapioReturn = new Cardapio();

                    cardapioReturn = cardapio;

                    lastIdLanche = cardapio.IdLanche;

                    countCardapio++;
                }
                else
                {
                    lastIdLanche = cardapio.IdLanche;

                    cardapioReturn.IdLancheIngrediente = cardapio.IdLancheIngrediente;
                    cardapioReturn.LancheNome = cardapio.LancheNome;

                    cardapioReturn.IngredienteNome += ", " + cardapio.IngredienteNome;
                    cardapioReturn.Valor += cardapio.Valor;

                    countCardapio++;
                }

                if (countCardapio.Equals(allcardapio))
                    lstCardapioReturn.Add(cardapioReturn);

            });

            return lstCardapioReturn;
        }

        #endregion
    }
}
