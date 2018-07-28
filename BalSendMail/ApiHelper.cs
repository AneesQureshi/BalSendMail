using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;
using System.Threading.Tasks;

namespace BalSendMail
{
    public class ApiHelper
    {

             

        //Get UserList and AccountantList from Apifrom API whom user has bal less than certain limit 
        //as set in api config

        public AccountantUserList GetAccountantUserList()
        {
            AccountantUserList AU = new AccountantUserList();

            try
            {
                string appservice = ConfigurationManager.AppSettings["GetUserBalApi"];
                string appfoldername = ConfigurationManager.AppSettings["ApiFolderName"];
                

                string result = "";
               

                HttpResponseMessage response = null; // if   HttpResponseMessage dnt work then add in referece system.net,system.nethttp, system.net.formatiing  etc



                string struri2 = appfoldername+ "/" + "api" + "/" + "AlertEmail" + "/" + "GetUserBalance" + "/";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(appservice);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                response = client.GetAsync(struri2).Result;  // Blocking call!

                if (response != null || response.IsSuccessStatusCode)
                {

                    result = response.Content.ReadAsStringAsync().Result;

                }

                AU = JsonConvert.DeserializeObject<AccountantUserList>(result);

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }
            return AU;
        }
    }
}
