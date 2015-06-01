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
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;


namespace TrackingDefaceBUS.Utils
{
    public class UtilsHtmlAgility
    {
        public static List<string> ImageList;
        public static List<string> ImageHash;
        // Creating a list array
        //public static byte[] encryptData(string data)
        //{
        //    System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //    byte[] hashedBytes;
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
        //    return hashedBytes;
        //}
        //public static string md5(string data)
        //{
        //    return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        //}

        static string getMd5Hash(byte[] buffer)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(buffer);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        static byte[] imageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public static string GetContent(string url)
        {
            string str2;
            
            HtmlDocument doc = new HtmlDocument();
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

            GetChildLink(doc, url);

            str2 = Regex.Replace(output, "<.*?>", string.Empty);
            str2 = str2.Trim();
 
            return str2;
        }

        public static List<string> GetChildLink(HtmlDocument doc, string url)
        {
            string links = "";          
            ImageList = new List<string>();
            var output1 = doc.DocumentNode.SelectNodes("//a[@class='tmenu']");
            foreach (var image in doc.DocumentNode.SelectNodes("//img"))
            {
                var src = image.GetAttributeValue("src", null);
                links = url + src.ToString();
                ImageList.Add(links);
                Console.WriteLine(links);              
            }
            return ImageList;
        }
    
        public static List<string> HashImage ()
        {
            ImageHash = new List<string>();
            string hash = "";
            var client = new WebClient();
            foreach (string url in ImageList)
            {
                 byte[] data = client.DownloadData(url);
                 using (MemoryStream mem = new MemoryStream(data)) 
                {
                    var yourImage = Image.FromStream(mem) ; 
                    // If you want it as Png
                    yourImage.Save("D:/TestImage/image.png", ImageFormat.Jpeg) ;
                    Image imagee = Image.FromFile(@"D:/TestImage/image.png");
                    byte[] buffer = imageToByteArray(imagee);
                    hash = getMd5Hash(buffer);
                    ImageHash.Add(hash);
                 }
            }
            return ImageHash;
        }
            
        

        // Thuat toan
        public static bool  SoSanh(string s1, string s2)
        {
            int i, j, k, loi, saiSo;
            saiSo = (int)Math.Round(s1.Length * 0.4);
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