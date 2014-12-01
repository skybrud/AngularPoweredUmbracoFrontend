using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace code
{
    public static class Extensions
    {
        /// <summary>
        /// Konvertere en csv streng til et int array
        /// </summary>
        /// <param name="source">Kommasepareret streng</param>
        /// <returns></returns>
        public static int[] CsvToInt(this string source)
        {
            try
            {
                return source.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(a => int.Parse(a)).ToArray();
            }
            catch (Exception)
            {

                return new int[] { };
            }
        }

        /// <summary>
        /// Tjekke med en boolean om siden er skjult (umbracoNaviHide)
        /// </summary>
        /// <param name="content">IPublishedContent</param>
        /// <returns></returns>
        public static bool Hidden(this IPublishedContent content)
        {
            return content.GetPropertyValue<bool>("umbracoNaviHide");
        }

        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]).ToString() + str.Substring(1);
        }

        /// <summary>
        /// Returnere et IPublishedContent objekt ud fra en egenskab der indeholder et enkelt id. Bruges f.eks på en Content Picker egenskab
        /// </summary>
        /// <param name="content">IPublishedContent</param>
        /// <param name="property">Egenskabens alias</param>
        /// <returns></returns>
        public static IPublishedContent TypedContent(this IPublishedContent content, string property)
        {
            var id = content.GetPropertyValue<int>(property);

            if (id == 0)
                return null;

            return UmbracoContext.Current.ContentCache.GetById(id);
        }

        /// <summary>
        /// Returnere et IPublishedContent objekt fra MediaCache. Egenskaben skal indeholde et enkelt id
        /// </summary>
        /// <param name="content">IPublishedContent</param>
        /// <param name="property">Egenskabens alias</param>
        /// <returns></returns>
        public static IPublishedContent TypedMedia(this IPublishedContent content, string property)
        {
            var id = content.GetPropertyValue<int>(property);

            if (id == 0)
                return null;

            try
            {
                var item = UmbracoContext.Current.MediaCache.GetById(id);

                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returnere en liste af IPublishedContent objekter. Bruges på f.eks. MNTP der gemmer i CSV format.
        /// </summary>
        /// <param name="content">IPublishedContent</param>
        /// <param name="property">Egenskabens alias</param>
        /// <returns></returns>
        public static IEnumerable<IPublishedContent> TypedCsvContent(this IPublishedContent content, string property)
        {
            return content.GetPropertyValue<string>(property).CsvToInt().Select(a => UmbracoContext.Current.ContentCache.GetById(a)).Where(a => a != null);
        }

        /// <summary>
        /// Returnere en liste af IPublishedContent objekter fra MediaCache. Bruges på f.eks. DAMP
        /// </summary>
        /// <param name="content">IPublishedContent</param>
        /// <param name="property">Egenskabens alias</param>
        /// <returns></returns>
        public static IEnumerable<IPublishedContent> TypedCsvMedia(this IPublishedContent content, string property)
        {
            var ids = content.GetPropertyValue<string>(property).CsvToInt();

            List<IPublishedContent> result = new List<IPublishedContent>();

            foreach (var id in ids)
            {
                try
                {
                    var item = UmbracoContext.Current.MediaCache.GetById(id);
                    if (item != null)
                        result.Add(item);
                }
                catch (Exception)
                {
                }
            }

            return result;
        }
    }
}
