using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PDFReader.Models;
using PDFReader.Models.Records;
namespace PDFReader
{
    public class Parser
    {
        public static string ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);


                    //Separar la pagina en lineas
                    string[] Lines = currentText.Split("\n");
                    PhoneNumber phoneNumber;
                    for (int l = 0; l < Lines.Length; l++)
                    {
                        //empezamos a parsear linea por linea
                        string subLine = Lines[l].Substring(0, 10);
                        string[] splitLine = Lines[l].Split(" ");
                        long phone;
                        if(subLine == "TELEFONO :" && long.TryParse(splitLine[2],out phone))
                        {
                            phoneNumber = new PhoneNumber(phone.ToString());
                        }
                        subLine = Lines[l].Substring(0, 25);
                        if (subLine == "LISTADO DE LLAMADAS TEL :")
                        {
                            string Line = "";
                            if(l+1 <Lines.Length)
                                Line = Lines[l+1];
                            if(Line == "NO FECHA HORA E/R LUGAR DESTINO TELEFONO DURACION TA LD DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan las llamadas
                                for (int i = 0; i < Lines; i++)
                                {

                                }
                            }
                        }   
                    }





                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }
    }
}
