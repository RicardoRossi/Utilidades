using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereBaanXDesenho
{
    public class ListaRack
    {
        public List<Rack> CriarKitRack(string fullPath)
        {
            var listaDeRackFinal = new List<Rack>();
            try
            {
                var listaDeLinhasDoArquivo = new List<string>();
                //using (var reader = new StreamReader(@"C:\Users\54808\source\repos\Utilidades\4020001-4020256 - do Baan.txt", Encoding.Default))
                using (var reader = new StreamReader(fullPath, Encoding.Default))

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

                Rack novoRack = null;               
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaDeRackFinal;
        }
    }
}
