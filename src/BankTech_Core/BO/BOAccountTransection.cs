using BankTech_Core.Base;
using BankTech_Core.DAO;
using BankTech_Core.DAO.MSSQL;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model;
using BankTech_Model.Enums;
using BankTech_Model.Models.System;
using System;

namespace BankTech_Core.BO
{
    public class BOAccountTransection : BaseBO
    {
        private IAccountTransactions IAccountTran;
        public BOAccountTransection(IDb p_iDb = null) : base(p_iDb)
        {
            switch (this.dbType)
            {
                case DbType.ORCL:
                    break;
                default:
                    this.IAccountTran = new AccountTransactionsSQLImpl((SQL)this.iDb);
                    break;
            }
        }
        public ResultDataModels AddAccountTransection(AccountTransactionsModels customer)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {
                ResultDbModels resultDb = IAccountTran.Insert(customer);
                if (resultDb.status != ResultDbModels.st.SUCCESS)
                {
                    throw new Exception(resultDb.msg);
                }
                else
                {
                    result.success = true;
                    result.data = resultDb;
                    result.msg = "Insert Transection Success";
                }
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }

            return result;
        }

    }
}
