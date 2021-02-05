using System;
using Data.Models;
using PDFReader;
using PDFReader.Models;

using Repo;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(int.MaxValue);
            Factura s = Parser.ReadPdfFile("/mnt/69F79531507E7A36/CS/This year's stuff/Base de Datos II/PhoneService/Enero 2020.pdf");
            // Console.WriteLine("Termino");
            // foreach (var phone in s.Numbers)
            // {
            //     db.PhoneLines.Add(new PhoneLine()
            //     {
            //         PhoneNumber = phone.Number
            //     });
            // }
            Console.WriteLine("Hola");
        }
    }
}
