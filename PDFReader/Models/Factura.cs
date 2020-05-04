using System;
using System.Collections.Generic;
using System.Text;
using PDFReader.Models.Records;

namespace PDFReader.Models
{
    public class Factura
    {
        public List<PhoneNumber> Numbers { get; set; }
        public string FactNumber { get; set; }
        public DateTime Date { get; set; }
        public FactRecord FactRecord { get; set; }
    }
}
