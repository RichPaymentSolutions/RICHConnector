using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace RICH_Connector.Helper
{
    public static class HttpHelper
    {
        public static string GetStringFromUrl(string url)
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString(url);
            return downloadString;
           
        }

        public static bool ContainsXHTML(this string input)
        {
            try
            {
                XElement x = XElement.Parse("<wrapper>" + input + "</wrapper>");
                return !(x.DescendantNodes().Count() == 1 && x.DescendantNodes().First().NodeType == XmlNodeType.Text);
            }
            catch (XmlException ex)
            {
                return true;
            }
        }

        public static string ConvertXHTMLEntities(this string input)
        {
            // Convert all ampersands to the ampersand entity.
            string output = input;
            output = output.Replace("&amp;", "amp_token");
            output = output.Replace("&", "&amp;");
            output = output.Replace("amp_token", "&amp;");

            // Convert less than to the less than entity (without messing up tags).
            output = output.Replace("< ", "&lt; ");
            return output;
        }

    }
}
