
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using Data.Models;
using System;
using System.Collections.Generic;

namespace MVCPhoneServiceWeb.Utils
{
    public static class SD
    {
        public static string AdminUser = "Admin";
        public static string VisitorUser = "Vistitor";
        public static int NumberPages = 25;
        public static Dictionary<string, int> months = new Dictionary<string, int>() { { "enero", 1 }, { "febrero", 2 }, { "marzo", 3 }, { "abril", 4 }, { "mayo", 5 }, { "junio", 6 }, { "julio", 7 }, { "agosto", 8 }, { "septiembre", 9 }, { "octubre", 10 }, { "noviembre", 11 }, { "diciembre", 12 } };
        public static Dictionary<int, string> Months = new Dictionary<int, string>() { { 1, "enero" }, { 2, "febrero" }, { 3, "marzo" }, { 4, "abril" }, { 5, "mayo" }, { 6, "junio" }, { 7, "julio" }, { 8, "agosto" }, { 9, "septiembre" }, { 10, "octubre" }, { 11, "noviembre" }, { 12, "diciembre" } };
        public static List<string> ProvinceCode = new List<string>() { "7" };
        //diccionarios para pasar parametros a las vistas
        public static Dictionary<string, string> CallingPlanAssignments_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" }, { "cPCheck", "On" } };
        public static Dictionary<string, string> CallingPlans_params = new Dictionary<string, string> { { "cPCheck", "On" }, { "minutesCheck", "On" }, { "messagesCheck", "On" } };
        public static Dictionary<string, string> CostCenterMobileExpenses_params = new Dictionary<string, string> { { "costCenterCodeCheck", "On" }, { "costCenterNameCheck", "On" }, { "callsCheck", "On" }, { "smsCheck", "On" }, { "gprsCheck", "On" }, { "totalCheck", "On" }, { "percentCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" } };
        public static Dictionary<string, string> CostCenterMobileExpensesDetails_params = new Dictionary<string, string> { { "employeeNameCheck", "On" }, { "phoneNumberCheck", "On" }, { "callingPlanIdCheck", "On" }, { "callsCheck", "On" }, { "smsCheck", "On" }, { "gprsCheck", "On" }, { "totalCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" } };
        public static Dictionary<string, string> CostCenterMobileGeneralExpenses_params = new Dictionary<string, string> { { "employeeNameCheck", "On" }, { "phoneNumberCheck", "On" }, { "CostCenterCodeCheck", "On" }, { "costCenterNameCheck", "On" }, { "callingPlanIdCheck", "On" }, { "callsCheck", "On" }, { "smsCheck", "On" }, { "gprsCheck", "On" }, { "totalCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" } };
        public static Dictionary<string, string> CostCenters_params = new Dictionary<string, string> { { "codeCheck", "On" }, { "nameCheck", "On" }, { "managementNameCheck", "On" } };
        public static Dictionary<string, string> DataPlanAssignments_params = new Dictionary<string, string> { { "pNCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" }, { "DPcheck", "On" } };
        public static Dictionary<string, string> DataPlans_params = new Dictionary<string, string> { { "dPCheck", "On" }, { "dataCheck", "On" } };
        public static Dictionary<string, string> Employee_params = new Dictionary<string, string> { { "idCheck", "On" }, { "nameCheck", "On" }, { "costCenterCheck", "On" }, { "personalCodeCheck", "On" }, { "emailCheck", "On" }, { "extensionCheck", "On" } };
        public static Dictionary<string, string> Managements_params = new Dictionary<string, string> { { "nameCheck", "On" } };
        public static Dictionary<string, string> MobilePhoneCalls_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "dateTimeCheck", "On" }, { "AddressCheck", "On" }, { "totalCostCheck", "On" }, { "roamingCheck", "On" } };
        public static Dictionary<string, string> MobilePhoneEmployees_params = new Dictionary<string, string> { { "iMEICheck", "On" }, { "employeeNameCheck", "On" } };
        public static Dictionary<string, string> MobilePhones_params = new Dictionary<string, string> { { "iMEICheck", "On" }, { "modelCheck", "On" } };
        public static Dictionary<string, string> PhoneLineEmployees_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "employeeNameCheck", "On" } };
        public static Dictionary<string, string> PhoneLines_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "pUKCheck", "On" }, { "pINCheck", "On" } };
        public static Dictionary<string, string> Sms_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "dateTimeCheck", "On" }, { "erCheck", "On" }, { "totalCheck", "On" }, { "locationCheck", "On" }, { "destinationCheck", "On" }, { "roamingCheck", "On" } };
        public static Dictionary<string, string> SmsPlansAssignments_params = new Dictionary<string, string> { { "pNCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" }, { "sPCheck", "On" } };
        public static Dictionary<string, string> SmsPlans_params = new Dictionary<string, string> { { "sPCheck", "On" }, { "messagesCheck", "On" }, { "costCheck", "On" } };
        public static Dictionary<string, string> InternationalCalls_params = new Dictionary<string, string> { { "sPCheck", "On" }, { "messagesCheck", "On" }, { "costCheck", "On" } };
        public static Dictionary<string, string> UserWhoHasExceededCallingPlan_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "employeeNameCheck", "On" }, { "callingPlanCheck", "On" }, { "smsPlanCheck", "On" }, { "smsExcCheck", "On" }, { "minPercentCheck", "On" }, { "smsPercentCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" } };
        public static Dictionary<string, string> UserWhoHasExceededDataPlan_params = new Dictionary<string, string> { { "phoneNumberCheck", "On" }, { "employeeNameCheck", "On" }, { "dataPlanCheck", "On" }, { "cccCheck", "On" }, { "ccNameCheck", "On" }, { "monthCheck", "On" }, { "yearCheck", "On" }, { "DataExcCheck", "On" }, { "PercentCheck", "On" } };


        public static Dictionary<int, string> callingPlanAssignment = new Dictionary<int, string>() { { 0, "PhoneNumber" }, { 1, "Month" }, { 2, "Year" }, { 3, "Calling Plan" } };
        public static Dictionary<int, string> callingPlan = new Dictionary<int, string>() { { 0, "Calling Plan" }, { 1, "Minutes" }, { 2, "Cost" } };
        public static Dictionary<int, string> costCenterMPExpenses = new Dictionary<int, string>() { { 0, "CC" }, { 1, "Cost Center" }, { 2, "Calls" }, { 4, "Sms" }, { 6, "GPRS" }, { 12, "Month" }, { 13, "Year" }, { 8, "Total" }, { 10, "Percent" } };
        public static Dictionary<int, string> costCenterMPExpensesDetails = new Dictionary<int, string>() { { 0, "Employee" }, { 1, "Phone Number" }, { 2, "Calling Plan" }, { 3, "Calls" }, { 5, "Sms" }, { 7, "GPRS" }, { 11, "Month" }, { 12, "Year" }, { 9, "Total" } };
        public static Dictionary<int, string> costCenterMPGeneralExpenses = new Dictionary<int, string>() { { 0, "Employee" }, { 1, "Phone Number" }, { 2, "CC" }, { 3, "Cost Center" }, { 4, "Calling Plan" }, { 5, "Calls" }, { 7, "Sms" }, { 9, "GPRS" }, { 11, "Month" }, { 12, "Year" }, { 13, "Total" } };
        public static Dictionary<int, string> costCenter = new Dictionary<int, string>() { { 0, "CC" }, { 1, "Cost Center" }, { 2, "Gerency" } };
        public static Dictionary<int, string> dataPlanAssignment = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "Month" }, { 2, "Year" }, { 3, "Data Plan" } };
        public static Dictionary<int, string> dataPlan = new Dictionary<int, string>() { { 0, "Data Plan" }, { 1, "Data" }, { 2, "Cost" } };
        public static Dictionary<int, string> employee = new Dictionary<int, string>() { { 0, "Name" }, { 3, "Email" }, { 1, "CC" }, { 4, "Extension" }, { 2, "Personal Code" } };
        public static Dictionary<int, string> internationalCalls = new Dictionary<int, string>() { { 1, "Employee" }, { 0, "Phone Number" }, { 3, "CC" }, { 2, "Cost Center" }, { 5, "Month" }, { 6, "Year" }, { 4, "Addresse" }, { 7, "Expense" }, { 9, "PerCent" } };
        public static Dictionary<int, string> management = new Dictionary<int, string>() { { 0, "Gerency" } };
        public static Dictionary<int, string> managementMobilePhoneExpenses = new Dictionary<int, string>() { { 0, "Management" }, { 1, "Calls" }, { 3, "SMS" }, { 5, "GPRS" }, { 7, "Total" }, { 9, "Percent" }, { 11, "Moth" }, { 12, "Year" } };
        public static Dictionary<int, string> mobilePhoneCalls = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "Date Time" }, { 4, "Addressee" }, { 5, "Total Cost" }, { 7, "Roaming Call" } };
        public static Dictionary<int, string> mobilePhoneEmployee = new Dictionary<int, string>() { { 0, "IMEI" }, { 1, "Employee" } };
        public static Dictionary<int, string> mobilePhone = new Dictionary<int, string>() { { 0, "IMEI" }, { 1, "Model" } };
        public static Dictionary<int, string> phoneLineEmployee = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "Employee" } };
        public static Dictionary<int, string> phoneLine = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "PUK" }, { 2, "PIN" }, { 3, "Contract" } };
        public static Dictionary<int, string> smsPlanAssignment = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "Month" }, { 2, "Year" }, { 3, "SMS Plan" } };
        public static Dictionary<int, string> smsPlan = new Dictionary<int, string>() { { 0, "SMS Plan" }, { 1, "Messages" }, { 2, "Cost" } };
        public static Dictionary<int, string> sms = new Dictionary<int, string>() { { 0, "Phone Number" }, { 1, "Date Time" }, { 4, "E_R" }, { 5, "Location" }, { 6, "Destination" }, { 7, "Total" }, { 9, "Roaming" } };
        public static Dictionary<int, string> usersExceededCallingPlan = new Dictionary<int, string>() { { 1, "Employee" }, { 0, "Phone Number" }, { 2, "Calling Plan" }, { 3, "Sms Plan" }, { 4, "Calling Cost Exceded" }, { 6, "Sms Cost Exceded" }, { 12, "Sms %" }, { 10, "Calls %" }, { 8, "Month" }, { 9, "Year" } };
        public static Dictionary<int, string> userExceededDataPlan = new Dictionary<int, string>() { { 1, "Employee" }, { 0, "Phone Number" }, { 3, "CC" }, { 4, "Cost Center" }, { 2, "Data Plan" }, { 7, "Data Cost Exceded" }, { 5, "Month" }, { 6, "Year" }, { 9, "PerCent" } };

        public static string csvString(List<string> headers, List<List<string>> data)
        {
            string csv = "";
            int count = headers.Count;

            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                    csv += headers[i] + "\n";
                else
                    csv += headers[i] + ",";
            }

            int rows = data.Count;

            for (int i = 0; i < rows; i++)
            {
                List<string> row = data[i];
                for (int j = 0; j < count; j++)
                {
                    if (j == count - 1)
                        csv += row[j];
                    else
                        csv += row[j] + ",";
                }
                csv += "\n";
            }

            return csv;
        }
        public static string HttpSessionString(List<string> data)
        {
            string name = "";
            foreach (var s in data)
            {
                name += s;
            }
            return name;
        }

        public static Tuple<bool, string>[] Show(List<string> checks, List<string> filters)
        {
            Tuple<bool, string>[] result = new Tuple<bool, string>[checks.Count];
            for (int i = 0; i < checks.Count; i++)
            {
                var check = (checks[i] != null) ? true : false;
                var filter = (filters[i] != null) ? filters[i] : null;

                result[i] = new Tuple<bool, string>(check, filter);
            }
            return result;
        }
        // mes de entrada y año de entrada y los otros son los de la query
        public static bool DateFilter(int m, int y, int[] monthA, int[] yearA, bool now, bool general = false)
        {
            int month = monthA[0];
            int year = yearA[0];
            if (!general)
            {
                for (int i = 1; i < monthA.Length; i++)
                {
                    if (month != monthA[i])
                        return false;
                }
                for (int i = 1; i < monthA.Length; i++)
                {
                    if (month != monthA[i])
                        return false;
                }
            }
            if (now && m == -1 && y == -1)
            {
                var _month = (DateTime.Now.Month - 1 == 0) ? 12 : DateTime.Now.Month - 1;
                var _year = (DateTime.Now.Month - 1 == 0) ? DateTime.Now.Year - 1 : DateTime.Now.Year;
                if (_month == month && _year == year)
                    return true;
                else
                    return false;
            }
            else
            {
                if (m != -1 && y != -1 && y == year && m == month)
                    return true;
                else if (m == -1 && y != -1 && y == year)
                    return true;
                else if (m != -1 && y == -1 && m == month)
                    return true;
                else if (m == -1 && y == -1)
                    return true;
                else
                    return false;

            }
        }
        //public static class CSVString<T>
        //{

        //    public static string csvString(List<string> headers, List<List<string>> data)
        //    {
        //        string csv = "";
        //        int count = headers.Count;

        //        for (int i = 0; i < count; i++)
        //        {
        //            if (i == count - 1)
        //                csv += headers[i] + "\n";
        //            else
        //                csv += headers[i] + ",";
        //        }

        //        int rows = data.Count;

        //        for (int i = 0; i < rows; i++)
        //        {
        //            List<string> row = data[i];
        //            for (int j = 0; j < count; j++)
        //            {
        //                if (j == count - 1)
        //                    csv += row[j];
        //                else
        //                    csv += row[j] + ",";
        //            }
        //            csv += "\n";
        //        }

        //        return csv;
        //    }
        //}
        public static class AuxiliarFilterClass<TClass>
        {
            public static IEnumerable<TClass> TheFilter(int? id, Func<TClass, int> member, IEnumerable<TClass> items)
            {
                if (id == -1)
                    return items;

                List<TClass> filteredItems = new List<TClass>();

                foreach (var item in items)
                {
                    var f = member(item);
                    if (f == id)
                        filteredItems.Add(item);
                }

                return filteredItems;
            }
            public static IEnumerable<TClass> TheFilter(string id, Func<TClass, string> member, IEnumerable<TClass> items)
            {
                if (id == null)
                    return items;
                List<TClass> filteredItems = new List<TClass>();

                foreach (var item in items)
                {
                    var f = member(item);
                    f = f.ToLower();
                    if (f.Contains(id))
                        filteredItems.Add(item);
                }

                return filteredItems;
            }

        }
    }
}
