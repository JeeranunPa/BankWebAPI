using BankTech_Model;

namespace BankTech_Core.DAO
{
    interface IConfigMaster
    {
        ConfigMasterModels SelectConfig(string configCode);
    }
}
