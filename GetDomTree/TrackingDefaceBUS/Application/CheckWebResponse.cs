using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceBUS.Application
{
    public class CheckWebResponse
    {
        /* Method Check by Ping website with time out 3000 */
        public bool Ping(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                Ping ping = new Ping();
                PingReply pingReply = ping.Send(uri.Host, 3000);
                if (pingReply.Status != IPStatus.Success)
                {
                    // Website does not available.
                    Console.WriteLine("Khong ket noi duoc");
                    return false;
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Loi ket noi voi URL");
                return false;
            }
        }

        /* Method check HttpWebRequest with timeout 3000 */
        public bool isOnline(string URL)
        {
            bool result = false;

            WebRequest webRequest = WebRequest.Create(URL);
            webRequest.Timeout = 3000; // miliseconds
            webRequest.Method = "HEAD";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                result = true;
            }
            catch (WebException webException)
            {
                Console.WriteLine(URL + " doesn't exist: " + webException.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        public bool GetPage(string url)
        {
            bool result = false;
            try
            {
                // Creates an HttpWebRequest for the specified URL. 
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                // Sends the HttpWebRequest and waits for a response.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("\r\nResponse Status Code is OK and StatusDescription is: {0}",
                                         myHttpWebResponse.StatusDescription);
                    result = true;
                    return result;
                }
                else
                    result = false;
                // Releases the resources of the response.
                myHttpWebResponse.Close();
                return result;
            }
            catch (WebException e)
            {
                Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
                return result;
            }
        }
    }
}
