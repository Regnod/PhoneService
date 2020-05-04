using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class FactRecord
    {
        public string TA { get; set; }
        public string ConsumoSms { get; set; }
        public string ConsumoSmsRoaming { get; set; }
        public string ConsumoLLamadasRoaming { get; set; }
        public string ConsumoGprs { get; set; }
        public string ConsumoGprsRoaming { get; set; }
        public string Total { get; set; }
    }
}
