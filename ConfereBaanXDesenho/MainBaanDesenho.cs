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

            Rack[,] racks= new Rack[10000,10000];

            var rackSw = new List<Rack>();
            foreach (var sw in ListaSwOrder)
            {
                //compOrderSw = sw.ListaDeComponentes.OrderBy(x => x.codigo);
                rackSw.Add(sw);
            }
            EscreveTXT(rackSw, @"C:\RELATORIO\outSw.txt");

            var rackBa = new List<Rack>();
            foreach (var ba in ListaBaanOrder)
            {
                //compOrderBaan = ba.ListaDeComponentes.OrderBy(x => x.codigo);
                rackBa.Add(ba);
            }
            EscreveTXT(rackBa, @"C:\RELATORIO\outBaan.txt");

            //EscreveTXT(rackDoBaan);
            //EscreveTXT(rackDoSolidworks);
            Console.ReadKey();
        }

        static void EscreveTXT(List<Rack> listaDeRackFinal, string fullPathOutTxt)
        {
            try
            {
                var i = 0;
                using (StreamWriter sw = new StreamWriter(fullPathOutTxt))
                {
                    foreach (var rackFinal in listaDeRackFinal)
                    {
                        //var flex = rackFinal.ListaDeComponentes.Select(x => x.codigo.Contains("3010001"));
                        sw.WriteLine(rackFinal.codigoDoRack);
                        foreach (var item in rackFinal.ListaDeComponentes.OrderBy(x=>x.codigo))
                        {
                            sw.WriteLine(item.codigo + ";" + item.qt);
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
