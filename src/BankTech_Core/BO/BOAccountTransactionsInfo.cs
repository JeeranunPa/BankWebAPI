using BankTech_Core.Base;
using BankTech_Model;
using BankTech_Model.Enums;
using BankTech_Model.Model.ViewModels;
using BankTech_Model.Model.ViewModels.Filter;
using BankTech_Model.Models.System;
using System;
using System.Collections.Generic;

namespace BankTech_Core.BO
{
    public class BOAccountTransactionsInfo : BaseBO
    {
        public ResultDataModels AddAccountTransectionInfo(AccountTransectionFilter filter)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {

                ResultDataModels resultAccountInfo = this.GenAccountTransection(filter);
                if (!resultAccountInfo.success)
                {
                    throw new Exception(resultAccountInfo.msg);
                }
                AccountViewModels accountInfo = (AccountViewModels)resultAccountInfo.data;

                this.iDb.Open();
                this.iDb.Begin();

                try
                {

                    BOAccountTransection boAccountTracsection = new BOAccountTransection(this.iDb);
                    if (accountInfo.accountTransection?.Count > 0)
                    {
                        foreach (var acctransection in accountInfo.accountTransection)
                        {
                            ResultDataModels resultAccountTracsection = boAccountTracsection.AddAccountTransection(acctransection);
                            if (resultAccountTracsection.success == false)
                            {
                                throw new Exception(resultAccountTracsection.msg);
                            }

                        }

                    }
                    BOAccount boAccount = new BOAccount(this.iDb);
                    if (accountInfo.account?.Count > 0)
                    {
                        foreach (var account in accountInfo.account)
                        {
                            ResultDataModels resultAccount = boAccount.UpdateAccount(account);
                            if (resultAccount.success == false)
                            {
                                throw new Exception(resultAccount.msg);
                            }

                        }

                    }
                    result.data = new
                    {
                        transection = new
                        {
                            success = true,
                            msg = "Transection has Complete, Amount total : " + accountInfo.amount.ToString(),
                        }
                    };
                    this.iDb.Commit();
                }
                catch (Exception ex)
                {
                    this.iDb.RollBack();
                    result.msg = ex.Message.ToString();
                }
                finally
                {
                    this.iDb.Close();
                }

            }
            catch (Exception ex)
            {
                result.msg = ex.Message.ToString();
            }

            return result;
        }
        public ResultDataModels GenAccountTransection(AccountTransectionFilter filter)
        {
            ResultDataModels result = new ResultDataModels();

            try
            {
                decimal amount = filter.amount;
                string accountNo = filter.accountNoFrom;
                string accountTranfer = filter.accountNoTo;
                int transectionType = filter.transectionType;
                if (string.IsNullOrWhiteSpace(accountNo)) { throw new Exception("Account is not null"); }
                AccountViewModels accountInfo = new AccountViewModels
                {
                    accountTransection = new List<AccountTransactionsModels>(),
                    account = new List<AccountModels>(),
                };

                BOAccount boAcc = new BOAccount();
                ResultDataModels resultGetAcc = boAcc.GetAccount(accountNo);
                AccountModels account = (AccountModels)resultGetAcc.data;
                if (string.IsNullOrWhiteSpace(account?.accountNo) == true) { throw new Exception("Account is not correct, please contact....."); }
                if (amount <= 0) { throw new Exception("Please input your money!"); }
                decimal chargedFee = (decimal)0.1;

                if (transectionType == (int)AccountType.DEPOSIT)
                {
                    decimal amountCal = Math.Round((((decimal)amount * ((decimal)100 - chargedFee)) / (decimal)100), 2);
                    decimal amountFeeCal = Math.Round(((amount * chargedFee) / 100), 2);
                    accountInfo.accountTransection.Add(new AccountTransactionsModels
                    {
                        acountId = account.acountId.ToInt(),
                        companyId = account.companyId,
                        officeId = account.officeId,
                        amount = amountCal,
                        transactionsTypeId = (int)AccountType.DEPOSIT,
                        amountFee = amountFeeCal,
                        accountNo = accountNo,
                    });
                    accountInfo.account.Add(new AccountModels
                    {
                        acountId = account.acountId.ToInt(),
                        companyId = account.companyId,
                        officeId = account.officeId,
                        amount = account.amount.ToDecimal() + amountCal,
                        accountNo = accountNo,
                        amountFee = account.amountFee.ToDecimal() + amountFeeCal,
                    });
                    accountInfo.amount = account.amount.ToDecimal() + amountCal;

                }
                else if (transectionType == (int)AccountType.TRANSFER)
                {

                    ResultDataModels resultGetAccTo = boAcc.GetAccount(accountTranfer);
                    AccountModels accountTo = (AccountModels)resultGetAccTo.data;
                    if (string.IsNullOrWhiteSpace(accountTo?.accountNo) == true) { throw new Exception("Tranfer account is not correct, please contact....."); }
                    if ((account.amount.ToDecimal() - amount) < 0) { throw new Exception("Can't tranfer amount avalible " + account.amount); }
                    accountInfo.accountTransection.Add(new AccountTransactionsModels
                    {
                        accountNo = account.accountNo,
                        companyId = account.companyId,
                        officeId = account.officeId,
                        transactionsTypeId = (int)AccountType.TRANSFER,
                        tranferFlag = "TR",
                        tranferTo = accountTranfer,
                        amount = amount,
                    });

                    accountInfo.accountTransection.Add(new AccountTransactionsModels
                    {
                        accountNo = accountTo.accountNo,
                        companyId = accountTo.companyId,
                        officeId = accountTo.officeId,
                        transactionsTypeId = (int)AccountType.TRANSFER,
                        tranferFlag = "RC",
                        amount = amount,
                        tranferFrom = accountNo
                    });

                    accountInfo.account.Add(new AccountModels
                    {
                        accountNo = account.accountNo,
                        companyId = account.companyId,
                        officeId = account.officeId,
                        amount = account.amount.ToDecimal() - amount,
                        amountFee = account.amountFee,
                    });

                    accountInfo.account.Add(new AccountModels
                    {
                        accountNo = accountTo.accountNo,
                        companyId = accountTo.companyId,
                        officeId = accountTo.officeId,
                        amount = accountTo.amount.ToDecimal() + amount,
                    });

                }
                else
                {
                    throw new Exception("Please select transection Type!");

                }

                result.data = accountInfo;
                result.success = true;

            }
            catch (Exception ex)
            {
                result.msg = ex.Message.ToString();
            }

            return result;

        }

    }
}
