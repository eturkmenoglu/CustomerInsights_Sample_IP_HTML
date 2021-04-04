[CustomerInsights_HTML_Screenshot](https://github.com/eturkmenoglu/CustomerInsights_Sample_IP_HTML/blob/master/screenshot.png?raw=true)
This is a Sample HTML/.NET based First Customer Insights Web Project. In other-words IP on top of CI. 

Where, It is connected to Customer Insights API, and consumes customer details and shows them in a custom UI as one wishes.

You may reach a deployed version on cloud from here: https://customerinsightsretaildesignv1.azurewebsites.net

Feel free to feedback directly to erturkm@microsoft.com 

I have created this for 3 reasons, 

1. To Enable PreSales Teams to be able to show alternative CI Screens to the customers, explaining the extensibility of CI. 
2. To Enable regions where we need to deploy CI in hybrid mode, half onprem and half cloud.
3. To Enable partners and give them a starting point on tru CI IPs.
	
  
Comments feedbacks are welcome ! 
This is a free community project, feel free to download and make your own version of it. 

For Configuring your Customer Insights environment to be able to consume API, please follow below steps. 

Here are some useful links for you to have a look at, in order
 	
  1. Getting Started: https://developer.ci.ai.dynamics.com/
	2. AppRegistration: https://developer.ci.ai.dynamics.com/appregistration
	3. ServertoServerConfig: https://developer.ci.ai.dynamics.com/servertoserver
	4. C# .NodeJS ,Phyton Library: https://developer.ci.ai.dynamics.com/clientlibraries
	5. Sample Apps C#,NodeJS: https://github.com/microsoft/Dynamics365-CustomerInsights-Client-Libraries


 

Sample appsettings.json is supposed to be like, 

 

{

       "AppRegistration": {

              "Instance": "",//CIInstanceID

              "Tenant": "", //TenantID

              "ClientId": "", //AppID

              "ClientSecret": "",//AppRegistrationClientSecret

              "Authority": "https://login.microsoftonline.com/{TenantID}"

 

 

       },

       "ApiRegistration": {

              "ApiSubscriptionKey": "" //ApiSubscriptionKeyCI

 

       }

}
