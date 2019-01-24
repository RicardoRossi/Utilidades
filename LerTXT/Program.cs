using System;
using System.IO;

namespace LeTXT
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read byte
            //using (FileStream fs = new FileStream(@"C:\RELATORIO\arq.txt", FileMode.Open, FileAccess.Read))
            //{
            //    while (fs.Position < fs.Length)
            //    {
            //        var c = (char)fs.ReadByte();
            //        Console.WriteLine(c);
            //    }
            //    Console.ReadKey();
            //}

            using (FileStream fs = new FileStream(@"C:\RELATORIO\arq.txt", FileMode.Open, FileAccess.Read))
            {

            }


        }
    }
}
