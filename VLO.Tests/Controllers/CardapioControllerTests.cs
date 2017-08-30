using Microsoft.VisualStudio.TestTools.UnitTesting;
using VLO.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLO.Models;

namespace VLO.Controllers.Tests
{
    [TestClass()]
    public class CardapioControllerTests
    {
        public ContextDB db = new ContextDB();

        #region .: Testes para Valor dos lanches de cardápio :.

        [TestMethod()]
        public void GetValorSumXBacon()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BACON") select l.IdLanche).FirstOrDefault();
            decimal baconValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("BACON") select i.Valor).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            decimal expectedResult = baconValue + carneValue + queijoValue;
            decimal actual = 0;
            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();
            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            // act
            actual = CardapioController.GetValorSum(listIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorSumXBurger()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            decimal expectedResult = carneValue + queijoValue;
            decimal actual = 0;
            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();
            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            // act
            actual = CardapioController.GetValorSum(listIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorSumXEgg()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-EGG BACON") select l.IdLanche).FirstOrDefault();
            decimal ovoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("OVO") select i.Valor).FirstOrDefault();
            decimal baconValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("BACON") select i.Valor).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            decimal expectedResult = ovoValue + baconValue + carneValue + queijoValue;
            decimal actual = 0;
            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();
            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            // act
            actual = CardapioController.GetValorSum(listIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorSumXEggBacon()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-EGG") select l.IdLanche).FirstOrDefault();
            decimal ovoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("OVO") select i.Valor).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            decimal expectedResult = ovoValue + carneValue + queijoValue;
            decimal actual = 0;
            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();
            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            // act
            actual = CardapioController.GetValorSum(listIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        #endregion

        #region .: Testes para Regra de Negocios (Calculo de preco e promocoes) :.

        [TestMethod()]
        public void GetValorDescontoLight()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            Ingrediente alface = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("ALFACE") select i).FirstOrDefault();
            decimal actual = 0;
            decimal expectedResult = ((carneValue + queijoValue + alface.Valor) * 10) / 100;
            string returnMessage = string.Empty;

            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();
            lstIdIngredientes.Add(alface.IdIngrediente);

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();

            // act
            actual = CardapioController.CheckDiscount(listIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorDescontoMuitaCarne()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            Ingrediente carne = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i).FirstOrDefault();
            decimal actual = 0;
            decimal expectedResult = carne.Valor;
            string returnMessage = string.Empty;

            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();
            listIngredientes.Add(carne);
            listIngredientes.Add(carne);

            // act
            actual = CardapioController.CheckDiscount(listIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorDescontoMuitaCarneMais()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal queijoValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i.Valor).FirstOrDefault();
            Ingrediente carne = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i).FirstOrDefault();
            decimal actual = 0;
            decimal expectedResult = carne.Valor * 2;
            string returnMessage = string.Empty;

            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();
            listIngredientes.Add(carne);
            listIngredientes.Add(carne);
            listIngredientes.Add(carne);
            listIngredientes.Add(carne);
            listIngredientes.Add(carne);

            // act
            actual = CardapioController.CheckDiscount(listIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorDescontoMuitoQueijo()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            Ingrediente queijo = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i).FirstOrDefault();
            decimal actual = 0;
            decimal expectedResult = queijo.Valor;
            string returnMessage = string.Empty;

            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();
            listIngredientes.Add(queijo);
            listIngredientes.Add(queijo);
            
            // act
            actual = CardapioController.CheckDiscount(listIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetValorDescontoMuitoQueijoMais()
        {
            // arrange
            int idLanche = (from l in db.Lanches where l.LancheNome.ToUpper().Equals("X-BURGER") select l.IdLanche).FirstOrDefault();
            decimal carneValue = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("CARNE") select i.Valor).FirstOrDefault();
            Ingrediente queijo = (from i in db.Ingredientes where i.IngredienteNome.ToUpper().Contains("QUEIJO") select i).FirstOrDefault();
            decimal actual = 0;
            decimal expectedResult = queijo.Valor * 2;
            string returnMessage = string.Empty;

            List<int> lstIdIngredientes = (from li in db.LancheIngredientes where li.IdLanche == idLanche select li.IdIngrediente).ToList();

            List<Ingrediente> listIngredientes = (from c in db.Ingredientes where lstIdIngredientes.Contains(c.IdIngrediente) select c).ToList();
            listIngredientes.Add(queijo);
            listIngredientes.Add(queijo);
            listIngredientes.Add(queijo);
            listIngredientes.Add(queijo);
            listIngredientes.Add(queijo);

            // act
            actual = CardapioController.CheckDiscount(listIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        #endregion
    }
}