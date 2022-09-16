using System.Collections.Generic;

namespace BankTech_Model.Model.ViewModels
{
    public class AccountViewModels
    {
        public decimal amount { set; get; }
        public List<AccountModels> account { set; get; }
        public List<AccountTransactionsModels> accountTransection { set; get; }

    }
}
