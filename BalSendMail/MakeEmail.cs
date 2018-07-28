using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalSendMail
{
    public class MakeEmail
    {
        public void makeEmail(AccountantUserList AU)
        {
            List<User> userList = new List<User>();
            List<Accountant>  accountantList = new List<Accountant> ();

            userList = AU.userList;
            accountantList = AU.accountantList;

            foreach(var user in userList)
            {
                SendEmail send = new SendEmail();

                string username = user.firstName;
                string useremail = user.email;
                string userbal = user.balLeft;


                foreach (var accountant in accountantList)
                {
                    string accountantname = accountant.userInterface;
                   


                    string ApplicationName = ConfigurationManager.AppSettings["SearchExpenseUrl"];
                    string ApplicationUrl = ConfigurationManager.AppSettings["SearchExpenseUrl"];
                    string LowBalUrl = ConfigurationManager.AppSettings["SearchExpenseUrl"];

                    string body = "Dear " + accountantname + "," + "<br/><br/>" +
                                    "The advance amount for a user has dropped below minimum threshold level. Please find details below:" + "<br/>" + "<br/>" +
                                    "User: " + username+ "<br/>" +
                                    "Email: " + useremail + "<br/>" +
                                    "Advance Amount: " + userbal + "<br/>" +
                                    "Please take necessary action. You can login to the system here " + ApplicationUrl + "/" + LowBalUrl + "<br/>" + "<br/>" +
                                    "Best," + "<br/>" + "<br/>" +
                                    "Team Prabhat" + "<br/>" +
                                    "Expense Management";

                    string subject = username+"'s" + " advance is too low" + ApplicationName;
                    List<string> toEmail = new List<string>();
                    
                    toEmail.Add(accountant.loginId);
                    send.subject = subject;
                    send.body = body;
                    send.toEmail = toEmail;

                    send.sendEmail(send);

                }



            }


        }

    }
}
