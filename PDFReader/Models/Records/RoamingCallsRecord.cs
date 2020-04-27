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

        public Duration RecibidasDuration { get; set; }
        public Duration OriginadasDuration { get; set; }
        public Duration TotalDuration { get; set; }
    }
}
