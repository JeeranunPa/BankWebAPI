using BankTech_Core.Base;
using BankTech_Model;
using BankTech_Model.Models.System;
using System;
using System.Threading.Tasks;


namespace BankTech_Core.BO
{
    public class BOAccountInfo : BaseBO
    {
        public async Task<ResultDataModels> AddAccountInfo(AccountModels cusInfo)
        {
            ResultDataModels result = new ResultDataModels();

            try
            {
                BOGenerateIBAN boIban = new BOGenerateIBAN();
                cusInfo.accountNo = await boIban.GenerateIBAN();
                result = this.AddAccount(cusInfo);
            }
            catch (Exception ex)
            {
                result.msg = ex.Message.ToString();

            }
            return result;
        }
        public ResultDataModels AddAccount(AccountModels cusInfo)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {

                BOAccount boCustomer = new BOAccount();
                ResultDataModels resultCus = boCustomer.AddAccount(cusInfo);
                if (resultCus.success == false)
                {
                    throw new Exception(resultCus.msg);
                }
                result.data = new
                {
                    accountInfo = new
                    {
                        success = true,
                        msg = "Creata Account Complete, AccountNo : " + cusInfo.accountNo,
                    }
                };
            }
            catch (Exception ex)
            {
                result.msg = ex.Message.ToString();
            }

            return result;
        }
    }
}
