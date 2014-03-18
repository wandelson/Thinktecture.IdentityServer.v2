using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Thinktecture.IdentityServer.Web.Helpers
{
    public static class SummaryHelper
    {
        /// <summary>
        /// Creates a validation summary with a container element 
        /// surrounding the summary and error messages.
        /// </summary>        
        public static HtmlString ValidationSummaryWithContainer(this HtmlHelper ext, string message)
        {
            var summaryoutput = ext.ValidationSummary(message);

            if (ext.ViewContext.HttpContext.Request.IsAjaxRequest())
            {
                if (!ext.ViewData.ModelState.IsValid)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("<div class='alert alert-error'><button type='button' class='close' data-dismiss='alert'>×</button>");
                    sb.Append(summaryoutput);
                    sb.Append("</div>");
                    return new HtmlString(sb.ToString());
                }
                else
                    return new HtmlString("");
            }
            else
            {
                if (!ext.ViewData.ModelState.IsValid)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("<div class='alert alert-error'><button type='button' class='close' data-dismiss='alert'>×</button>");
                    sb.Append(summaryoutput);
                    sb.Append("</div>");
                    return new HtmlString(sb.ToString());
                }
                else
                    return new HtmlString("");
            }
        }
    }
}