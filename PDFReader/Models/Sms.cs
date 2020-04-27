using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models
{
    public class Sms
    {
        public int No { get; set; }
        public bool RoamingSms { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string E_R { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public float Monto { get; set; }
        public float LD { get; set; }
        public float Discount { get; set; }
        public float Charge { get; set; }
        public float Total { get; set; }
    }
}
