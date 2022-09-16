using BankTech_Core.Utility;
using BankTech_Core.Utility.Providers.DB;
using BankTech_Model;
using System;
using System.Data.SqlClient;
using System.Text;

namespace BankTech_Core.DAO.MSSQL
{
    class ConfigMasterSQLImpl : IConfigMaster
    {
        private SQL sql;
        private Utils utils;
        public ConfigMasterSQLImpl(SQL pSql = null)
        {
            if (pSql == null)
            {
                this.sql = new SQL();
            }
            else
            {
                this.sql = pSql;
            }

            this.utils = new Utils();
        }
        public ConfigMasterModels SelectConfig(string configCode)
        {
            this.sql.Open();

            try
            {
                StringBuilder cmd = new StringBuilder();
                cmd.Append(@"

                            select * from Configmaster 
                            where configCode=@configCode
                                  and recStatus='A'    

                          ");

                this.sql.cmd.CommandText = cmd.ToString();
                this.sql.cmd.Parameters.Clear();
                this.sql.cmd.Parameters.AddValue("configCode", configCode);
                SqlDataReader dr = this.sql.cmd.ExecuteReader();
                ConfigMasterModels data = utils.AsModel<ConfigMasterModels>(dr);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sql.Close();
            }
        }

    }
}
