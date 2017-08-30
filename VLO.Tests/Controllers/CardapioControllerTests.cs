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
        #region .: Banco de Dados :.
        
        public ContextDB db = new ContextDB();

        #endregion

        #region .: Instanciacao de objetos manuais :.
        
        public Lanche xBacon = new Lanche { IdLanche = 1, LancheNome = "X-BACON" };
        public Lanche xBurger = new Lanche { IdLanche = 2, LancheNome = "X-BURGER" };
        public Lanche xEgg = new Lanche { IdLanche = 3, LancheNome = "X-EGG" };
        public Lanche xEggBacon = new Lanche { IdLanche = 4, LancheNome = "X-EGG BACON" };
        
        public Ingrediente bacon = new Ingrediente { IdIngrediente = 1, IngredienteNome = "BACON", Valor = (decimal)2.00 };
        public Ingrediente carne = new Ingrediente { IdIngrediente = 2, IngredienteNome = "CARNE", Valor = (decimal)3.00 };
        public Ingrediente queijo = new Ingrediente { IdIngrediente = 3, IngredienteNome = "QUEIJO", Valor = (decimal)1.50 };
        public Ingrediente alface = new Ingrediente { IdIngrediente = 4, IngredienteNome = "ALFACE", Valor = (decimal)0.40 };
        public Ingrediente ovo = new Ingrediente { IdIngrediente = 5, IngredienteNome = "OVO", Valor = (decimal)0.40 };
        
        #endregion

        #region .: Testes com valores instanciados manualmente :.

        #region .: Testes para Valor dos lanches de cardápio :.

        [TestMethod()]
        public void GetManualValorSumXBacon()
        {
            // arrange
            List<Ingrediente> xBaconIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = bacon.Valor + carne.Valor + queijo.Valor;

            xBaconIngredientes.Add(bacon);
            xBaconIngredientes.Add(carne);
            xBaconIngredientes.Add(queijo);

            // act
            actual = CardapioController.GetValorSum(xBaconIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetManualValorSumXBurger()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = carne.Valor + queijo.Valor;
            
            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            // act
            actual = CardapioController.GetValorSum(xBurgerIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetManualValorSumXEgg()
        {
            // arrange
            List<Ingrediente> xEggIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = ovo.Valor + carne.Valor + queijo.Valor;

            xEggIngredientes.Add(ovo);
            xEggIngredientes.Add(carne);
            xEggIngredientes.Add(queijo);

            // act
            actual = CardapioController.GetValorSum(xEggIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetManualValorSumXEggBacon()
        {
            // arrange
            List<Ingrediente> xEggBaconIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = ovo.Valor + bacon.Valor + carne.Valor + queijo.Valor;

            xEggBaconIngredientes.Add(ovo);
            xEggBaconIngredientes.Add(bacon);
            xEggBaconIngredientes.Add(carne);
            xEggBaconIngredientes.Add(queijo);

            // act
            actual = CardapioController.GetValorSum(xEggBaconIngredientes);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        #endregion

        #region .: Testes para Regra de Negocios (Calculo de preco e promocoes) :.

        [TestMethod()]
        public void GetManualValorDescontoLight()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = ((carne.Valor + queijo.Valor + alface.Valor ) * 10) / 100;
            string returnMessage = string.Empty;

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(alface);

            // act
            actual = CardapioController.CheckDiscount(xBurgerIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void GetManualValorDescontoMuitaCarne()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = carne.Valor;
            string returnMessage = string.Empty;

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(carne);

            // act
            actual = CardapioController.CheckDiscount(xBurgerIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);
            
        }

        [TestMethod()]
        public void GetManualValorDescontoMuitaCarneMais()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = carne.Valor * 2;
            string returnMessage = string.Empty;

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(carne);

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(carne);

            // act
            actual = CardapioController.CheckDiscount(xBurgerIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);

        }

        [TestMethod()]
        public void GetManualValorDescontoMuitoQueijo()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = queijo.Valor;
            string returnMessage = string.Empty;

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(queijo);
            xBurgerIngredientes.Add(queijo);
            
            // act
            actual = CardapioController.CheckDiscount(xBurgerIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);

        }

        [TestMethod()]
        public void GetManualValorDescontoMuitoQueijoMais()
        {
            // arrange
            List<Ingrediente> xBurgerIngredientes = new List<Ingrediente>();
            decimal actual = 0;
            decimal expectedResult = queijo.Valor * 2;
            string returnMessage = string.Empty;

            xBurgerIngredientes.Add(carne);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(queijo);
            xBurgerIngredientes.Add(queijo);

            xBurgerIngredientes.Add(queijo);
            xBurgerIngredientes.Add(queijo);
            xBurgerIngredientes.Add(queijo);

            // act
            actual = CardapioController.CheckDiscount(xBurgerIngredientes, out returnMessage);

            // assert 
            Assert.AreEqual(expectedResult, actual);

        }
        
        #endregion

        #endregion

        #region .: Utilizando Bando de Dados :.

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

        #endregion
    }
}