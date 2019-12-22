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
            if (IsSameYearMonth(startDate, endDate))
            {
                var budget = Repo.GetAll().FirstOrDefault(model => model.YearMonth == startDate.ToString("yyyyMM"));
                if (budget != null)
                {
                    var overlappingDays = OverlappingDays(startDate, endDate);
                    return budget.DailyAmount() * overlappingDays;
                }
                return 0;
            }

            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);

            while (currentDate <= endDate)
            {
                if (IsSameYearMonth(startDate, currentDate))
                {
                    var budget = FindBudget(currentDate);
                    if (budget != null)
                    {
                        var overlappingEnd = budget.LastDay();
                        var overlappingDays = OverlappingDays(startDate, overlappingEnd);
                        totalBudget += budget.DailyAmount() * overlappingDays;
                    }
                }
                else if (IsSameYearMonth(endDate, currentDate))
                {
                    totalBudget += BudgetOfMonth(endDate, endDate.Day);
                }
                else
                {
                    totalBudget += BudgetOfMonth(currentDate, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                currentDate = currentDate.AddMonths(1);
            }

            return totalBudget;
        }

        private Budget FindBudget(DateTime startDate)
        {
            return Repo.GetAll().FirstOrDefault(model => model.YearMonth == startDate.ToString("yyyyMM"));
        }

        private static bool IsSameYearMonth(DateTime x, DateTime y)
        {
            return x.ToString("yyyyMM") == y.ToString("yyyyMM");
        }

        private static int OverlappingDays(DateTime overlappingStart, DateTime overlappingEnd)
        {
            return overlappingEnd.Subtract(overlappingStart).Days + 1;
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