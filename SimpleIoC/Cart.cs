using SimpleIoC.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoC
{
    class Cart
    {
        private readonly IDatabase _db;
        private readonly ILogger _lg;
        private readonly IEmailSender _es;

        public Cart(IDatabase database, ILogger logger, IEmailSender emailSender)
        {
            _db = database;
            _lg = logger;
            _es = emailSender;
        }
        
    }
}
