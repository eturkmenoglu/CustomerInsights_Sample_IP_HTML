using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInsights_Console_NewAPI_V1
{
    public class AppRegistrationConfig
    {
        public string Instance { get; set; }

        public string Tenant { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Authority { get; set; }
    }
}
