using Compensaction.Share;
using CompensationPlan.Calculo.Bussines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
namespace CompensationPlan.Calculo
{
    class Program
    {

        private static Timer _timer;
        static void SetTimer()
        {
            _timer = new Timer(2000);
            _timer.Elapsed += TimeronElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void TimeronElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"timer event raised at {e.SignalTime }");
            OnTimedEvent();
        }

        static void Main(string[] args)
        {
            bool procesando = false;
            Console.WriteLine("Aplication Started");
            Console.WriteLine("Press any key to exit");
            var time = new System.Threading.Timer(obj=> {
                Console.WriteLine($"System.Threading.Timer event raised at:{DateTime.UtcNow:F}");
                if (procesando==false)
                {
                    procesando = true;
                    OnTimedEvent();
                    procesando = false;
                }
                

            },null,TimeSpan.FromSeconds(20
            ), TimeSpan.FromSeconds(20));
           
            
        
            Console.ReadLine();




        }

       
        private static void OnTimedEvent()
        {

           


            using (var db = new LocalDbContext())
            {

                Console.WriteLine("Iniciado");
                CalcularComision calcularComision = new CalcularComision(db);
                var proceso = db.PCProceso.FirstOrDefault();
                if (proceso != null)
                {
                    if (proceso.Iniciado==false && proceso.Accion==1)
                    {
                        proceso.Iniciado = true;
                        proceso.RegistrosProcesados = 0;
                        proceso.RegistrosCerrados = 0;
                        db.SaveChanges();
                        db.Database.ExecuteSqlCommand("PCLimpiaTemporal @p0", "");
                        calcularComision.extraerDatos(proceso.Desde, proceso.Hasta, proceso.UsuarioRegistro);


                        List<PCComisionesTemporal> pCTemporal = new List<PCComisionesTemporal>();
                        pCTemporal = db.PCComisionesTemporal.ToList();
                        if (pCTemporal != null)
                        {
                            proceso.TotalReg = pCTemporal.Count;
                            db.SaveChanges();

                            int cant = 1;
                            foreach (var item in pCTemporal)
                            {
                                proceso.RegistrosProcesados = cant++;

                                calcularComision.CalcularRecibo(item.Id);
                                
                                Console.WriteLine($"Calculando el reg Nro:{item.Id}");
                            }
                        }

                        proceso.Culminado = true;
                        proceso.Accion = 0;
                        db.SaveChanges();
                        db.Database.ExecuteSqlCommand("PCSpResumenComisionTemporal @p0", "");
                        
    }

                    if (proceso.Iniciado == false && proceso.Accion == 2 )
                    {

                        proceso.Iniciado = true;
                        db.SaveChanges();


                        calcularComision.Cierre();

                        proceso.Accion = 0;
                        proceso.Culminado = true;
                        db.SaveChanges();
                    }

                }


                Console.WriteLine("Culmidado");

            }

        }
    }
}
