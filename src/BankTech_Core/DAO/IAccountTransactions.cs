using BankTech_Model;
using BankTech_Model.Models.System;

namespace BankTech_Core.DAO
{
    interface IAccountTransactions
    {
        ResultDbModels Insert(AccountTransactionsModels acctran);
    }
}
