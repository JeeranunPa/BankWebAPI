public static class IntExtensions
{
    public static int ToInt(this int? number)
    {
        if (number == null)
        {
            return 0;
        }
        else
        {
            return (int)number;
        }
    }

    public static decimal ToDecimal(this decimal? number)
    {
        if (number == null)
        {
            return 0;
        }
        else
        {
            return (decimal)number;
        }
    }
}

