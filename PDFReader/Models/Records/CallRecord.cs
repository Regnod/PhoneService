using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class CallRecord
    {
        public int RecibidasCalls { get; set; }
        public int OriginadasCalls { get; set; }
        public int HorasPicoCalls { get; set; }
        public int HorasNoPicoCalls { get; set; }
        public int TotalCalls { get; set; }

        public float RecibidasCost { get; set; }
        public float OriginadasCost { get; set; }
        public float HorasPicoCost { get; set; }
        public float HorasNoPicoCost { get; set; }
        public float TotalCost { get; set; }

        public float ReciveDuration { get; set; }
        public float MadeDuration { get; set; }
        public float HorasPicoDuration { get; set; }
        public float HorasNoPicoDuration { get; set; }
        public float TotalDuration { get; set; }
    }
}
