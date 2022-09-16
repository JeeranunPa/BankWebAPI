using BankTech_Core.Utility;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model.Enums;
using Microsoft.Extensions.Configuration;

namespace BankTech_Core.Base
{
    public class BaseBO
    {
        public static IConfiguration Configuration;
        public BaseBO(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IDb iDb { set; get; }
        protected Utils utils { set; get; }
        public DbType dbType { set; get; }
        public BaseBO(IDb p_iDb = null, ConDb conDb = ConDb.CONDB_DEFAULT, DbType? fixedType = null)
        {

            if (p_iDb != null)
            {
                this.iDb = p_iDb;
            }
            else
            {
                if (fixedType != null)
                {
                    this.dbType = (DbType)fixedType;
                    switch (this.dbType)
                    {
                        case DbType.ORCL:
                            //this.iDb = new ORCL(null, conDb);
                            break;
                        default:
                            this.iDb = new SQL(null, conDb);
                            break;
                    }
                }
                else
                {
                    string db = Configuration.GetConnectionString("DBContext");
                    switch (db.ToUpper())
                    {
                        //case "ORCL":
                        //    //this.iDb = new ORCL(null, conDb);
                        //    //dbType = DbType.ORCL;
                        //    break;
                        default:
                            this.iDb = new SQL(null, conDb);
                            dbType = DbType.SQL;
                            break;
                    }
                }

            }

            this.utils = new Utils();
        }
    }
}
