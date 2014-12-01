using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace code.models
{
    public class SubpageModel : MasterModel
    {


        public static SubpageModel GetFromContent(IPublishedContent a)
        {
            return new SubpageModel
            {
                Id = a.Id,
                Name = a.Name,
                AngularTemplateUrl = "",
                Created = a.CreateDate,
                Updated = a.UpdateDate
            };
        }
    }
}
