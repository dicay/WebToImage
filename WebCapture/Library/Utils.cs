using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebCapture.Library
{
    public class Utils
    {
        public static string GetFileName(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
              return  Regex.Replace(url, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (url.ToLower().StartsWith("https://") || url.ToLower().StartsWith("http://"))
                {
                    return url;
                }
                else
                {
                    //default return HTTP
                    return "http://" + url;
                }
            }
            else
            {
                return string.Empty;
            } 
        }
    }
}