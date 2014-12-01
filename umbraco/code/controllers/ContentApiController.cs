using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using code.models;
using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace code.controllers
{
    [JsonOnlyConfiguration]
    public class ContentApiController : UmbracoApiController
    {
        private UmbracoHelper _helper = new UmbracoHelper(UmbracoContext.Current);

        public object GetData(string url, string langKey = "da")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            try
            {
                var urlName = HandleUrlDecoding(url);

                //find siden ud fra url eller returner null
                var content = !string.IsNullOrEmpty(urlName)
                    ? UmbracoContext.Current.ContentCache.GetByXPath(string.Format(@"//*[@isDoc and @urlName=""{0}""]", urlName)).FirstOrDefault()
                    : null;

                if (content != null)
                {
                    if (content.DocumentTypeAlias.ToLower() == "frontpage")
                    {
                        return
                            Request.CreateResponse(JsonMetaResponse.GetSuccessFromObject(content,
                                FrontpageModel.GetFromContent));
                    }
                    else if (content.DocumentTypeAlias.ToLower() == "subpage")
                    {
                        return
                            Request.CreateResponse(JsonMetaResponse.GetSuccessFromObject(content,
                                SubpageModel.GetFromContent));
                    }
                    

                    //smid en 500
                    return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, "Der skete en fejl på serveren." + content.DocumentTypeAlias));
                }
                else
                {
                    //smid en 404
                    return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.NotFound, "Siden fandtes ikke."));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info(typeof(ContentApiController), String.Format("Der skete en fejl: {0}", ex.Message));

                //smid en 500
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, "Der skete en fejl på serveren."));
            }


        }

        public string HandleUrlDecoding(string url)
        {
            url = HttpUtility.UrlDecode(url);

            var urlName = url == "/"
                ? "home"
                : url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();


            if (!string.IsNullOrEmpty(urlName))
            {
                urlName = urlName.Replace(".aspx", "");
                urlName = urlName.ToLower();
            }

            return urlName;
        }
    }
}
