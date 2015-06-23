using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingDefaceDAO;
using TrackingDefaceDTO;
using TrackingDefaceBUS;
using System.Media;

namespace TrackingDefaceBUS.Application
{
    public class Define
    {
        TextContentBUS objectTextContentBUS = new TextContentBUS();
        WebDAO webDAO = new WebDAO();
        EmailBUS emailBUS = new EmailBUS();

        public void CayQuyetDinh (int _case, int webID, string _textContent)
        {
            string result = "";
            Web web = new Web();
            /* Trong so quyet dinh webSite co bi hack khong */
            int hour = DateTime.Now.Hour;
            if (hour > 19 && _case > 0 || hour < 6 && _case > 0)
            {
                // Canh bao ngay lap tuc
                web = webDAO.GetEmailByWebID(webID);
                web.webStatus = "Warning";
                webDAO.UpdateStatus(web);
                objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                /*Canh bao bang moi gia'*/
                emailBUS.SendEmail(web, "WebSite cua ban chac chan da bi tan cong");
            }
            else
            {
                switch (_case)
                {
                    case 0:
                        result = "Safe";
                        Console.WriteLine("Website an toan");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        web.webID = webID;
                        web.webStatus = "Safe";
                        webDAO.UpdateStatus(web);
                        break;

                    case 1:
                        result = "Safe";
                        Console.WriteLine("Website an toan");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        web.webID = webID;
                        web.webStatus = "Safe";
                        webDAO.UpdateStatus(web);
                        break;

                    case 2:
                        result = "Warning";
                        /* Send email sms */
                        web = webDAO.GetEmailByWebID(webID);
                        web.webStatus = result;
                        emailBUS.SendEmail(web, "Website co the bi hack muc do 2");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        webDAO.UpdateStatus(web);
                        PlaySound();
                        break;
                    case 3:
                        result = "Warning";
                        web = webDAO.GetEmailByWebID(webID);
                        emailBUS.SendEmail(web, "Website bi thay doi noi dung va hinh anh muc do 3");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        web.webStatus = result;
                        webDAO.UpdateStatus(web);
                        break;
                    case 4:
                        result = "Warning";
                        emailBUS.SendEmail(web, "Website bi thay doi noi dung va hinh anh muc do 4");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        web.webStatus = result;
                        webDAO.UpdateStatus(web);
                        PlaySound();
                        break;
                    case 5:
                        result = "Warning";
                        emailBUS.SendEmail(web, "Website bi tan cong o muc do 5");
                        objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                        web.webStatus = result;
                        webDAO.UpdateStatus(web);
                        PlaySound();
                        break;
                }

                if (_case > 5)
                {
                    web = webDAO.GetEmailByWebID(webID);
                    web.webStatus = "Warning";
                    webDAO.UpdateStatus(web);
                    objectTextContentBUS.UpdateTextContent(webID, _textContent, result);
                    /*Canh bao bang moi gia'*/
                    emailBUS.SendEmail(web, "WebSite cua ban chac chan da bi tan cong");
                    PlaySound();
                }
            }
            
        }

        public void PlaySound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\ád\Documents\GitHub\DoAnChuyenNganh\GetDomTree\TrackingDefaceDTO\bellRing.wav");
            simpleSound.Play();
        }
    }
}
