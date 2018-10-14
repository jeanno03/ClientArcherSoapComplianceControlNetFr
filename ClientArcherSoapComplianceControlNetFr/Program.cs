using ClientArcherSoapComplianceControlNetFr.Tests;
using ClientArcherSoapComplianceControlNetFr.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientArcherSoapComplianceControlNetFr
{
    class Program
    {
        static void Main(string[] args)
        {

            string login = ConfigurationManager.AppSettings.GetAppConfigValues("login");
            string mdp = ConfigurationManager.AppSettings.GetAppConfigValues("mdp");
            string endPoint = ConfigurationManager.AppSettings.GetAppConfigValues("endPoint");
            string spaceName = ConfigurationManager.AppSettings.GetAppConfigValues("space-name");
            Console.WriteLine("Values of App.config : ");
            Console.WriteLine("login : " + login);
            Console.WriteLine("mdp : " + mdp);
            Console.WriteLine("endPoint : " + endPoint);
            Console.WriteLine("space-name : " + spaceName);
            Console.WriteLine("---------------------------------------");
            TestsClass test01 = new TestsClass();

            //test01.testUser();
            test01.TestPercentageCompliance();
        }
    }
}
