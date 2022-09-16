using System;

namespace BankTech_Model
{
    public class AccountTransactionsModels
    {
        public int? transactionsId { set; get; }
        public string accountNo { set; get; }
        public int? transactionsTypeId { set; get; }
        public int? acountId { set; get; }
        public int? companyId { set; get; }
        public int? officeId { set; get; }
        public string tranferTo { set; get; }
        public string tranferFrom { set; get; }
        public DateTime? trandte { set; get; }
        public string tranferFlag { set; get; }
        public decimal? amount { set; get; }
        public decimal? amountFee { set; get; }
        public decimal? amountFeecal { set; get; }
        public string recStatus { set; get; }
        public string createBy { set; get; }
        public DateTime? createDate { set; get; }
        public string createFrom { set; get; }
    }
}
