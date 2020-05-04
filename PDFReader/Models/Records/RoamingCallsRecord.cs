using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class RoamingCallsRecord
    {
        public int RecibidasCalls { get; set; }
        public int OriginadasCalls { get; set; }
        public int TotalCalls { get; set; }

        public float RecibidasCost { get; set; }
        public float OriginadasCost { get; set; }
        public float TotalCost { get; set; }

        public float RecibidasDuration { get; set; }
        public float OriginadasDuration { get; set; }
        public float TotalDuration { get; set; }
    }
}
