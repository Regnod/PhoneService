using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using PDFReader;
using PDFReader.Models;
using Repo;

namespace InvoiceStorer
{
    public class InvoiceStorer
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceStorer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Store(Factura bill)
        {
            if (_dbContext.Bills.Any(a => a.Number == bill.FactNumber))
                throw new Exception("Bill already loaded in database");
            _dbContext.Bills.Add(new Bill()
            {
                Number = bill.FactNumber,
                Month = bill.Date.Month,
                Year = bill.Date.Year
            });

            foreach (var phoneNumber in bill.Numbers)
            {
                var phoneLine = _dbContext.PhoneLines.Find(phoneNumber.Number);
                if (phoneLine == null)
                {
                    _dbContext.PhoneLines.Add(new PhoneLine()
                    {
                        PhoneNumber = phoneNumber.Number,
                        PIN = -1,
                        PUK = -1
                    });
                    phoneLine = _dbContext.PhoneLines.Find(phoneNumber.Number);
                }
                if (phoneNumber.Calls.Count != 0)
                    StoreCall(phoneNumber.Number, phoneNumber.Calls);
                else
                    phoneLine.CallsDetails = false;

                if (phoneNumber.Sms.Count != 0)
                    StoreSms(phoneNumber.Number, phoneNumber.Sms);
                else
                    phoneLine.SMSDetails = false;

                if (phoneNumber.Gprs.Count == 0)
                    //StoreGPRS(phoneNumber.Number, phoneNumber.Gprs);
                    phoneLine.GPRSDetails = false;

                StorePhoneLineSummary(phoneNumber, bill.Date);
                StoreCallingPlanAssigment(phoneNumber, bill.Date);
                StoreDataPlanAssigment(phoneNumber, bill.Date);
                StoreSmsPlanAssigment(phoneNumber, bill.Date);

            }
            _dbContext.SaveChanges();
        }

        private void StoreSmsPlanAssigment(PhoneNumber phoneNumber, DateTime date)
        {
            (int month, int year) lastSmsPlanAssigment = (date.Month != 1) ? (date.Month - 1, date.Year) : (12, date.Year - 1);
            var smsPlanAssigment = _dbContext.SmsPlanAssignments.Find(phoneNumber.Number, lastSmsPlanAssigment.month, lastSmsPlanAssigment.year);
            if (smsPlanAssigment != null)
            {
                _dbContext.SmsPlanAssignments.Add(new SMSPlanAssignment()
                {
                    PhoneNumber = phoneNumber.Number,
                    Month = date.Month,
                    Year = date.Year,
                    SMSPlanId = smsPlanAssigment.SMSPlanId
                });
            }
        }

        private void StoreDataPlanAssigment(PhoneNumber phoneNumber, DateTime date)
        {
            if (phoneNumber.DataPlan == null)
                return;

            var dataPlan = _dbContext.DataPlans.Find(phoneNumber.DataPlan);
            if (dataPlan == null)
            {
                dataPlan = new DataPlan() { DataPlanId = phoneNumber.DataPlan };
                _dbContext.DataPlans.Add(dataPlan);
            }

            _dbContext.DataPlanAssignments.Add(new DataPlanAssignment()
            {
                DataPlanId = dataPlan.DataPlanId,
                PhoneNumber = phoneNumber.Number,
                Month = date.Month,
                Year = date.Year
            });
        }

        private void StoreCallingPlanAssigment(PhoneNumber phoneNumber, DateTime date)
        {
            if (phoneNumber.Plan == null)
                return;

            var plan = _dbContext.CallingPlans.Find(phoneNumber.Plan);
            if (plan == null)
            {
                plan = new CallingPlan() { CallingPlanId = phoneNumber.Plan };
                _dbContext.CallingPlans.Add(plan);
            }

            _dbContext.CallingPlanAssignments.Add(new CallingPlanAssignment()
            {
                CallingPlanId = plan.CallingPlanId,
                PhoneNumber = phoneNumber.Number,
                Month = date.Month,
                Year = date.Year
            });
        }

        private void StorePhoneLineSummary(PhoneNumber phoneNumber, DateTime dateTime)
        {
            _dbContext.PhoneLineSummaries.Add(new PhoneLineSummary()
            {
                PhoneNumber = phoneNumber.Number,
                Month = dateTime.Month,
                Year = dateTime.Year,
                ImportByDetails = phoneNumber.FinalPhoneNumberRecord.ImportePorDetalle,
                Rent = phoneNumber.FinalPhoneNumberRecord.Renta,
                AirTime = phoneNumber.FinalPhoneNumberRecord.TiempoDeAire,
                LongDistance = phoneNumber.FinalPhoneNumberRecord.LargaDistancia,
                DiscountTA = phoneNumber.FinalPhoneNumberRecord.DescuentoTA,
                DiscountLD = phoneNumber.FinalPhoneNumberRecord.DescuentoLD,
                ExtraCharges = phoneNumber.FinalPhoneNumberRecord.CargosExtras,
                TotalCalls = phoneNumber.FinalPhoneNumberRecord.TotalDeLlamadas,
                DayOfUse = phoneNumber.FinalPhoneNumberRecord.DiasDeUso,
                SubTotal = phoneNumber.FinalPhoneNumberRecord.SubTotal,
                SmsExpenses = phoneNumber.FinalPhoneNumberRecord.ConsumoSms,
                GprsExpenses = phoneNumber.FinalPhoneNumberRecord.ConsumoGprs,
                RoamingExpenses = phoneNumber.FinalPhoneNumberRecord.ConsumoRoaming,
                RoamingSmsExpenses = phoneNumber.FinalPhoneNumberRecord.ConsumoRoamingSms,
                RoamingGprsExpenses = phoneNumber.FinalPhoneNumberRecord.ConsumoRoamingGprs,
                Total = phoneNumber.FinalPhoneNumberRecord.Total
            });
        }

        private void StoreSms(string phoneNumber, IEnumerable<Sms> phoneNumberSms)
        {
            foreach (var sms in phoneNumberSms)
            {
                _dbContext.SMS.Add(new SMS()
                {
                    PhoneNumber = phoneNumber,
                    DateTime = new DateTime
                    (
                        sms.Date.Year,
                        sms.Date.Month,
                        sms.Date.Day,
                        sms.Time.Hour,
                        sms.Time.Minute,
                        sms.Time.Second),
                    E_R = sms.E_R,
                    Location = sms.Location,
                    Amount = sms.Monto,
                    LD = sms.LD,
                    Charge = sms.Charge,
                    Destination = sms.PhoneNumber,
                    Discount = sms.Discount,
                    Roaming = sms.RoamingSms,
                    Total = sms.Total
                });
            }
        }

        private void StoreCall(string phoneNumber, IEnumerable<Call> calls)
        {
            foreach (var call in calls)
            {
                _dbContext.MobilePhoneCalls.Add(new MobilePhoneCall()
                {
                    PhoneNumber = phoneNumber,
                    Addressee = call.PhoneNumber,
                    Charge = call.Charge,
                    TotalCost = call.Total,
                    DateTime = new DateTime(
                        call.Date.Year,
                        call.Date.Month,
                        call.Date.Day,
                        call.Time.Hour,
                        call.Time.Minute,
                        call.Time.Second),
                    Discount = call.Discount,
                    Duration = call.Duration.Hours * 60 + call.Duration.Minutes, // minutes
                    LD = call.LD,
                    RoamingCall = call.RoamingCall,
                    TA = call.TA,
                });
            }
        }
    }
}