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
    public class BOAccount : BaseBO
    {
        private IAccount iAcc;
        public BOAccount(IDb p_iDb = null) : base(p_iDb)
        {
            switch (this.dbType)
            {
                case DbType.ORCL:
                    break;
                default:
                    this.iAcc = new AccountSQLImpl((SQL)this.iDb);
                    break;
            }
        }
        public ResultDataModels AddAccount(BankTech_Model.AccountModels account)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {
                ResultDbModels resultDb = iAcc.Insert(account);
                if (resultDb.status != ResultDbModels.st.SUCCESS)
                {
                    throw new Exception(resultDb.msg);
                }
                else
                {
                    result.success = true;
                    result.data = resultDb;
                    result.msg = "Insert Account Success";
                }
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }

            return result;
        }
        public ResultDataModels GetAccount(string accountNo)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {
                AccountModels data = iAcc.SelectByAccount(accountNo);
                if (data == null)
                {
                    throw new Exception("Data not found");
                }
                else
                {
                    result = this.utils.RenderData<AccountModels>(data);
                }
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }

            return result;
        }
        public ResultDataModels UpdateAccount(AccountModels account)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {
                ResultDbModels resultDb = iAcc.UpdateAccount(account);
                if (resultDb.status != ResultDbModels.st.SUCCESS)
                {
                    throw new Exception(resultDb.msg);
                }
                else
                {
                    result.success = true;
                    result.data = resultDb;
                    result.msg = "Update Account Success";
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
