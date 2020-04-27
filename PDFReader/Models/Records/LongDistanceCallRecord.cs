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

        public Duration InternacionalDuration { get; set; }
        public Duration NacionalDuration { get; set; }
        public Duration TotalDuration { get; set; }
    }
}
