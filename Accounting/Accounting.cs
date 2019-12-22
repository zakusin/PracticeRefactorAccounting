using System;
using System.Linq;

namespace Accounting
{
    internal class Accounting
    {
        public decimal QueryBudget(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return 0;

            var totalBudget = 0m;
            if (startDate.Year == endDate.Year)
            {
                if (startDate.Month == endDate.Month)
                {
                    var budget = Repo.GetAll().FirstOrDefault(model => model.YearMonth == startDate.ToString("yyyyMM"));
                    if (budget != null)
                    {
                        var overlappingDays = endDate.Subtract(startDate).Days + 1;
                        var daysInBudget = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                        return budget.Amount / daysInBudget * overlappingDays;
                    }
                    return 0;
                }
            }

            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);

            var i = 0;

            while (true)
            {
                if (currentDate > endDate)
                    break;
                if (i == 0)
                {
                    totalBudget += BudgetOfMonth(startDate,
                        DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1);
                }
                else if (currentDate.Year == endDate.Year && currentDate.Month == endDate.Month)
                {
                    totalBudget += BudgetOfMonth(endDate, endDate.Day);
                }
                else
                {
                    totalBudget += BudgetOfMonth(currentDate, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                currentDate = currentDate.AddMonths(1);
                i++;
            }

            return totalBudget;
        }

        private decimal BudgetOfMonth(DateTime startDate, int days)
        {
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

            var budget = Repo.GetAll().FirstOrDefault(model => model.YearMonth == startDate.ToString("yyyyMM"));
            if (budget != null) return budget.Amount / daysInMonth * days;
            return 0;
        }


        public IBudgetRepo Repo { get; set; }
    }
}