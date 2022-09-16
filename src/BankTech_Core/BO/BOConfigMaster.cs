using BankTech_Core.Base;
using BankTech_Core.DAO;
using BankTech_Core.DAO.MSSQL;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model;
using BankTech_Model.Enums;
using BankTech_Model.Models.System;
using System;

namespace BankTech_Core.BO
{
    public class BOConfigMaster : BaseBO
    {
        private readonly IServiceProvider _sp;
        public BOConfigMaster(IServiceProvider sp)
        {
            _sp = sp;
        }
        private IConfigMaster iConfig;
        public BOConfigMaster(IDb p_iDb = null) : base(p_iDb)
        {
            switch (this.dbType)
            {
                case DbType.ORCL:
                    break;
                default:
                    this.iConfig = new ConfigMasterSQLImpl((SQL)this.iDb);
                    break;
            }
        }
        public ResultDataModels GetConfigMaster(string configCode)
        {
            ResultDataModels result = new ResultDataModels();
            try
            {
                ConfigMasterModels data = iConfig.SelectConfig(configCode);
                if (data == null)
                {
                    throw new Exception("Not data generate IBAN found.");
                }
                else
                {
                    result = this.utils.RenderData<ConfigMasterModels>(data);
                }
            }
            catch (Exception ex)
            {
                result.error = ex.Message.ToString();
                result.msg = ex.Message.ToString();
            }

            return result;
        }

    }
}
