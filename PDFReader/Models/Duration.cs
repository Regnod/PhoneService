using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models
{
    public class Duration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public Duration(string h, string m, string s)
        {
            Hours = int.Parse(h);
            Minutes = int.Parse(m);
            Seconds = int.Parse(s);
        }
    }
}
