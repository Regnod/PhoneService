using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;

namespace MVCPhoneServiceWeb.Utils
{
    public static class SD
    {
        public static string AdminUser = "Admin";
        public static string VisitorUser = "Vistitor";
        //public static string ReadPdfFile(string fileName)
        //{
        //    StringBuilder text = new StringBuilder();

        //    if (File.Exists(fileName))
        //    {
        //        PdfReader pdfReader = new PdfReader(fileName);

        //        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
        //        {
        //            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
        //            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

        //            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
        //            text.Append(currentText);
        //        }
        //        pdfReader.Close();
        //    }
        //    return text.ToString();
        //}
    }

        //public static class PdfTextExtractor
        //{
        //    public static void pdfText(string path) //ej: path = "D:\\Demo.pdf"
        //    {
        //        var pdf = string.Empty;
        //        PdfReader reader = new PdfReader(path);

        //        for (int page = 1; page <= reader.NumberOfPages; page++)
        //        {
        //            pdf += PdfTextExtractor.GetTextFromPage(reader, page);
        //        }
        //        reader.Close();
        //        //Hasta aqui leemos el pdf y lo guardamos en un string

        //        // Con esto creamos un array con las lineas, 
        //        // la funcion split separa cadenas por un caracter determinado
        //        // "\n" es el salto de linea
        //        string[] lineas = data.Split('\n');

        //        foreach (string linea in lineas)
        //        {
        //            // Aqui ya preguntas si la linea empieza por...
        //            // O sea lo que tengo hecho yo, tienes que ver porque pueden cambiar cosas
        //        }
        //    }

        //}
}
