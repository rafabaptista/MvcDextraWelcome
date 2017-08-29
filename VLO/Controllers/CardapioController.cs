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
        
        public ActionResult Pedido(int? id)
        {
            Cardapio cardapio;
            decimal valorSum = 0;

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

            valorSum = GetValorSum(listIngredientes);

            ViewBag.DiscountMessage = "Nehuma promoção.";
            ViewBag.Ingredientes = listIngredientes;
            Session["Ingredientes"] = listIngredientes;
            ViewBag.Cardapio = cardapio;
            ViewBag.Discount = 0;
            ViewBag.ValorSum = valorSum;
            ViewBag.ValorTotal = valorSum;

            return View(cardapio);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pedido([Bind(Include = "IdIngrediente,IngredienteNome")] int? id, int? IdIngrediente, string IngredienteNome)
        {
            Cardapio cardapio;
            
            List<Ingrediente> listIngredientes = (List < Ingrediente > )Session["Ingredientes"];
            
            Ingrediente ingrediente = (from i in db.Ingredientes where i.IdIngrediente == IdIngrediente select i).FirstOrDefault();

            string returnDiscountMessage = string.Empty;
            decimal valorSum = 0, valorDiscount = 0;

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

            valorSum = GetValorSum(listIngredientes);

            valorDiscount = CheckDiscount(listIngredientes, out returnDiscountMessage);

            ViewBag.Discount = valorDiscount;
            ViewBag.DiscountMessage = returnDiscountMessage;

            ViewBag.Ingredientes = listIngredientes;
            Session["Ingredientes"] = listIngredientes;
            ViewBag.Cardapio = cardapio;
            ViewBag.ValorSum = valorSum;
            ViewBag.ValorTotal = valorSum - valorDiscount;

            return View(cardapio);
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Ingrediente> listIngredientes = (List<Ingrediente>)Session["Ingredientes"];
            string returnDiscountMessage = string.Empty;
            decimal valorSum = 0, valorDiscount = 0;

            listIngredientes.Remove(listIngredientes[id]);

            valorSum = GetValorSum(listIngredientes);

            valorDiscount = CheckDiscount(listIngredientes, out returnDiscountMessage);

            ViewBag.Discount = valorDiscount;
            ViewBag.DiscountMessage = returnDiscountMessage;

            ViewBag.Ingredientes = listIngredientes;
            Session["Ingredientes"] = listIngredientes;
            
            ViewBag.ValorSum = valorSum;
            ViewBag.ValorTotal = valorSum - valorDiscount;

            return View("Pedido"); 
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

        private decimal CheckDiscount(List<Ingrediente> listIngredientes, out string returnDiscountMessage)
        {
            int countHamburguer = GetQtyIngrediente("CARNE", listIngredientes),
                countQueijo = GetQtyIngrediente("QUEIJO", listIngredientes),
                countAlface = GetQtyIngrediente("ALFACE", listIngredientes),
                countBacon = GetQtyIngrediente("BACON", listIngredientes),
                qtyPromoHamburguer = 0,
                qtyPromoQueijo = 0;

            decimal valorHamburguer = GetValorIngrediente("CARNE"),
                    valorQueijo = GetValorIngrediente("QUEIJO"),
                    valorSum = GetValorSum(listIngredientes),
                    valorTotal = 0;

            if (countAlface > 0 && countBacon == 0)
            {
                valorTotal = (valorSum * 10) / 100;
                returnDiscountMessage = "Promoções Aplicadas: <br />- Light";
            }
            else
            {
                valorTotal = 0;
                returnDiscountMessage = string.Empty;
            }

            if (countHamburguer >= 3)
            {
                qtyPromoHamburguer = (int)(countHamburguer - (countHamburguer / 3));

                valorTotal = (valorHamburguer * (countHamburguer - qtyPromoHamburguer)) + valorTotal;

                returnDiscountMessage += (string.IsNullOrEmpty(returnDiscountMessage) ? "Promoções Aplicadas: <br />- Muita carne" : "<br />- Muita carne");
            }

            if (countQueijo >= 3)
            {
                qtyPromoQueijo = (int)(countQueijo - (countQueijo / 3));

                valorTotal = (valorQueijo * (countQueijo - qtyPromoQueijo)) + valorTotal;

                returnDiscountMessage += (string.IsNullOrEmpty(returnDiscountMessage) ? "Promoções Aplicadas: <br />- Muito queijo" : "<br />- Muito queijo");
            }

            return valorTotal;
        }

        private int GetQtyIngrediente(string ingrediente, List<Ingrediente> listIngredientes)
        {
            return (from i in listIngredientes where i.IngredienteNome.ToUpper().Trim().Contains(ingrediente) select i).ToList().Count();
        }

        private decimal GetValorIngrediente(string ingrediente)
        {
            return (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains(ingrediente) select i.Valor).FirstOrDefault();
        }

        private decimal GetValorSum(List<Ingrediente> listIngredientes)
        {
            return listIngredientes.Sum(li => li.Valor);
        }

        #endregion
    }
}
