using System;

namespace Accounting
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public decimal Amount { get; set; }

        public int DaysInBudget()
        {
            var firstDayOfBudget = FirstDay();
            return DateTime.DaysInMonth(firstDayOfBudget.Year, firstDayOfBudget.Month);
        }

        public DateTime FirstDay()
        {
            return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);
        }

        public decimal DailyAmount()
        {
            return Amount / DaysInBudget();
        }

        public DateTime LastDay()
        {
            var daysInMonth = DateTime.DaysInMonth(FirstDay().Year,FirstDay().Month);
            return DateTime.ParseExact(YearMonth + daysInMonth, "yyyyMMdd", null);
        }
    }
}