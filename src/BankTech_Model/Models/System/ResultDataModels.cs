namespace BankTech_Model.Models.System
{
    public class ResultDataModels
    {
        private string __codeMsg;
        private string __typeMsg;
        public int total { set; get; }
        public int status { set; get; }
        public string codeMsg
        {
            set
            {
                this.__codeMsg = value;
            }
            get
            {
                if (this.__codeMsg == null)
                {
                    this.__codeMsg = "SYS-000000";
                }
                return this.__codeMsg;
            }
        }
        public object data { set; get; }
        public string msg { set; get; }
        public bool success { set; get; }
        public string detail { set; get; }
        public string msgType { set; get; }
        public string error { set; get; }
        public int? codeErrorMsg { set; get; }
        public int? groupMsg { set; get; }

        public ResultDataModels(string msg = "", bool success = false, string detail = "")
        {
            this.msg = msg;
            this.success = success;
            this.detail = detail;
        }
    }
}
