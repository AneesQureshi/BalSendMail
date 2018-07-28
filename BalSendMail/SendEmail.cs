using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BalSendMail
{
    public class SendEmail
    {
        public List<string> toEmail { get; set; }
        public List<string> ccEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string Attachments { get; set; }

        string appservice = ConfigurationManager.AppSettings["SendEmailService"];

        public void sendEmail(SendEmail mailData)
        {

            try
            {
                
                var json = new JavaScriptSerializer().Serialize(mailData);
            JavaScriptSerializer jsserialize = new JavaScriptSerializer();
            string output = jsserialize.Serialize(mailData);
            string struri = appservice;
         
            Uri uri = new Uri(struri);
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string strout = serializer.Serialize(mailData);
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(strout);
            }
            string result = "";
            using (WebResponse response = request.GetResponse())
            {
                DataContractJsonSerializer jsonserialize = new DataContractJsonSerializer(typeof(string));
                result = (string)jsonserialize.ReadObject(response.GetResponseStream());
            }


                if (result == "Successfully send")
                {
                    foreach (string mail in mailData.toEmail)
                    {
                        WriteErrorLog(mail + " => TO---------------Mail sent successfully---✔✔✔✔✔✔✔✔✔✔");
                    }
                }
                else
                {
                    foreach (string mail in mailData.toEmail)
                    {
                        WriteErrorLog(mail + " => TO***************Mail not sent***✖✖✖✖✖✖✖✖✖✖");
                    }
                    WriteErrorLog(result);
                   
                }
               
            }

            catch (Exception ex)
            {
                //WriteErrorLog(toEmail + "***************Mail not sent?");
                WriteErrorLog(ex);
                //throw;
            }


           
        }
        // This function write Message to log file.    
        public void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
