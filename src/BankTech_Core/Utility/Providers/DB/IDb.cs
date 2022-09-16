using System.Data;

namespace BankTech_Core.Utility.Providers.DB
{
    public interface IDb
    {
        bool Open();
        bool Close();
        bool Begin(IsolationLevel isolationLv = IsolationLevel.ReadCommitted);
        bool Commit();
        bool RollBack();
    }
}
