using BankTech_Model.Models.System;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BankTech_Core.Utility
{
    public class Utils
    {
        public T AsModel<T>(SqlDataReader dr) where T : new()
        {
            try
            {
                if (dr.HasRows == false)
                {
                    throw new Exception("Data empty.");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);


                    var drFirst = dt.Rows.Cast<System.Data.DataRow>().Take(1).FirstOrDefault();
                    if (drFirst != null)
                    {
                        DataTable dtNew = dt.Clone();
                        var drs = dtNew.NewRow();

                        for (int i = 0; i < drFirst.ItemArray.Count(); i++)
                        {
                            drs[i] = drFirst[i];
                        }
                        dtNew.Rows.Add(drs);

                        JsonSerializerSettings settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            Formatting = Formatting.None,
                            DateFormatHandling = DateFormatHandling.IsoDateFormat
                        };
                        settings.Converters.Add(new JsonNumberConverter());
                        string json = JsonConvert.SerializeObject(dtNew, settings);
                        json = json.Substring(1, json.Length - 2);
                        T data = JsonConvert.DeserializeObject<T>(json);
                        return data;
                    }
                    else
                    {
                        throw new Exception("Not Data found");
                    }

                }
            }
            catch
            {
                return default(T);
            }
        }
        public ResultDataModels RenderData<T>(T data, string msg = "")
        {
            ResultDataModels result = new ResultDataModels(msg);
            try
            {
                result.data = data;
                if (data == null)
                {
                    result.total = 0;
                    result.success = false;
                }
                else
                {
                    result.total = 1;
                    result.success = true;
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                result.detail = ex.Message.ToString();
            }

            return result;

        }
        public ResultDbModels ToResultDbModels(int affrow, string msg = "")
        {
            ResultDbModels result = new ResultDbModels(msg: msg);
            if (affrow == 0)
            {
                result.status = ResultDbModels.st.NOAFFECT;
                result.msg = "No affect records.";
            }
            else if (affrow > 0)
            {
                result.status = ResultDbModels.st.SUCCESS;
                result.affectRow = affrow;
            }
            else if (affrow == -1 && msg == "prc")
            {
                result.status = ResultDbModels.st.SUCCESS;
                result.affectRow = affrow;
            }
            else
            {
                result.status = ResultDbModels.st.ERROR;
                result.msg = "error records.";
            }
            return result;
        }
        public ResultDbModels ToResultDbModels(SqlDataReader dr)
        {
            try
            {
                ResultDbModels result = new ResultDbModels();
                int rowAffected = dr.RecordsAffected;
                if (dr.HasRows == true)
                {
                    rowAffected = 1;
                    result.data = new DataTable();
                    result.data.Load(dr);
                    if (result.data.Columns.Contains("lastId") == true)
                    {
                        result.lastId = (int)result.data.Rows[result.data.Rows.Count - 1]["lastId"];
                    }
                }

                result.affectRow = rowAffected;

                if (rowAffected == 0)
                {
                    result.status = ResultDbModels.st.NOAFFECT;
                }
                else if (rowAffected > 0)
                {
                    result.status = ResultDbModels.st.SUCCESS;
                }
                else
                {
                    result.status = ResultDbModels.st.ERROR;
                }

                if (dr.IsClosed == false)
                {
                    dr.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }


    public class JsonNumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(double) | objectType == typeof(decimal));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double numVal = Convert.ToDouble(value);
            if ((numVal % 1) == 0)
            {
                writer.WriteValue(Convert.ToInt64(numVal).ToString());
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }
}