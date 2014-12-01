using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code.models.helper;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace code.models
{
    public class FrontpageModel : MasterModel
    {
        public IEnumerable<ImageModel> ContentImages { get; set; }

        public string ContentBody { get; set; }


        public static FrontpageModel GetFromContent(IPublishedContent a)
        {
            return new FrontpageModel
            {
                Id = a.Id,
                Name = a.Name,
                ContentImages = ImageModel.GetImages(a, "contentImages"),
                ContentBody = a.GetPropertyValue<string>("contentBody"),
                AngularTemplateUrl = "/ng-views/frontpage.html",    //could be done from Umbraco
                Created = a.CreateDate,
                Updated = a.UpdateDate
            };        
        }
    }
}
