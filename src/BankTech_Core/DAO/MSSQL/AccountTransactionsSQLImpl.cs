using BankTech_Core.Utility;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model;
using BankTech_Model.Models.System;
using System;
using System.Data.SqlClient;

namespace BankTech_Core.DAO.MSSQL
{
    class AccountTransactionsSQLImpl : IAccountTransactions
    {
        private SQL sql;
        private Utils utils;

        public AccountTransactionsSQLImpl(SQL pSql = null)
        {
            if (pSql == null)
            {
                this.sql = new SQL();
            }
            else
            {
                this.sql = pSql;
            }

            this.utils = new Utils();
        }
        public ResultDbModels Insert(AccountTransactionsModels acctran)
        {
            ResultDbModels result = new ResultDbModels();
            this.sql.Open();
            try
            {
                string cmd = @"
                    
                    INSERT INTO AccountTransactions (
                        accountNo,
                        transactionsTypeId,
						companyId,
                        officeId,
						trandte,
                        tranferFlag,
                        tranferFrom,
                        tranferTo,
                        amount,
						amountFee,
						createBy,
						createDate
                    ) 
                    VALUES (
                        @accountNo,
                        @transactionsTypeId,
						@companyId,
                        @officeId,
						GetDate(),
                        @tranferFlag,
                        @tranferFrom,
                        @tranferTo,
                        @amount,
						@amountFee,
                        @createBy,
                        GetDate()
                    )
                
                ";
                this.sql.cmd.CommandText = cmd;
                this.sql.cmd.Parameters.Clear();
                this.sql.cmd.Parameters.AddValue("accountNo", acctran.accountNo);
                this.sql.cmd.Parameters.AddValue("transactionsTypeId", acctran.transactionsTypeId);
                this.sql.cmd.Parameters.AddValue("companyId", acctran.companyId);
                this.sql.cmd.Parameters.AddValue("officeId", acctran.officeId);
                this.sql.cmd.Parameters.AddValue("tranferFlag", acctran.tranferFlag);
                this.sql.cmd.Parameters.AddValue("tranferFrom", acctran.tranferFrom);
                this.sql.cmd.Parameters.AddValue("tranferTo", acctran.tranferTo);
                this.sql.cmd.Parameters.AddValue("amount", acctran.amount);
                this.sql.cmd.Parameters.AddValue("amountFee", acctran.amountFee);
                this.sql.cmd.Parameters.AddValue("createBy", acctran.createBy);

                Log.writeLogSql(this.sql.cmd);
                SqlDataReader dr = this.sql.cmd.ExecuteReader();
                result = utils.ToResultDbModels(dr);


            }
            catch (Exception ex)
            {
                result.msg = ex.Message.ToString();
            }
            finally
            {
                this.sql.Close();
            }

            return result;
        }

    }
}
