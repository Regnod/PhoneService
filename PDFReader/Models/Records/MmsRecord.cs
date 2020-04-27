using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class MmsRecord
    {
        public int RecibidasMms { get; set; }
        public int OriginadasMms { get; set; }
        public int HorasPicoMms { get; set; }
        public int HorasNoPicoMms { get; set; }
        public int TotalMms { get; set; }

        public float RecibidasCost { get; set; }
        public float OriginadasCost { get; set; }
        public float HorasPicoCost { get; set; }
        public float HorasNoPicoCost { get; set; }
        public float TotalCost { get; set; }
    }
}
