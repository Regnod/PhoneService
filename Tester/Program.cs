using System;
using PDFReader;
using PDFReader.Models;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Factura s = Parser.ReadPdfFile(@"C:\Users\Richard\Documents\Facturas 2020\Enero2020.pdf");
        }
    }
}
