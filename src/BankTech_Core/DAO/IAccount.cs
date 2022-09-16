using BankTech_Model;
using BankTech_Model.Models.System;

namespace BankTech_Core.DAO
{
    interface IAccount
    {
        ResultDbModels Insert(AccountModels account);
        AccountModels SelectByAccount(string accountNo);
        ResultDbModels UpdateAccount(AccountModels account);
    }
}
