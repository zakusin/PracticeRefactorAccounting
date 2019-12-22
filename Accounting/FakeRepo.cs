using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    class FakeRepo : IBudgetRepo
    {
        public List<Budget> GetAll()
        {
            var list = new List<Budget>();
            var dec09 = new Budget {Amount = 30, YearMonth = "201909"};

            list.Add(dec09);

            var dec10 = new Budget {Amount = 300, YearMonth = "201911"};

            list.Add(dec10);

            var dec11 = new Budget();

            dec11.Amount = 3100;
            dec11.YearMonth = "201912";
            list.Add(dec11);

            var dec01 = new Budget();

            dec01.Amount = 31000;
            dec01.YearMonth = "202001";
            list.Add(dec01);

            var dec202012 = new Budget();

            dec202012.Amount = 310000;
            dec202012.YearMonth = "202012";
            list.Add(dec202012);

            return list;
        }
    }
}
