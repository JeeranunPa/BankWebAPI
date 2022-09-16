using System;
using System.ComponentModel.DataAnnotations;
namespace BankTech_Model
{
    public class AccountModels
    {

        public int? acountId { set; get; }
        public string accountNo { set; get; }
        [Required]
        public string idNo { set; get; }
        public int companyId { set; get; }
        public int officeId { set; get; }
        [Required]
        public string accountName { set; get; }
        [Required]
        public string firstName { set; get; }
        public string lastName { set; get; }
        [Required]
        public int? accountTypId { set; get; }
        [Required]
        public string email { set; get; }
        [Required]
        public string mobile { set; get; }
        public DateTime? birthDate { set; get; }
        public DateTime? issueDate { set; get; }
        public DateTime? expiryDate { set; get; }
        public decimal? amount { set; get; }
        public decimal? amountFee { set; get; }
        public string emailFlag { set; get; }
        public string smsFlag { set; get; }
        public string recStatus { set; get; }
        public string createBy { set; get; }
        public DateTime? createDate { set; get; }
        public string createFrom { set; get; }
        public string updateBy { set; get; }
        public DateTime? updateDate { set; get; }
        public string updateFrom { set; get; }

    }
}
