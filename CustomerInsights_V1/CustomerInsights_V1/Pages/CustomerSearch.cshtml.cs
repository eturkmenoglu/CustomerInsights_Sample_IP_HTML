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
    public class CustomerSearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        
      
        private readonly ILogger<CustomerSearchModel> _logger;

        public CustomerSearchModel(ILogger<CustomerSearchModel> logger)
        {
            _logger = logger;
        }


        public void Search()
        {

        }



        public void OnPostCustomerDetail()
        {
            
        }

        public void OnGet(string searchterm)
        {
            
            
            //ViewData["message"] = "helloooo god damn it";

            //string json = "  [{        'category': 'One',        'value1': 1,        'value2': 5,        'value3': 3    }, {        'category': 'Two',        'value1': 2,        'value2': 5,        'value3': 3    }, {        'category': 'Three',        'value1': 3,        'value2': 5,        'value3': 4    }, {        'category': 'Four',        'value1': 4,        'value2': 5,        'value3': 6    }, {        'category': 'Five',        'value1': 3,        'value2': 5,        'value3': 4    }, {        'category': 'Six',        'value1': 2,        'value2': 13,        'value3': 1    }]";
            //var obj = JsonConvert.DeserializeObject(json);

            //ViewData["chartdatastr"] = json;
            //ViewData["chartdata"] = obj;

            //List<CustomerCard> customers = new List<CustomerCard>();

            //customers.Add(new CustomerCard("archie"));
            //customers.Add(new CustomerCard("ercument"));
            //customers.Add(new CustomerCard("ikram"));
            //customers.Add(new CustomerCard("Petra"));

            //ViewData["customers"] = getCustomerListHTML(customers);


            CIConnector.ConnectAsync().Wait();
            
            var api = CIConnector.api;

            List<InstanceInfo> allInstances = (List<InstanceInfo>)api.GetAllInstances();

            InstanceInfo ciInstance = allInstances.Find(i => i.Name == "CI Trial");


            string searchStr = "a";

            if (searchterm != null)
            {
                searchStr = searchterm;
            }

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

            ViewData["customers"] = getCustomerListHTML(customers);

            api.Dispose();

        }
        
        public string getCustomerListHTML(List<Customer> customers) {

            string customerCardTemplate = " <th style=\"width: 10%; padding-right: 4px; padding-left: 4px;\" >    <button id=\"customerid\" onclick=\"reply_click(this.id);\" style=\"border: transparent;\">       <div class=\"account-info\" style=\"background:white;height: 100%;\">                                <div class=\"account-background\"></div>                                <div class=\"account-photo\">                                    <img style=\"width:50%; height: 50%; border-radius: 50px;  \" src=\"customerheadshot\">                                </div>                                <div class=\"account-name\" style=\"font-family: Segoe UI Semibold;font-size: 18px;\">customername</div>                                <div class=\"account-location\" style=\"font-family: Segoe UI ;font-size: 9;\">                                    <h6>1108 23 Ave S</h6>                                    <h6>customercountry</h6>                                </div>                            </div>               </button>   </th>";

            if (customers.Count < 10) {

                customerCardTemplate = customerCardTemplate.Replace("width: 10%;", "width: 190%;");
            }
            
            
            string html = "<tr>";
            int i = 0;
            foreach (var customer in customers)
            {
                html += customerCardTemplate.Replace("customername", customer.FullName).Replace("customerheadshot",customer.Headshot).Replace("customercountry", customer.City+","+customer.Country).Replace("customerid",customer.CustomerId);
                i++;

                if (i == 10) {

                    html += html + "</tr><tr>";

                    i = 0;
                }

            }


            html += html + "</tr>";
            return html;
        }
    }
}
