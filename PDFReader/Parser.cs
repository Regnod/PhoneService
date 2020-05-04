using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PDFReader.Models;
using PDFReader.Models.Records;
using System.Collections.Generic;

namespace PDFReader
{
    public static class Parser
    {
        public static Factura ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();
            List<PhoneNumber> report = new List<PhoneNumber>();
            Factura factura = new Factura();

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);
                PhoneNumber phoneNumber = null;

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    string[] Lines = currentText.Split("\n");

                    if (page == 1)
                    {
                        factura.FactNumber = Lines[1];
                        string[] splits = Lines[2].Split("/");
                        factura.Date = new DateTime(int.Parse(("20" + splits[2])), int.Parse(splits[1]), int.Parse(splits[0]));
                        for (int i = 0; i < Lines.Length; i++)
                        {
                            if (Lines[i].Length >= 16 && Lines[i].Substring(0, 16) == "3 TIEMPO DE AIRE")
                            {
                                string[] split1 = Lines[i].Split(" ");
                                string[] split2 = Lines[i + 1].Split(" ");
                                string[] split3 = Lines[i + 2].Split(" ");
                                string[] split4 = Lines[i + 3].Split(" ");
                                string[] split5 = Lines[i + 4].Split(" ");
                                string[] split6 = Lines[i + 5].Split(" ");
                                string[] split7 = Lines[i + 6].Split(" ");

                                FactRecord record = new FactRecord()
                                {
                                    TA = split1[4],
                                    ConsumoSms = split2[4],
                                    ConsumoSmsRoaming = split3[5],
                                    ConsumoLLamadasRoaming = split4[4],
                                    ConsumoGprs = split5[3],
                                    ConsumoGprsRoaming = split6[4],
                                    Total = split7[2]
                                };
                                factura.FactRecord = record;
                            }
                        }
                    }

                    //Separar la pagina en lineas
                    for (int l = 0; l < Lines.Length; l++)
                    {
                        //empezamos a parsear linea por linea
                        string linea = Lines[l];

                        string[] splitLine = Lines[l].Split(" ");
                        long phone;
                        if (l < Lines.Length && Lines[l] == "Resumen Tiempo Aire Recibidas Originadas Horas Pico Horas No Pico Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");
                            string[] split3 = Lines[l + 3].Split(" ");
                            CallRecord cr = new CallRecord()
                            {
                                RecibidasCalls = int.Parse(split1[1]),
                                OriginadasCalls = int.Parse(split1[2]),
                                HorasPicoCalls = int.Parse(split1[3]),
                                HorasNoPicoCalls = int.Parse(split1[4]),
                                TotalCalls = int.Parse(split1[5]),

                                RecibidasCost = float.Parse(split2[1]),
                                OriginadasCost = float.Parse(split2[2]),
                                HorasPicoCost = float.Parse(split2[3]),
                                HorasNoPicoCost = float.Parse(split2[4]),
                                TotalCost = float.Parse(split2[5]),

                                ReciveDuration = float.Parse(split3[1]),
                                MadeDuration = float.Parse(split3[2]),
                                HorasPicoDuration = float.Parse(split3[3]),
                                HorasNoPicoDuration = float.Parse(split3[4]),
                                TotalDuration = float.Parse(split3[5]),
                            };
                            phoneNumber.CallRecord = cr;
                            l += 4;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen Larga Distancia Internacional Nacional Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");
                            string[] split3 = Lines[l + 3].Split(" ");
                            LongDistanceCallRecord cr = new LongDistanceCallRecord()
                            {
                                InternacionalCalls = int.Parse(split1[1]),
                                NacionalCalls = int.Parse(split1[2]),
                                TotalCalls = int.Parse(split1[3]),

                                InternacionalCost = float.Parse(split2[1]),
                                NacionalCost = float.Parse(split2[2]),
                                TotalCost = float.Parse(split2[3]),

                                InternacionalDuration = float.Parse(split3[1]),
                                NacionalDuration = float.Parse(split3[2]),
                                TotalDuration = float.Parse(split3[3]),
                            };
                            phoneNumber.LongDistanceCallRecord = cr;
                            l += 4;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen SMS Recibidos Originados Horas Pico Horas No Pico Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");

                            SmsRecords sr = new SmsRecords()
                            {
                                RecibidasSms = int.Parse(split1[3]),
                                OriginadasSms = int.Parse(split1[4]),
                                HorasPicoSms = int.Parse(split1[5]),
                                HorasNoPicoSms = int.Parse(split1[6]),
                                TotalSms = int.Parse(split1[7]),

                                RecibidasCost = float.Parse(split2[1]),
                                OriginadasCost = float.Parse(split2[2]),
                                HorasPicoCost = float.Parse(split2[3]),
                                HorasNoPicoCost = float.Parse(split2[4]),
                                TotalCost = float.Parse(split2[5]),

                            };
                            phoneNumber.SmsRecord = sr;
                            l += 3;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen Larga Distancia SMS Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");
                            LongDistanceSmsRecord sr = new LongDistanceSmsRecord()
                            {
                                TotalSms = int.Parse(split1[1]),
                                TotalCost = float.Parse(split2[1])
                            };
                            phoneNumber.LongDistanceSmsRecord = sr;
                            l = l + 3;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen MMS Recibidos Originados Horas Pico Horas No Pico Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");

                            MmsRecord sr = new MmsRecord()
                            {
                                RecibidasMms = int.Parse(split1[3]),
                                OriginadasMms = int.Parse(split1[4]),
                                HorasPicoMms = int.Parse(split1[5]),
                                HorasNoPicoMms = int.Parse(split1[6]),
                                TotalMms = int.Parse(split1[7]),

                                RecibidasCost = float.Parse(split2[1]),
                                OriginadasCost = float.Parse(split2[2]),
                                HorasPicoCost = float.Parse(split2[3]),
                                HorasNoPicoCost = float.Parse(split2[4]),
                                TotalCost = float.Parse(split2[5]),

                            };
                            phoneNumber.MmsRecord = sr;
                            l += 3;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen GPRS Horas Pico Horas No Pico Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            GprsRecord sr = new GprsRecord()
                            {
                                HorasPicoCost = float.Parse(split1[1]),
                                HorasNoPicoCost = float.Parse(split1[2]),
                                TotalCost = float.Parse(split1[3])
                            };
                            phoneNumber.GprsRecord = sr;
                            l += 2;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen Llamadas de Roaming Recibidas Originadas Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");
                            string[] split3 = Lines[l + 3].Split(" ");

                            RoamingCallsRecord cr = new RoamingCallsRecord()
                            {
                                RecibidasCalls = int.Parse(split1[1]),
                                OriginadasCalls = int.Parse(split1[2]),
                                TotalCalls = int.Parse(split1[3]),

                                RecibidasCost = float.Parse(split2[1]),
                                OriginadasCost = float.Parse(split2[2]),
                                TotalCost = float.Parse(split2[3]),

                                RecibidasDuration = float.Parse(split3[1]),
                                OriginadasDuration = float.Parse(split3[2]),
                                TotalDuration = float.Parse(split3[3]),
                            };
                            phoneNumber.RoamingCallRecord = cr;
                            l += 2;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen SMS de Roaming Recibidos Originados Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");

                            RoamingSmsRecord sr = new RoamingSmsRecord()
                            {
                                RecibidasSms = int.Parse(split1[3]),
                                OriginadasSms = int.Parse(split1[4]),
                                TotalSms = int.Parse(split1[5]),

                                RecibidasCost = float.Parse(split2[1]),
                                OriginadasCost = float.Parse(split2[2]),
                                TotalCost = float.Parse(split2[3]),

                            };
                            phoneNumber.RoamingSmsRecord = sr;
                            l += 3;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen MMS Roaming Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");

                            RoamingMmsRecord sr = new RoamingMmsRecord()
                            {
                                CantMms = int.Parse(split1[3]),
                                TotalCost = float.Parse(split2[1]),

                            };
                            phoneNumber.RoamingMmsRecord = sr;
                            l += 3;
                        }
                        if (l < Lines.Length && Lines[l] == "Resumen GPRS Roaming Total")
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            RoamingGprsRecord sr = new RoamingGprsRecord()
                            {
                                TotalCost = float.Parse(split1[1])
                            };
                            phoneNumber.RoamingGprsRecord = sr;
                            l += 2;
                        }
                        if (l < Lines.Length && 10 <= Lines[l].Length && Lines[l].Substring(0, 10) == "TELEFONO :" && (Lines[l].Split(" ")).Length >= 3 && long.TryParse(Lines[l].Split(" ")[2], out phone))
                        {
                            string[] contr = Lines[l - 1].Split(" ");
                            string[] contr1 = Lines[l + 1].Split(":");
                            string[] contr2 = Lines[l + 3].Split(":");
                            string[] contr3 = Lines[l + 4].Split(":");

                            phoneNumber = new PhoneNumber(phone.ToString());
                            phoneNumber.HasDetails = true;
                            phoneNumber.Contract = contr[2];
                            phoneNumber.Plan = contr1[1].Substring(1, contr1[1].Length - 1);
                            if (contr2[0] == "PAQUETE SMS ")
                                phoneNumber.SmsPlan = contr2[1].Substring(1, contr2[1].Length - 1);
                            if (contr3[0] == "PAQUETE GPRS ")
                                phoneNumber.DataPlan = contr3[1].Substring(1, contr3[1].Length - 1);
                        }
                        // para parsear las llamadas
                        if (l < Lines.Length && Lines[l].Length >= 25 && Lines[l].Substring(0, 25) == "LISTADO DE LLAMADAS TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR DESTINO TELEFONO DURACION TA LD DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan las llamadas
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen Tiempo Aire Recibidas Originadas Horas Pico Horas No Pico Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");
                                        string[] split3 = Lines[i + 3].Split(" ");

                                        CallRecord cr = new CallRecord()
                                        {
                                            RecibidasCalls = int.Parse(split1[1]),
                                            OriginadasCalls = int.Parse(split1[2]),
                                            HorasPicoCalls = int.Parse(split1[3]),
                                            HorasNoPicoCalls = int.Parse(split1[4]),
                                            TotalCalls = int.Parse(split1[5]),

                                            RecibidasCost = float.Parse(split2[1]),
                                            OriginadasCost = float.Parse(split2[2]),
                                            HorasPicoCost = float.Parse(split2[3]),
                                            HorasNoPicoCost = float.Parse(split2[4]),
                                            TotalCost = float.Parse(split2[5]),

                                            ReciveDuration = float.Parse(split3[1]),
                                            MadeDuration = float.Parse(split3[2]),
                                            HorasPicoDuration = float.Parse(split3[3]),
                                            HorasNoPicoDuration = float.Parse(split3[4]),
                                            TotalDuration = float.Parse(split3[5]),
                                        };
                                        phoneNumber.CallRecord = cr;
                                        l = i + 3;
                                        break;
                                    }
                                    if (line == "Resumen Larga Distancia Internacional Nacional Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");
                                        string[] split3 = Lines[i + 3].Split(" ");
                                        LongDistanceCallRecord cr = new LongDistanceCallRecord()
                                        {
                                            InternacionalCalls = int.Parse(split1[1]),
                                            NacionalCalls = int.Parse(split1[2]),
                                            TotalCalls = int.Parse(split1[3]),

                                            InternacionalCost = float.Parse(split2[1]),
                                            NacionalCost = float.Parse(split2[2]),
                                            TotalCost = float.Parse(split2[3]),

                                            InternacionalDuration = float.Parse(split3[1]),
                                            NacionalDuration = float.Parse(split3[2]),
                                            TotalDuration = float.Parse(split3[3]),
                                        };
                                        phoneNumber.LongDistanceCallRecord = cr;
                                        l = i + 3;
                                        break;
                                    }
                                    Call call = new Call();

                                    call.No = int.Parse(splitLine[0]);
                                    call.Date = Date(splitLine[1]);
                                    call.Time = Time(splitLine[2]);
                                    if (splitLine[3] == "EMI")
                                        call.E_R = "EMI";
                                    else
                                        call.E_R = "REC";
                                    Tuple<int, string[]> tuple = Location(splitLine);
                                    call.Location = tuple.Item2[0];
                                    call.Destination = tuple.Item2[1];
                                    int index = tuple.Item1;
                                    index++;
                                    call.PhoneNumber = splitLine[index++];
                                    call.Duration = Duration(splitLine[index++]);
                                    call.TA = float.Parse(splitLine[index++]);
                                    call.LD = float.Parse(splitLine[index++]);
                                    call.Discount = float.Parse(splitLine[index++]);
                                    call.Charge = float.Parse(splitLine[index++]);
                                    call.Total = float.Parse(splitLine[index++]);

                                    phoneNumber.Calls.Add(call);

                                    l = i;
                                }
                            }
                        }
                        // para parsear las Sms
                        if (l < Lines.Length && Lines[l].Length >= 32 && Lines[l].Substring(0, 32) == "LISTADO DE MENSAJES CORTOS TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR DESTINO MONTO LD DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan los sms
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen SMS Recibidos Originados Horas Pico Horas No Pico Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");

                                        SmsRecords sr = new SmsRecords()
                                        {
                                            RecibidasSms = int.Parse(split1[3]),
                                            OriginadasSms = int.Parse(split1[4]),
                                            HorasPicoSms = int.Parse(split1[5]),
                                            HorasNoPicoSms = int.Parse(split1[6]),
                                            TotalSms = int.Parse(split1[7]),

                                            RecibidasCost = float.Parse(split2[1]),
                                            OriginadasCost = float.Parse(split2[2]),
                                            HorasPicoCost = float.Parse(split2[3]),
                                            HorasNoPicoCost = float.Parse(split2[4]),
                                            TotalCost = float.Parse(split2[5]),

                                        };
                                        phoneNumber.SmsRecord = sr;
                                        l = i + 2;
                                        break;
                                    }
                                    if (line == "Resumen Larga Distancia SMS Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");
                                        LongDistanceSmsRecord sr = new LongDistanceSmsRecord()
                                        {
                                            TotalSms = int.Parse(split1[1]),
                                            TotalCost = float.Parse(split2[1])
                                        };
                                        phoneNumber.LongDistanceSmsRecord = sr;
                                        l = i + 2;
                                        break;
                                    }

                                    Sms sms = new Sms();

                                    sms.No = int.Parse(splitLine[0]);
                                    sms.Date = Date(splitLine[1]);
                                    sms.Time = Time(splitLine[2]);
                                    if (splitLine[3] == "EMI")
                                        sms.E_R = "EMI";
                                    else
                                        sms.E_R = "REC";


                                    int index = splitLine.Length - 1;

                                    sms.Total = float.Parse(splitLine[index--]);
                                    sms.Charge = float.Parse(splitLine[index--]);
                                    sms.Discount = float.Parse(splitLine[index--]);
                                    sms.LD = float.Parse(splitLine[index--]);
                                    sms.Monto = float.Parse(splitLine[index--]);
                                    sms.PhoneNumber = splitLine[index--];

                                    string location = "";
                                    int n = 4;
                                    while (index >= n)
                                    {
                                        if (n != index)
                                            location += (splitLine[n] + " ");
                                        else
                                            location += splitLine[n];
                                        n++;
                                    }
                                    sms.Location = location;

                                    //int index = 4;
                                    //string location = "";
                                    //long n = 0;
                                    //while (!long.TryParse(splitLine[index], out n))
                                    //{
                                    //    if (!long.TryParse(splitLine[index + 1], out n))
                                    //        location += (splitLine[index] + " ");
                                    //    else
                                    //        location += splitLine[index];
                                    //    index++;
                                    //}

                                    //sms.PhoneNumber = splitLine[index++];
                                    //sms.Monto = float.Parse(splitLine[index++]);
                                    //sms.LD = float.Parse(splitLine[index++]);
                                    //sms.Discount = float.Parse(splitLine[index++]);
                                    //sms.Charge = float.Parse(splitLine[index++]);
                                    //sms.Total = float.Parse(splitLine[index++]);

                                    phoneNumber.Sms.Add(sms);

                                    l = i;
                                }
                            }
                        }
                        // para parsear gprs
                        if (l < Lines.Length && Lines[l].Length >= 21 && Lines[l].Substring(0, 21) == "LISTADO DE GPRS TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR APN - DESTINO VOLUMEN VOL. FACT. MONTO DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan los datos
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen MMS Recibidos Originados Horas Pico Horas No Pico Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");

                                        MmsRecord sr = new MmsRecord()
                                        {
                                            RecibidasMms = int.Parse(split1[3]),
                                            OriginadasMms = int.Parse(split1[4]),
                                            HorasPicoMms = int.Parse(split1[5]),
                                            HorasNoPicoMms = int.Parse(split1[6]),
                                            TotalMms = int.Parse(split1[7]),

                                            RecibidasCost = float.Parse(split2[1]),
                                            OriginadasCost = float.Parse(split2[2]),
                                            HorasPicoCost = float.Parse(split2[3]),
                                            HorasNoPicoCost = float.Parse(split2[4]),
                                            TotalCost = float.Parse(split2[5]),

                                        };
                                        phoneNumber.MmsRecord = sr;
                                        l = i + 2;
                                        break;
                                    }
                                    if (line == "Resumen GPRS Horas Pico Horas No Pico Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        GprsRecord sr = new GprsRecord()
                                        {
                                            HorasPicoCost = float.Parse(split1[1]),
                                            HorasNoPicoCost = float.Parse(split1[2]),
                                            TotalCost = float.Parse(split1[3])
                                        };
                                        phoneNumber.GprsRecord = sr;
                                        l = i + 1;
                                        break;
                                    }

                                    Gprs gprs = new Gprs();

                                    gprs.No = int.Parse(splitLine[0]);
                                    gprs.Date = Date(splitLine[1]);
                                    gprs.Time = Time(splitLine[2]);
                                    gprs.E_R = "DAT";

                                    //aqui empezamos de atras para alante
                                    int index = splitLine.Length - 1;
                                    gprs.Total = float.Parse(splitLine[index--]);
                                    gprs.Charge = float.Parse(splitLine[index--]);
                                    gprs.Discount = float.Parse(splitLine[index--]);
                                    gprs.Monto = float.Parse(splitLine[index--]);
                                    gprs.Vol_Fact = splitLine[index--];
                                    gprs.Volume = splitLine[index--];
                                    gprs.Apn = splitLine[index--];
                                    string location = "";
                                    for (int k = 4; k <= index; k++)
                                    {
                                        if (k != index)
                                            location += (splitLine[k] + " ");
                                        else
                                            location += splitLine[k];
                                    }
                                    gprs.Location = location;

                                    phoneNumber.Gprs.Add(gprs);

                                    l = i;
                                }
                            }
                        }
                        // para parsear llamadas roaming
                        if (l < Lines.Length && Lines[l].Length >= 36 && Lines[l].Substring(0, 36) == "LISTADO DE LLAMADAS DE ROAMING TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR DESTINO TELEFONO DURACION TA LD DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan las llamadas
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen Llamadas de Roaming Recibidas Originadas Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");
                                        string[] split3 = Lines[i + 3].Split(" ");

                                        RoamingCallsRecord cr = new RoamingCallsRecord()
                                        {
                                            RecibidasCalls = int.Parse(split1[1]),
                                            OriginadasCalls = int.Parse(split1[2]),
                                            TotalCalls = int.Parse(split1[3]),

                                            RecibidasCost = float.Parse(split2[1]),
                                            OriginadasCost = float.Parse(split2[2]),
                                            TotalCost = float.Parse(split2[3]),

                                            RecibidasDuration = float.Parse(split3[1]),
                                            OriginadasDuration = float.Parse(split3[2]),
                                            TotalDuration = float.Parse(split3[3]),
                                        };
                                        phoneNumber.RoamingCallRecord = cr;
                                        l = i + 3;
                                        break;
                                    }

                                    Call call = new Call();
                                    call.RoamingCall = true;
                                    call.No = int.Parse(splitLine[0]);
                                    call.Date = Date(splitLine[1]);
                                    call.Time = Time(splitLine[2]);
                                    if (splitLine[3] == "EMI")
                                        call.E_R = "EMI";
                                    else
                                        call.E_R = "REC";
                                    int index = splitLine.Length - 1;
                                    call.Total = float.Parse(splitLine[index--]);
                                    call.Charge = float.Parse(splitLine[index--]);
                                    call.Discount = float.Parse(splitLine[index--]);
                                    call.LD = float.Parse(splitLine[index--]);
                                    call.TA = float.Parse(splitLine[index--]);
                                    call.Duration = Duration(splitLine[index--]);
                                    call.PhoneNumber = splitLine[index--];

                                    string loc_des = "";
                                    for (int k = 4; k <= index; k++)
                                    {
                                        if (k != index)
                                            loc_des += splitLine[k];
                                        else
                                            loc_des += splitLine[k];
                                    }
                                    call.Lugar_Destino = loc_des;

                                    phoneNumber.Calls.Add(call);

                                    l = i;
                                }
                            }
                        }
                        // para parsear sms roaming
                        if (l < Lines.Length && Lines[l].Length >= 43 && Lines[l].Substring(0, 43) == "LISTADO DE MENSAJES CORTOS DE ROAMING TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR DESTINO MONTO LD DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan los sms
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen SMS de Roaming Recibidos Originados Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");

                                        RoamingSmsRecord sr = new RoamingSmsRecord()
                                        {
                                            RecibidasSms = int.Parse(split1[3]),
                                            OriginadasSms = int.Parse(split1[4]),
                                            TotalSms = int.Parse(split1[5]),

                                            RecibidasCost = float.Parse(split2[1]),
                                            OriginadasCost = float.Parse(split2[2]),
                                            TotalCost = float.Parse(split2[3]),

                                        };
                                        phoneNumber.RoamingSmsRecord = sr;
                                        l = i + 2;
                                        break;
                                    }

                                    Sms sms = new Sms();
                                    sms.RoamingSms = true;
                                    sms.No = int.Parse(splitLine[0]);
                                    sms.Date = Date(splitLine[1]);
                                    sms.Time = Time(splitLine[2]);
                                    if (splitLine[3] == "EMI")
                                        sms.E_R = "EMI";
                                    else
                                        sms.E_R = "REC";


                                    int index = splitLine.Length - 1;

                                    sms.Total = float.Parse(splitLine[index--]);
                                    sms.Charge = float.Parse(splitLine[index--]);
                                    sms.Discount = float.Parse(splitLine[index--]);
                                    sms.LD = float.Parse(splitLine[index--]);
                                    sms.Monto = float.Parse(splitLine[index--]);
                                    sms.PhoneNumber = splitLine[index--];

                                    string location = "";
                                    int n = 4;
                                    while (index >= n)
                                    {
                                        if (n != index)
                                            location += (splitLine[n] + " ");
                                        else
                                            location += splitLine[n];
                                        n++;
                                    }
                                    sms.Location = location;

                                    //int index = 4;
                                    //string location = "";
                                    //long n = 0;
                                    //while (!long.TryParse(splitLine[index], out n))
                                    //{
                                    //    if (!long.TryParse(splitLine[index + 1], out n))
                                    //        location += (splitLine[index] + " ");
                                    //    else
                                    //        location += splitLine[index];
                                    //    index++;
                                    //}

                                    //sms.PhoneNumber = splitLine[index++];
                                    //sms.Monto = float.Parse(splitLine[index++]);
                                    //sms.LD = float.Parse(splitLine[index++]);
                                    //sms.Discount = float.Parse(splitLine[index++]);
                                    //sms.Charge = float.Parse(splitLine[index++]);
                                    //sms.Total = float.Parse(splitLine[index++]);

                                    phoneNumber.Sms.Add(sms);

                                    l = i;
                                }
                            }
                        }
                        // para parsear gprs de roaming
                        if (l < Lines.Length && Lines[l].Length >= 32 && Lines[l].Substring(0, 32) == "LISTADO DE GPRS DE ROAMING TEL :")
                        {
                            string Line = "";
                            if (l + 1 < Lines.Length)
                                Line = Lines[l + 1];
                            if (Line == "NO FECHA HORA E/R LUGAR APN - DESTINO VOLUMEN VOL. FACT. MONTO DESCUENTO CARGOS TOTAL")
                            {
                                //aqui empiezan los datos
                                for (int i = l + 2; i < Lines.Length; i++)
                                {
                                    string line = Lines[i];
                                    splitLine = line.Split(" ");
                                    if (line == "Resumen MMS Roaming Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        string[] split2 = Lines[i + 2].Split(" ");

                                        RoamingMmsRecord sr = new RoamingMmsRecord()
                                        {
                                            CantMms = int.Parse(split1[3]),
                                            TotalCost = float.Parse(split2[1]),

                                        };
                                        phoneNumber.RoamingMmsRecord = sr;
                                        l = i + 2;
                                        break;
                                    }
                                    if (line == "Resumen GPRS Roaming Total")
                                    {
                                        string[] split1 = Lines[i + 1].Split(" ");
                                        RoamingGprsRecord sr = new RoamingGprsRecord()
                                        {
                                            TotalCost = float.Parse(split1[1])
                                        };
                                        phoneNumber.RoamingGprsRecord = sr;
                                        l = i + 1;
                                        break;
                                    }

                                    Gprs gprs = new Gprs();
                                    gprs.RoamingGprs = true;
                                    gprs.No = int.Parse(splitLine[0]);
                                    gprs.Date = Date(splitLine[1]);
                                    gprs.Time = Time(splitLine[2]);
                                    gprs.E_R = "DAT";

                                    //aqui empezamos de atras para alante
                                    int index = splitLine.Length - 1;
                                    gprs.Total = float.Parse(splitLine[index--]);
                                    gprs.Charge = float.Parse(splitLine[index--]);
                                    gprs.Discount = float.Parse(splitLine[index--]);
                                    gprs.Monto = float.Parse(splitLine[index--]);
                                    gprs.Vol_Fact = splitLine[index--];
                                    gprs.Volume = splitLine[index--];
                                    gprs.Apn = splitLine[index--];
                                    string location = "";
                                    for (int k = 4; k <= index; k++)
                                    {
                                        if (k != index)
                                            location += (splitLine[k] + " ");
                                        else
                                            location += splitLine[k];
                                    }
                                    gprs.Location = location;

                                    phoneNumber.Gprs.Add(gprs);

                                    l = i;
                                }
                            }
                        }
                        //Resumen Final
                        if (l < Lines.Length && phoneNumber != null && Lines[l] == "Resumen del Telefono : " + phoneNumber.Number)
                        {
                            string[] split1 = Lines[l + 1].Split(" ");
                            string[] split2 = Lines[l + 2].Split(" ");
                            string[] split3 = Lines[l + 3].Split(" ");
                            string[] split4 = Lines[l + 4].Split(" ");
                            string[] split5 = Lines[l + 5].Split(" ");
                            string[] split6 = Lines[l + 6].Split(" ");
                            string[] split7 = Lines[l + 7].Split(" ");
                            string[] split8 = Lines[l + 8].Split(" ");
                            string[] split9 = Lines[l + 9].Split(" ");
                            string[] split10 = Lines[l + 10].Split(" ");
                            string[] split11 = Lines[l + 11].Split(" ");
                            string[] split12 = Lines[l + 12].Split(" ");
                            string[] split13 = Lines[l + 13].Split(" ");
                            string[] split14 = Lines[l + 14].Split(" ");
                            string[] split15 = Lines[l + 15].Split(" ");
                            string[] split16 = Lines[l + 16].Split(" ");
                            FinalPhoneNumberRecord fr = new FinalPhoneNumberRecord();
                            if (Lines[l + 1].Length >= 15 && Lines[l + 1].Substring(0, 15) == "Imp por Detalle")
                                fr = new FinalPhoneNumberRecord()
                                {
                                    ImportePorDetalle = float.Parse(split1[3]),
                                    Renta = float.Parse(split2[1]),
                                    TiempoDeAire = float.Parse(split3[3]),
                                    LargaDistancia = float.Parse(split4[2]),
                                    DescuentoTA = float.Parse(split5[2]),
                                    DescuentoLD = float.Parse(split6[2]),
                                    CargosExtras = float.Parse(split7[2]),
                                    TotalDeLlamadas = int.Parse(split8[3]),
                                    DiasDeUso = int.Parse(split9[3]),
                                    SubTotal = float.Parse(split10[2]),
                                    ConsumoSms = float.Parse(split11[2]),
                                    ConsumoGprs = float.Parse(split12[2]),
                                    ConsumoRoaming = float.Parse(split13[2]),
                                    ConsumoRoamingSms = float.Parse(split14[3]),
                                    ConsumoRoamingGprs = float.Parse(split15[3]),
                                    Total = float.Parse(split16[1])
                                };
                            else
                            {
                                phoneNumber.HasDetails = false;
                                fr = new FinalPhoneNumberRecord()
                                {
                                    Renta = float.Parse(split1[1]),
                                    TiempoDeAire = float.Parse(split2[3]),
                                    LargaDistancia = float.Parse(split3[2]),
                                    DescuentoTA = float.Parse(split4[2]),
                                    DescuentoLD = float.Parse(split5[2]),
                                    CargosExtras = float.Parse(split6[2]),
                                    TotalDeLlamadas = int.Parse(split7[3]),
                                    DiasDeUso = int.Parse(split8[3]),
                                    SubTotal = float.Parse(split9[2]),
                                    ConsumoSms = float.Parse(split10[2]),
                                    ConsumoGprs = float.Parse(split11[2]),
                                    ConsumoRoaming = float.Parse(split12[2]),
                                    ConsumoRoamingSms = float.Parse(split13[3]),
                                    ConsumoRoamingGprs = float.Parse(split14[3]),
                                    Total = float.Parse(split15[1])
                                };
                            }
                            phoneNumber.FinalPhoneNumberRecord = fr;
                            report.Add(phoneNumber);
                        }
                    }





                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            factura.Numbers = report;
            return factura;
        }
        private static Duration Duration(string d)
        {
            string[] split = d.Split(":");
            return new Duration(split[0], split[1], split[2]);
        }
        private static DateTime Date(string date)
        {
            string[] splitLine = date.Split("/");
            int day = int.Parse(splitLine[0]);
            int month = Month(splitLine[1]);
            int year = int.Parse("20" + splitLine[2]);
            return new DateTime(year, month, day, 0, 0, 0, 0);
        }
        private static DateTime Time(string time)
        {
            string[] splitLine = time.Split(":");
            int hour = int.Parse(splitLine[0]);
            int minutes = int.Parse(splitLine[1]);
            return new DateTime(1, 1, 1, hour, minutes, 0, 0);
        }
        // Devu staticelve una tupla con el valor del indice y los strings de location y destiny(hay que completar con todos)
        private static Tuple<int, string[]> Location(string[] split)
        {
            int index = 4;
            int final = 4;
            for (int i = index + 1; i < split.Length; i++)
            {
                long o;
                if (long.TryParse(split[i], out o))
                {
                    final = i - 1;
                    break;
                }
            }
            string location = "";
            if (split[index] == "LA")
            {
                location += "LA";
                index++;
                if (split[index] == "HABANA")
                {
                    location += " HABANA";
                    index++;
                }
                else
                {
                    location += "";
                }
            }
            else if (split[index] == "CD.")
            {
                location += "CD.";
                index++;
                if (split[index] == "HABANA")
                {
                    location += " HABANA";
                    index++;
                }
            }

            else if (split[index] == "STGO.")
            {
                location += "STGO.";
                index++;
                if (split[index] == "CUBA")
                {
                    location += " CUBA";
                    index++;
                }
            }
            else if (split[index] == "PINAR")
            {
                location += "PINAR RIO";
                index += 2;
            }
            else if (split[index] == "ARTEMISA")
            {
                location += "ARTEMISA";
                index++;
            }
            else if (split[index] == "GRANMA")
            {
                location += "GRANMA";
                index++;
            }
            else if (split[index] == "MAYABEQUE")
            {
                location += "MAYABEQUE";
                index++;
            }
            else if (split[index] == "MATANZAS")
            {
                location += "MATANZAS";
                index++;
            }
            else if (split[index] == "CIENFUEGOS")
            {
                location += "CIENFUEGOS";
                index++;
            }
            else if (split[index] == "GERONA")
            {
                location += "GERONA";
                index++;
            }
            else if (split[index] == "LAS")
            {
                location += "LAS";
                index++;

                if (split[index] == "TUNAS")
                {
                    location += " TUNAS";
                    index++;
                }
                else
                {

                }
            }
            else if (split[index] == "GUANTANAMO")
            {
                location += "GUANTANAMO";
                index++;
            }
            else
            {
                location = "";
            }
            //falta completar arriba las que falten
            //Lo que queda es el destino:
            string destino = "";
            for (int i = index; i <= final; i++)
            {
                if (i != final)
                    destino += (split[i] + " ");
                else
                    destino += split[i];
            }
            return new Tuple<int, string[]>(final, new string[] { location, destino });
        }
        private static int Month(string m)
        {
            if (m == "JAN")
                return 1;
            if (m == "FEB")
                return 2;
            if (m == "MAR")
                return 3;
            if (m == "APR")
                return 4;
            if (m == "MAY")
                return 5;
            if (m == "JUN")
                return 6;
            if (m == "JUL")
                return 7;
            if (m == "AGO")
                return 8;
            if (m == "SEP")
                return 9;
            if (m == "OCT")
                return 10;
            if (m == "NOV")
                return 11;
            else
                return 12;
        }

    }
}
