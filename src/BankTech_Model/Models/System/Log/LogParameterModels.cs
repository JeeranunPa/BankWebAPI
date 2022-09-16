using System;
using System.Collections.Generic;

namespace BankTech_Model
{
    public class TrackLogMethodModels
    {
        public string callMethod { set; get; }
        public List<LogParameterModels> listParameter { set; get; }
        public DateTime callDateTime { set; get; }
        public object returnValue { set; get; }

        public TrackLogMethodModels()
        {
            this.listParameter = new List<LogParameterModels>();
        }
    }
    public class LogParameterModels
    {
        public string name { set; get; }
        public string type { set; get; }
        public object value { set; get; }
    }
}
