using BankTech_Core.Utility;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model;
using BankTech_Model.Models.System;
using System;
using System.Data.SqlClient;
using System.Text;

namespace BankTech_Core.DAO.MSSQL
{
    class AccountSQLImpl : IAccount
    {
        private SQL sql;
        private Utils utils;

        public AccountSQLImpl(SQL pSql = null)
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
        public ResultDbModels Insert(AccountModels account)
        {
            ResultDbModels result = new ResultDbModels();
            this.sql.Open();
            try
            {
                string cmd = @"
                    
                    INSERT INTO Account (
                        accountNo,
						idno,
                        companyId,
						officeId,
                        accountName,
						firstName,
						lastName,
						accountTypId,
						email,
                        mobile,
                        birthDate,
						issueDate,
                        expiryDate,
                        amount,
                        amountFee,
                        emailFlag,
                        smsFlag,
                        createBy,
                        createDate
                    ) 
                    VALUES (
                        @accountNo,
						@idno,
                        @companyId,
						@officeId,
                        @accountName,
						@firstName,
						@lastName,
						@accountTypId,
						@email,
                        @mobile,
                        @birthDate,
						@issueDate,
                        @expiryDate,
                        @amount,
                        @amountFee,
                        @emailFlag,
                        @smsFlag,
                        @createBy,
                        GetDate()
                    )
                
                ";
                this.sql.cmd.CommandText = cmd;
                this.sql.cmd.Parameters.Clear();
                this.sql.cmd.Parameters.AddValue("accountNo", account.accountNo);
                this.sql.cmd.Parameters.AddValue("idNo", account.idNo);
                this.sql.cmd.Parameters.AddValue("companyId", account.companyId);
                this.sql.cmd.Parameters.AddValue("officeId", account.officeId);
                this.sql.cmd.Parameters.AddValue("accountName", account.accountName);
                this.sql.cmd.Parameters.AddValue("firstName", account.firstName);
                this.sql.cmd.Parameters.AddValue("lastName", account.lastName);
                this.sql.cmd.Parameters.AddValue("accountTypId", account.accountTypId);
                this.sql.cmd.Parameters.AddValue("email", account.email);
                this.sql.cmd.Parameters.AddValue("mobile", account.mobile);
                this.sql.cmd.Parameters.AddValue("birthDate", account.birthDate);
                this.sql.cmd.Parameters.AddValue("issueDate", account.issueDate);
                this.sql.cmd.Parameters.AddValue("expiryDate", account.expiryDate);
                this.sql.cmd.Parameters.AddValue("amount", account.amount);
                this.sql.cmd.Parameters.AddValue("amountFee", account.amountFee);
                this.sql.cmd.Parameters.AddValue("emailFlag", account.emailFlag);
                this.sql.cmd.Parameters.AddValue("smsFlag", account.smsFlag);
                this.sql.cmd.Parameters.AddValue("createBy", account.createBy);

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
        public AccountModels SelectByAccount(string accountNo)
        {
            this.sql.Open();

            try
            {
                StringBuilder cmd = new StringBuilder();
                cmd.Append(@"

                            select * from Account 
                            where accountNo=@accountNo
                            and recStatus='A'

                          ");

                this.sql.cmd.CommandText = cmd.ToString();
                this.sql.cmd.Parameters.Clear();
                this.sql.cmd.Parameters.AddValue("accountNo", accountNo);

                SqlDataReader dr = this.sql.cmd.ExecuteReader();
                AccountModels data = utils.AsModel<AccountModels>(dr);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sql.Close();
            }
        }
        public ResultDbModels UpdateAccount(AccountModels account)
        {
            ResultDbModels result = new ResultDbModels();
            this.sql.Open();
            try
            {
                string cmd = @"
                    update Account set 
                        amount=@amount,
                        amountFee=@amountFee,
                        updateBy=@updateBy,
                        updateDate=GETDATE()
                    WHERE accountNo =@accountNo 
                ";

                this.sql.cmd.CommandText = cmd;
                this.sql.cmd.Parameters.Clear();
                this.sql.cmd.Parameters.AddValue("amount", account.amount);
                this.sql.cmd.Parameters.AddValue("amountFee", account.amountFee);
                this.sql.cmd.Parameters.AddValue("updateBy", account.updateBy);
                this.sql.cmd.Parameters.AddValue("accountNo", account.accountNo);

                Log.writeLogSql(this.sql.cmd);
                int affrow = this.sql.cmd.ExecuteNonQuery();
                result = utils.ToResultDbModels(affrow);
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
