using Umbraco.Core.Models;
using Umbraco.Web;

namespace code.models
{
    public class GlobalModel
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }

        public GlobalModel()
        {
        }

        public GlobalModel(IPublishedContent content)
        {
            var rootNode = content.AncestorOrSelf(1);

            CompanyName = rootNode.GetPropertyValue<string>("companyName");
            CompanyAddress = rootNode.GetPropertyValue<string>("companyAddress");
            CompanyPhone = rootNode.GetPropertyValue<string>("companyPhone");
            CompanyEmail = rootNode.GetPropertyValue<string>("companyEmail");
        }
    }
}