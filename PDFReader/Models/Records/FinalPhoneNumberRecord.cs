using System;
using System.Collections.Generic;
using System.Text;

namespace PDFReader.Models.Records
{
    public class FinalPhoneNumberRecord
    {
        public float ImportePorDetalle { get; set; }
        public float Renta { get; set; }
        public float TiempoDeAire { get; set; }
        public float LargaDistancia { get; set; }
        public float DescuentoTA { get; set; }
        public float DescuentoLD { get; set; }
        public float CargosExtras { get; set; }
        public int TotalDeLlamadas { get; set; }
        public int DiasDeUso { get; set; }
        public float SubTotal { get; set; }
        public float ConsumoSms { get; set; }
        public float ConsumoGprs { get; set; }
        public float ConsumoRoaming { get; set; }
        public float ConsumoRoamingSms { get; set; }
        public float ConsumoRoamingGprs { get; set; }
        public float Total { get; set; }
    }
}
