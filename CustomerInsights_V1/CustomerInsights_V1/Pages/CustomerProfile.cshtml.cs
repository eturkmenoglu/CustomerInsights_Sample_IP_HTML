using CustomerInsights_Console_NewAPI_V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Dynamics.CustomerInsights.Api;
using Microsoft.Dynamics.CustomerInsights.Api.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;



namespace CustomerInsights_V1.Pages
{
    public class CustomerProfileModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public CustomerProfileModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet(string cid)
        {

            CIConnector.ConnectAsync().Wait();
            var api = CIConnector.api;
            List<InstanceInfo> allInstances = (List<InstanceInfo>)api.GetAllInstances();

            InstanceInfo ciInstance = allInstances.Find(i => i.Name == "CI Trial");

            string searchStr = cid;

           

            List<Customer> customers = new List<Customer>();

            ODataEntityPayload customerResponse = (ODataEntityPayload)api.GetEntitiesWithODataPath(ciInstance.InstanceId.ToString(), $"Customer",
                    true,
                    expand: "UnifiedActivity,Customer_Measure",
                    search: $"\"{searchStr}\"");

            foreach (var customer in customerResponse.Value)
            {
                Customer mycustomer = JsonConvert.DeserializeObject<Customer>(customer.ToString());
                customers.Add(mycustomer);
            }

            ViewData["fullname"] = customers[0].FullName;
            ViewData["headshot"] = customers[0].Headshot;
            ViewData["address"] = customers[0].City + " , "+ customers[0].Country;
            ViewData["churnrisk"] = (customers[0].Customer_Measure.ChurnScore*100);
            ViewData["cltv"] = customers[0].Customer_Measure.TotalSpent;
        }
    }
}
