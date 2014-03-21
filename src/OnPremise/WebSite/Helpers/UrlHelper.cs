using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Thinktecture.IdentityServer.Web.Helpers
{
    public static class UrlHelper
    {
        /// <summary>
        /// Creates a validation summary with a container element 
        /// surrounding the summary and error messages.
        /// </summary>        
        public static string ForgotPasswordUrl(this HtmlHelper helper)
        {
            return ConfigurationManager.AppSettings["FORGOT_PASSWORD_URL"];	
        }
    }
}