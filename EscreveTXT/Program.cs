using System;
using System.Collections.Generic;
using System.IO;

namespace EscreveTXT
{
    class Program
    {
        static void Main(string[] args)
        {
           EscreveTXT();
        }


        static void EscreveTXT(List<string> listaDeCodigos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\54808\Documents\montcp_out.txt"))
                {
                    foreach (var codigo in listaDeCodigos)
                    {
                        sw.WriteLine(codigo);
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
