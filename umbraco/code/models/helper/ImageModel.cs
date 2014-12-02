using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace code.models.helper
{
    public class ImageModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("croppedUrl")]
        public string CroppedUrl { get; set; }

        [JsonProperty("altText")]
        public string AltText { get; set; }

        private static readonly string DomainName = string.Format("http://{0}", HttpContext.Current.Request.Url.Host);


        public static ImageModel GetImage(IPublishedContent content, string property, int width = 800, int height = 600)
        {
            var image = content.TypedCsvMedia(property).FirstOrDefault();

            if (image == null)
                return null;

            return new ImageModel
            {
                Id = image.Id,
                Url = DomainName + image.Url,
                CroppedUrl = DomainName + image.GetCropUrl(width, height, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop),
                AltText = image.HasValue("alttext") ? image.GetPropertyValue<string>("alttext") : ""
            };
        }


        public static IEnumerable<ImageModel> GetImages(IPublishedContent content, string property, int width = 800, int height = 600)
        {
            var imagesData = content.TypedCsvMedia(property);

            if (imagesData == null)
                return null;

            return imagesData.Select(m => new ImageModel
            {
                Id = m.Id,
                Url = DomainName + m.Url,
                CroppedUrl = DomainName + m.GetCropUrl(width, height, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop),
                AltText = m.HasValue("alttext") ? m.GetPropertyValue<string>("alttext") : ""
            }).ToList();
        }
    }
}
