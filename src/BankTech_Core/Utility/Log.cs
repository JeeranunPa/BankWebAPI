using KPTech_Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace BankTech_Core.Utility
{
    public class Log
    {
        public static IConfiguration Configuration;
        public Log(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public class TraceLogModels
        {
            public TraceLogReqeustModels traceRequest { set; get; }
            public string traceResult { set; get; }

            public TraceLogModels()
            {
                this.traceRequest = new TraceLogReqeustModels();
            }
        }
        public class TraceLogReqeustModels
        {
            public string requestUrl { set; get; }
            [JsonIgnore]
            public string contentData { set; get; }
            public string requestData { set; get; }
            public string requestPostData { set; get; }
            public string requestGetData { set; get; }
            public string headers { set; get; }
            public int statusContentData
            {
                get
                {
                    if (String.IsNullOrEmpty(this.contentData) == false)
                    {
                        return (int)StatusConentData.HAVE_CONTENT_DATA;
                    }
                    else
                    {
                        return (int)StatusConentData.NONE_CONTENT_DATA;
                    }
                }
            }
            public string method { set; get; }
        }
        public static bool writeLogSql(SqlCommand cmd, string fixDirPath = "")
        {
            try
            {
                string dirLog;

                string msg = cmd.CommandText.ToUpper();
                int cntPara = 1;
                foreach (SqlParameter prm in cmd.Parameters)
                {
                    string para = prm.ParameterName.ToUpper();
                    if (cmd.CommandType != CommandType.StoredProcedure)
                    {
                        if (msg.Contains("@" + para))
                        {
                            para = "@" + para;
                        }

                        switch (prm.SqlDbType)
                        {
                            case SqlDbType.Bit:
                                int boolToInt = (bool)prm.Value ? 1 : 0;
                                msg = msg.Replace(para, string.Format("{0}", (bool)prm.Value ? 1 : 0));
                                break;
                            case SqlDbType.Int:
                                msg = msg.Replace(para, string.Format("{0}", prm.Value));
                                break;
                            case SqlDbType.VarChar:
                                msg = msg.Replace(para, string.Format("'{0}'", prm.Value));
                                break;
                            default:
                                msg = msg.Replace(para, string.Format("'{0}'", prm.Value));
                                break;
                        }
                    }
                    else
                    {
                        msg += string.Format("'{0}'{1}", prm.Value, (cntPara < cmd.Parameters.Count) ? "," : "");
                    }
                    cntPara++;
                }


                string cfg_dirLog = Configuration.GetConnectionString("PathDirLogSQL");
                if (String.IsNullOrEmpty(fixDirPath) == true)
                {
                    dirLog = cfg_dirLog;
                }
                else
                {
                    dirLog = fixDirPath;
                }

                string pathLog = String.Format("{0}{1}/", dirLog, DateTime.Now.ToString("yyyyMMdd"));

                if (Directory.Exists(pathLog) == false)
                {
                    Directory.CreateDirectory(pathLog);
                }

                string fileName = "sql_statement_" + DateTime.Now.ToString("HHmm") + ".txt";
                string fullPath = String.Format("{0}{1}", pathLog, fileName);

                StringBuilder text = new StringBuilder();

                FileInfo _file = new FileInfo(fullPath);
                if (_file.Exists == true)
                {
                    if (_file.Length > 0)
                    {
                        text.AppendLine(",");
                    }
                }


                text.AppendLine("Sql Statement : {");
                text.AppendLine(msg);
                text.AppendLine("}");
                StreamWriter SW = new StreamWriter(fullPath, true);
                SW.Write(text);
                SW.Close();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool writeLogApi(TraceLogModels trace, string fixDirPath = "")
        {
            try
            {
                string dirLog;

                if (String.IsNullOrEmpty(fixDirPath) == false)
                {
                    dirLog = fixDirPath;
                }
                else
                {
                    string cfg_dirLog = Configuration.GetConnectionString("PathDirLogAPI");
                    if (String.IsNullOrEmpty(cfg_dirLog) == true)
                    {
                        dirLog = cfg_dirLog;
                    }
                    else
                    {
                        dirLog = fixDirPath;
                    }
                }

                string pathLog = String.Format("{0}{1}/", dirLog, DateTime.Now.ToString("yyyyMMdd"));

                if (Directory.Exists(pathLog) == false)
                {
                    Directory.CreateDirectory(pathLog);
                }

                string fileName = "" + DateTime.Now.ToString("HH") + ".txt";
                string fullPath = String.Format("{0}{1}", pathLog, fileName);

                StringBuilder text = new StringBuilder();
                FileInfo _file = new FileInfo(fullPath);
                if (_file.Exists == true)
                {
                    if (_file.Length > 0)
                    {
                        text.Append(",");
                    }
                }
                string requestId = Guid.NewGuid().ToString();

                text.AppendLine("{");
                text.AppendLine("\"logDateTime\": \"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ") + "\",");
                text.AppendLine("\"logRequest\": " + JsonConvert.SerializeObject(trace.traceRequest, Formatting.Indented) + ",");
                text.AppendLine("\"logResult\": " + (String.IsNullOrEmpty(trace.traceResult) == true ? "\"\"" : trace.traceResult));
                text.AppendLine("\"logHeaders\": " + (String.IsNullOrEmpty(trace.traceRequest.headers) == true ? "\"\"" : trace.traceRequest.headers));
                text.Append("}");

                StreamWriter SW = new StreamWriter(fullPath, true);
                SW.Write(text);
                SW.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
