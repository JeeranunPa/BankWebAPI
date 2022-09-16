using System.Data.SqlClient;

public static class SqlCommandExtensions
{

    public static void AddCommandText(this SqlCommand cmd, string cmdText)
    {
        cmd.CommandText = cmdText;
    }

}
