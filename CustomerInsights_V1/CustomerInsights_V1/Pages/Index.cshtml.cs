using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace CustomerInsights_V1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            ViewData["message"] = "helloooo god damn it";

            string json = "  [{        'category': 'One',        'value1': 1,        'value2': 5,        'value3': 3    }, {        'category': 'Two',        'value1': 2,        'value2': 5,        'value3': 3    }, {        'category': 'Three',        'value1': 3,        'value2': 5,        'value3': 4    }, {        'category': 'Four',        'value1': 4,        'value2': 5,        'value3': 6    }, {        'category': 'Five',        'value1': 3,        'value2': 5,        'value3': 4    }, {        'category': 'Six',        'value1': 2,        'value2': 13,        'value3': 1    }]";
            var obj = JsonConvert.DeserializeObject(json);

            ViewData["chartdatastr"] = json;
            ViewData["chartdata"] = obj;



        }
    }
}
