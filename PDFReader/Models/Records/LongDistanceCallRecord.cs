using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class LongDistanceCallRecord
    {
        public int InternacionalCalls { get; set; }
        public int NacionalCalls { get; set; }
        public int TotalCalls { get; set; }

        public float InternacionalCost { get; set; }
        public float NacionalCost { get; set; }
        public float TotalCost { get; set; }

        public float InternacionalDuration { get; set; }
        public float NacionalDuration { get; set; }
        public float TotalDuration { get; set; }
    }
}
