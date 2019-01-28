using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereBaanXDesenho
{
    class MainBaanDesenho
    {
        static void Main(string[] args)
        {
            var listaBaan = new ListaRack();
            var listaSw = new ListaRack();

            var rackDoSolidworks = listaSw.CriarKitRack(@"C:\Users\54808\source\repos\Utilidades\4020001-4020256 - do SolidWorks.txt");
            var rackDoBaan = listaBaan.CriarKitRack(@"C:\Users\54808\source\repos\Utilidades\4020001-4020256 - do Baan.txt");

            var ListaSwOrder = rackDoSolidworks.OrderBy(x => x.codigoDoRack);
            var ListaBaanOrder = rackDoBaan.OrderBy(x => x.codigoDoRack);
            IEnumerable<Componente> compOrderSw;
            IEnumerable<Componente> compOrderBaan;
            //foreach (var sw in ListaSwOrder)
            //{
            //    compOrderSw = sw.ListaDeComponentes.OrderBy(x => x.codigo);
            //    Console.WriteLine(sw.codigoDoRack);
            //    foreach (var item in compOrderSw)
            //    {
            //        Console.WriteLine(item.codigo + ";" + item.qt);
            //    }
            //}

            //Console.WriteLine();
            //Console.WriteLine("---------------------------------------------------------------------------");

            foreach (var ba in ListaBaanOrder)
            {
                compOrderBaan = ba.ListaDeComponentes.OrderBy(x => x.codigo);
                Console.WriteLine(ba.codigoDoRack);
                foreach (var item in compOrderBaan)
                {
                    Console.WriteLine(item.codigo + ";" + item.qt);
                }
            }

            //EscreveTXT(rackDoBaan);
            //EscreveTXT(rackDoSolidworks);
            Console.ReadKey();
        }

        static void EscreveTXT(List<Rack> listaDeRackFinal)
        {
            try
            {
                var i = 0;
                using (StreamWriter sw = new StreamWriter(@"C:\RELATORIO\outBaan.txt"))
                {
                    foreach (var rackFinal in listaDeRackFinal)
                    {


                        var flex = rackFinal.ListaDeComponentes.Select(x => x.codigo.Contains("3010001"));

                        foreach (var f in flex)
                        {
                            if (f == true)
                            {
                                i++;
                            }
                        }

                    }
                    Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
