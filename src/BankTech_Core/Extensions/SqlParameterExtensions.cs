using System;
using System.Data.SqlClient;

public static class SqlParameterExtensions
{
    public static void AddValue(this SqlParameterCollection parms, string parameterName, object value)
    {
        if (value == null)
        {
            parms.AddWithValue(parameterName, DBNull.Value);
        }
        else
        {
            parms.AddWithValue(parameterName, value);
        }

    }
}
