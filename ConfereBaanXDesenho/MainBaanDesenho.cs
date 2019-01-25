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
            try
            {
                var listaDeLinhasDoArquivo = new List<string>();
                using (var reader = new StreamReader(@"C:\Users\54808\source\repos\Utilidades\4020001-4020256 - do SolidWorks.txt", Encoding.Default))
                {
                    while (!reader.EndOfStream)
                    {
                        //Lê a linha do txt
                        string linhaDoArquivoTXT = reader.ReadLine();

                        //Adiciona a linha numa lista
                        listaDeLinhasDoArquivo.Add(linhaDoArquivoTXT);
                    }
                }
                var listaDeRackSemCodigo = new List<Rack>();
                var rack = new Rack();
                //int i = 1;
                //Console.WriteLine(i);
                foreach (var linha in listaDeLinhasDoArquivo)
                {
                    if (!linha.Equals(";"))
                    {
                        //Adciona todos os codigos como componentes do rack inclusive o proprio codigo do rack.
                        //Quando encontra a linha com o ";" indica o começo de um novo rack.
                        //No else adiciona o rack velho numa lista de rack e cria um novo objeto rack para receber os seus

                        //Componente 4020001; Codigo rack esta na lista de componentes
                        //Componente 2237; 2
                        //Componente 4216; 1
                        //Componente 2002716; 1
                        //Componente 2003687; 3
                        //Componente 2003724; 2
                        //Componente 2003738; 1
                        //Componente 2003739; 1
                        //Componente 2003740; 1
                        //Componente 2003741; 4
                        //Componente 2003743; 1
                        //Componente 2007918; 1
                        //Componente 2034870; 1
                        //Componente 2046643; 1
                        //Componente 2046819; 4
                        //Componente 2046820; 2
                        //Componente 2046821; 2
                        //Componente 2046822; 2
                        //Componente 2047621; 1
                        //Componente 2047930; 1
                        //Componente 2048261; 1
                        //Componente 2048294; 2
                        //Componente 2300082; 1
                        //Componente 2300142; 1
                        //Componente 2300166; 1
                        //Componente 3002924; 2
                        //Componente 3002927; 1
                        //Componente 3002932; 4
                        //Componente 3003557; 1
                        //Componente 3010001; 1
                        //Componente MONTCPRP0020; 2
                        //Componente 2047866; 1
                        string[] codigoQuantidadeNaLinha = linha.Split(';');
                        var c = new Componente() { codigo = codigoQuantidadeNaLinha[0], qt = codigoQuantidadeNaLinha[1] };
                        rack.ListaDeComponentes.Add(c);
                    }
                    else
                    {
                        //Guarda todos os racks criados.
                        //aqui o rack está com todos os componentes que pertencem a ele.
                        listaDeRackSemCodigo.Add(rack);
                        rack = new Rack();
                    }
                }

                //Cria o rack final, retirando o primeiro componente da lista que é o codigo item pai RACK "4020001" 
                //Rack 4020001; Codigo rack esta na lista de componentes
                //Componente 2237; 2
                //Componente 4216; 1
                //Componente 2002716; 1
                //Componente 2003687; 3
                //Componente 2003724; 2
                //Componente 2003738; 1
                //Componente 2003739; 1
                //Componente 2003740; 1
                //Componente 2003741; 4
                //Componente 2003743; 1
                //Componente 2007918; 1
                //Componente 2034870; 1
                //Componente 2046643; 1
                //Componente 2046819; 4
                //Componente 2046820; 2
                //Componente 2046821; 2
                //Componente 2046822; 2
                //Componente 2047621; 1
                //Componente 2047930; 1
                //Componente 2048261; 1
                //Componente 2048294; 2
                //Componente 2300082; 1
                //Componente 2300142; 1
                //Componente 2300166; 1
                //Componente 3002924; 2
                //Componente 3002927; 1
                //Componente 3002932; 4
                //Componente 3003557; 1
                //Componente 3010001; 1
                //Componente MONTCPRP0020; 2
                //Componente 2047866; 1

                Rack novoRack = null;
                var listaDeRackFinal = new List<Rack>();
                foreach (var r in listaDeRackSemCodigo)
                {
                    novoRack = new Rack() { codigoDoRack = r.ListaDeComponentes.First<Componente>().codigo.Replace(";", "").TrimStart(' ') };
                    var arrayDeComponentes = r.ListaDeComponentes.ToArray<Componente>(); //Converte lista em array
                    for (int i = 1; i < arrayDeComponentes.Length; i++) //Inicia do index 1, pois o index 0 já foi usado para codificar o rack
                    {
                        novoRack.ListaDeComponentes.Add(arrayDeComponentes[i]); //A partir do index 1 são componentes do rack.
                    }
                    novoRack.ListaDeComponentes.OrderBy(x => x.codigo); // LINQ para ordenar a lista
                    listaDeRackFinal.Add(novoRack);
                }

                //foreach (var rackFinal in listaDeRackFinal)
                //{
                //    Console.WriteLine(";\n" + rackFinal.codigoDoRack);
                //    foreach (var comp in rackFinal.ListaDeComponentes)
                //    {
                //        Console.WriteLine(comp.codigo + ";" + comp.qt);
                //    }
                //}
                //Console.WriteLine(";");

                EscreveTXT(listaDeRackFinal);
                Console.WriteLine("Quantide de racks: " + listaDeRackFinal.Count);

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void EscreveTXT(List<Rack> listaDeRackFinal)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\RELATORIO\ListaDoSolidWorks.txt"))
                {
                    foreach (var rackFinal in listaDeRackFinal)
                    {
                        sw.WriteLine(rackFinal.codigoDoRack);
                        foreach (var comp in rackFinal.ListaDeComponentes)
                        {
                            sw.WriteLine(comp.codigo + ";" + comp.qt);
                        }
                        sw.WriteLine(";");
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
