using System.Collections.Generic;

namespace Accounting
{
    internal interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}