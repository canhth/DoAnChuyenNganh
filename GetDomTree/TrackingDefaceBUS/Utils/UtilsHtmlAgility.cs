using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace TrackingDefaceBUS.Utils
{
    public class UtilsHtmlAgility
    {
        int i, j, k, loi, saiSo;
        HtmlDocument doc = new HtmlDocument();

        public string GetChildLink(string url)
        {
            String str2;
            // Call the page and get the generated HTML
            HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
            doc.OptionWriteEmptyNodes = true;
            try
            {
                var webRequest = HttpWebRequest.Create(url);
                Stream stream = webRequest.GetResponse().GetResponseStream();
                doc.Load(stream);
                stream.Close();

            }
            catch (System.UriFormatException uex)
            {
                Console.WriteLine("There was an error in the format of the url", uex);
                throw;
            }
            catch (System.Net.WebException wex)
            {
                Console.WriteLine("There was an error connecting to the url: ", wex);
                throw;
            }
            string output = doc.DocumentNode.OuterHtml;

            Console.WriteLine("Chuoi chua bo HTML");
            Console.WriteLine(output.Length);

            str2 = Regex.Replace(output, "<.*?>", string.Empty);

            Console.WriteLine("Chuoi da bo HTML");
            Console.WriteLine(str2.Length);  
            return "";
        }

        public string GetChildLink ()
        {
            string links = "";
            var output1 = doc.DocumentNode.SelectNodes("//a[@class='tmenu']");
            foreach (var ii in output1)
            {
                var data = ii.Attributes["href"].Value;
                links += data.ToString();
                Console.WriteLine(data);
            }
            return links;
        }

        // Thuat toan
        public bool SoSanh(string s1, string s2)
        {
            saiSo = (int)Math.Round(s1.Length * 0.3);
            Console.WriteLine("Sai so la");
            Console.WriteLine(saiSo);
            if (s1.Length < (s2.Length - saiSo) || s1.Length > (s2.Length + saiSo))

                return false;

            i = j = loi = 0;

            while (i < s2.Length && j < s1.Length)
            {

                if (s2[i] != s1[j])
                {

                    loi++;

                    for (k = 1; k <= saiSo; k++)
                    {

                        if ((i + k < s2.Length) && s2[i + k] == s1[j])
                        {
                            i += k;
                            loi += k - 1;
                            break;
                        }
                        else if ((j + k < s1.Length) && s2[i] == s1[j + k])
                        {
                            j += k;
                            loi += k - 1;
                            break;
                        }
                    }
                }
                i++;
                j++;
            }
            loi += s2.Length - i + s1.Length - j;
            Console.WriteLine("Loi:");
            Console.WriteLine(loi);
            if (loi <= saiSo)
                return true;
            else return false;
        }
    }
}