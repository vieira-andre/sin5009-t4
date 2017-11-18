using System.Web;
using System.Web.Mvc;

namespace SIN5009.T4a.Corretora
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
