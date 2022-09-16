using System.Data;

namespace BankTech_Model.Models.System
{
    public class ResultDbModels
    {
        public enum st
        {
            NONE = 0,
            SUCCESS = 1,
            NOAFFECT = 2,
            ERROR = 3
        }
        public st status { set; get; }
        public string msg { set; get; }
        public int affectRow { set; get; }
        public int lastId { set; get; }
        public DataTable data { set; get; }

        public ResultDbModels(st st = st.NONE, string msg = "")
        {
            this.status = st;
            this.msg = msg;
        }

    }
}
