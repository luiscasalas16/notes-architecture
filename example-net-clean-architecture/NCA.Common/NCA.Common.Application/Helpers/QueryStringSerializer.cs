using System.Web;

namespace NCA.Common.Application.Helpers
{
    public static class QueryStringHelper
    {
        public static string Serialize<T>(T obj)
        {
            var properties =
                from p in obj?.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select $"{HttpUtility.UrlEncode(p.Name)}" + $"={HttpUtility.UrlEncode(p.GetValue(obj)?.ToString())}";

            return string.Join("&", properties);
        }
    }
}
