using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class TotalCostController : Controller
    {
        private ApplicationDbContext _context;

        public TotalCostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CostCenterExpenseLandline(string month, string year)
        {
            int _month = Utils.Utils.IntTryParse(month);
            int _year = Utils.Utils.IntTryParse(year);
            var join =await  _context.Employees.Join(
                _context.LandLinePhoneCalls,
                a => a.EmployeeId,
                b => b.EmployeeId,
                (a, b) => new
                {
                    CostCenter = a.CostCenter,
                    Expense = b.LandlinePhoneCallCost,
                    Date = b.LandlinePhoneCallDateTime
                }).ToListAsync();
            
            if (_month != -1 && _year != 1)
            {
                join = join.Where(a => a.Date.Year == _year && a.Date.Month == _month).ToList();
            }
            List<TotalCostViewModel> viewModel = new List<TotalCostViewModel>();
            if (!join.Any()) return View(viewModel);
            
            Dictionary<string,int> costCenterExpenses = new Dictionary<string, int>();
            foreach (var item in join)
            {
                int expenses;
                if (costCenterExpenses.TryGetValue(item.CostCenter, out expenses))
                {
                    costCenterExpenses[item.CostCenter] = expenses + item.Expense;
                }
                else
                {
                    costCenterExpenses.Add(item.CostCenter, item.Expense);
                }
            }

            foreach (var (key, value) in costCenterExpenses)
            {
                viewModel.Add(new TotalCostViewModel(){CostCenter = key,Expense = value});
            }

            return View(viewModel);
        }

        public async  Task<IActionResult> CostCenterExpenseMobile(string month, string year)
        {
            int _month = Utils.Utils.IntTryParse(month);
            int _year = Utils.Utils.IntTryParse(year);

            var join =await _context.MobilePhoneCalls.Join(
                _context.PhoneLineEmployees,
                a => a.PhoneNumber,
                b => b.PhoneNumber,
                (a, b) => new
                {
                    EmployeeId = b.EmployeeId,
                    MobilePhoneCallCost = a.MobilePhoneCallCost,
                    Date = a.DateTime
                }).ToListAsync();
            
            var join2 =  join.Join(
                await _context.Employees.ToListAsync(),
                a => a.EmployeeId,
                b => b.EmployeeId,
                (a, b) => new
                {
                    CostCenter = b.CostCenter,
                    Expense = a.MobilePhoneCallCost,
                    Date = a.Date
                });
            
            if (_month != -1)
            {
                join2 = join2.Where(a => a.Date.Month == _month);
            }
            if (_year != -1)
            {
                join2 = join2.Where(a => a.Date.Year == _year);
            }
            
            List<TotalCostViewModel> viewModel = new List<TotalCostViewModel>();
            if (!join2.Any()) return View(viewModel);
            
            Dictionary<string,int> costCenterExpenses = new Dictionary<string, int>();
            foreach (var item in join2)
            {
                int expenses;
                if (costCenterExpenses.TryGetValue(item.CostCenter, out expenses))
                {
                    costCenterExpenses[item.CostCenter] = expenses + item.Expense;
                }
                else
                {
                    costCenterExpenses.Add(item.CostCenter, item.Expense);
                }
            }

            foreach (var (key, value) in costCenterExpenses)
            {
                viewModel.Add(new TotalCostViewModel(){CostCenter = key,Expense = value});
            }
            return View(viewModel);
        }
    }
}