namespace BankTech_Model.Model.ViewModels.Filter
{
    public class AccountTransectionFilter : BaseFilter
    {
        public string accountNoFrom { set; get; }
        public string accountNoTo { set; get; }
        public decimal amount { set; get; }
        public int transectionType { set; get; }
    }
}
