using BankTech_Model.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BankTech_Core.Utility.Providers.DB
{
    public class SQL : IDb
    {
        public static IConfiguration Configuration;
        public SQL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private string strConnection;
        public SqlTransaction trans { set; get; }
        public SqlConnection conn { set; get; }
        public SqlCommand cmd { set; get; }
        public ConDb conDb { set; get; }
        public string message { set; get; }

        enum transState : int
        {
            None = 0,
            Begin = 1,
            Commit = 2,
            Rollback = 3
        }
        transState transStatus { set; get; }
        public SQL(SQL pSql = null, ConDb condb = ConDb.CONDB_DEFAULT)
        {
            this.conDb = condb;
            if (pSql == null)
            {
                this.strConnection = Configuration.GetConnectionString("DBContext");
                this.conn = new SqlConnection(this.strConnection);
            }
            else
            {
                this.strConnection = pSql.strConnection;
                this.conn = pSql.conn;
                this.cmd = pSql.cmd;
                this.trans = pSql.trans;
                this.transStatus = pSql.transStatus;
            }

            this.conn.InfoMessage += new SqlInfoMessageEventHandler(eventConnectionInfoMessage);

        }

        void eventConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.message += e.Message;
        }

        public bool Open()
        {
            try
            {
                if (this.conn.State != ConnectionState.Open)
                {
                    this.conn.Open();
                    this.cmd = new SqlCommand();
                    cmd.Connection = this.conn;
                    cmd.CommandTimeout = 600;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Close()
        {
            try
            {
                if (this.transStatus == transState.None)
                {
                    this.conn.Close();
                }
                else
                {
                    if (this.transStatus == transState.Commit)
                    {
                        this.conn.Close();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Begin(IsolationLevel isolationLv = IsolationLevel.ReadCommitted)
        {
            try
            {
                this.transStatus = transState.Begin;
                this.trans = this.conn.BeginTransaction(isolationLv);
                this.cmd.Transaction = trans;
                return true;
            }
            catch (Exception ex)
            {
                this.transStatus = transState.None;
                throw ex;
            }
        }

        public bool Commit()
        {
            try
            {
                this.transStatus = transState.Commit;
                this.trans.Commit();
                if (this.conn.State == ConnectionState.Open)
                {
                    Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                this.transStatus = transState.None;
                throw ex;
            }
        }
        public bool RollBack()
        {
            try
            {
                this.transStatus = transState.Rollback;
                this.trans.Rollback();
                if (this.conn.State == ConnectionState.Open)
                {
                    Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                this.transStatus = transState.None;
                throw ex;
            }
        }
    }
}
