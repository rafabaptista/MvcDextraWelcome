using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Cardapio : LancheIngrediente
    {
        public string LancheNome { get; set; }
        public string IngredienteNome { get; set; }
        public decimal Valor { get; set; }

        //public List<Cardapio> GetAllCardapios()
        //{
        //    using (ContextDB db = new ContextDB())
        //    {
        //        List<Cardapio> lstCardapio = new List<Cardapio>();

        //        var query = (from li in db.LancheIngredientes
        //                join l in db.Lanches on li.IdLanche equals l.IdLanche
        //                join i in db.Ingredientes on li.IdIngrediente equals i.IdIngrediente
        //                let IdLancheIngrediente = li.IdLancheIngrediente
        //                let IdLanche = li.IdLanche
        //                let LancheNome = l.LancheNome
        //                let IdIngrediente = li.IdIngrediente
        //                let IngredienteNome = i.IngredienteNome
        //                let Valor = i.Valor
        //                select new { IdLancheIngrediente, IdLanche, LancheNome, IdIngrediente, IngredienteNome, Valor }).ToList();
                
        //        foreach (var obj in query)
        //        {
        //            lstCardapio.Add(new Cardapio
        //            {
        //                IdLancheIngrediente = obj.IdLancheIngrediente,
        //                IdLanche = obj.IdLanche,
        //                LancheNome = obj.LancheNome,
        //                IdIngrediente = obj.IdIngrediente,
        //                IngredienteNome = obj.IngredienteNome,
        //                Valor = obj.Valor
        //            });
                    
        //        }

        //        return lstCardapio;

        //    }
        //} 
    }
}