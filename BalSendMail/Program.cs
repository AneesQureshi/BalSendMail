using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalSendMail
{
    class Program
    {
        static void Main(string[] args)
        {
           

            //comment when we deploy on master branch becouse we pass parameters in taskScheduler
            //Start Comment

            Console.WriteLine("Enter your argument=");
            string[] tempArr = new string[1];
            string commandlinearg = Console.ReadLine();
            if (commandlinearg != "")
            {
                tempArr[0] = commandlinearg;
            }

            //End Comment

            //Replace tempArr with args for master
            foreach (string arg in tempArr)

                

            {
                if (arg == "SendBalEmail")
                {

                    //get userList whose bal is less than limit and AccountantList
                    AccountantUserList AU = new AccountantUserList();
                    ApiHelper ap = new ApiHelper();
                    AU= ap.GetAccountantUserList();

                    //calling making email function which in turn calls send email function 
                    MakeEmail make = new MakeEmail();
                    make.makeEmail(AU);



                   
                }

            }


        }
    }
}
