using Compensaction.Share;
using CompensationPlan.Calculo;
using CompensationPlan.Calculo.Bussines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Compensation.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private readonly LocalDbContext _context;

        public UnitTest1(LocalDbContext context)
        {
            _context = context;
        }

       

        

        [TestMethod]
        public void PagoComisionDoble()
        {
            //Preparacion
            string orden = "2310719100";
            string idCliente = "742969";
            CalcularComision calcularComisiones = new CalcularComision(_context);

            //Ejecucion
            bool clientePagaComisionDoble = calcularComisiones.ClientePagaComisionDoble(idCliente,(DateTime)calcularComisiones.GetfechaOrden(orden));

            //Comprobacion

            Assert.IsTrue(clientePagaComisionDoble);

        }
    }
}
