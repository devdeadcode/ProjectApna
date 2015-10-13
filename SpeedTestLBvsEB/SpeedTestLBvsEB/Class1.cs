using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics;
using System.IO;


namespace SpeedTestLBvsEB
{
    public class Class1
    {
        private const string CrmConnectionUrl = @"Url=https://test.eonesolutions.com:444; Username=eone\bob.christianson; Password=Chris10son;";

        static void Main(string[] args)
        {
            var connection = CrmConnection.Parse(CrmConnectionUrl);
            var service = new OrganizationService(connection);

            //Console.WriteLine("Early Bound - Short");
            //CreateAccounts("Early Bound - Short", 200, 10, () =>
            //{
                //Account a = new Account(); //Generated via SvcUtil
                //a.Name = "Via Context";

                //service.Create(a);
            //});

            Console.WriteLine("Late Bound - Short");
            CreateAccounts("Late Bound - Short", 200, 10, () =>
            {
                Entity e = new Entity("Account");
                e["name"] = "Via Service";

                service.Create(e);
            });

            //Console.WriteLine("Early Bound - Long");
            //CreateAccounts("Early Bound - Long", 5000, 250, () =>
            //{
            //    Account a = new Account();
            //    a.Name = "Via Context";

            //    service.Create(a);
            //});

            Console.WriteLine("Late Bound - Long");
            CreateAccounts("Late Bound - Long", 5000, 250, () =>
            {
                Entity e = new Entity("account");
                e["name"] = "Via Service";

                service.Create(e);
            });

            Console.WriteLine("Finished");

            Console.ReadKey();
        }

        static void CreateAccounts(String name, int runs, int sample, Action action)
        {
            StreamWriter file = new StreamWriter("c:\\temp\\evl3.txt", true);
            file.WriteLine(name);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 1; i <= runs; i++)
            {
                if (i == runs || i % sample == 0)
                {
                    Console.WriteLine(i + "," + stopwatch.ElapsedMilliseconds);
                    file.WriteLine(i + "," + stopwatch.ElapsedMilliseconds);
                }

                action();
            }

            stopwatch.Stop();

            file.Close();
        }
    }
}
