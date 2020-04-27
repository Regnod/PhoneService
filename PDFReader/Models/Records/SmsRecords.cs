using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class SmsRecords
    {
        public int RecibidasSms { get; set; }
        public int OriginadasSms { get; set; }
        public int HorasPicoSms { get; set; }
        public int HorasNoPicoSms { get; set; }
        public int TotalSms { get; set; }

        public float RecibidasCost { get; set; }
        public float OriginadasCost { get; set; }
        public float HorasPicoCost { get; set; }
        public float HorasNoPicoCost { get; set; }
        public float TotalCost { get; set; }
    }
}
