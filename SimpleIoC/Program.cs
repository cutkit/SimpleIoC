using SimpleIoC.Implement;
using SimpleIoC.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            DIContainer.SetModule<IDatabase, Database>();
            DIContainer.SetModule<ILogger, Logger>();
            DIContainer.SetModule<IEmailSender, EmailSender>();
            DIContainer.SetModule<Cart, Cart>();


            var myCart = DIContainer.GetModule<Cart>();

            Console.ReadKey();
        }
    }
}
