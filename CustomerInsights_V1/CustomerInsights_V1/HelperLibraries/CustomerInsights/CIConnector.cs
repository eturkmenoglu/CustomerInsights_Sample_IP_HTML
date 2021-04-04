using CustomerInsights_V1.Pages;
using Microsoft.Dynamics.CustomerInsights.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInsights_Console_NewAPI_V1
{
    public class CIConnector
    {
        public static CustomerInsights api;
        /// <summary>
        /// Loads the <see cref="AppRegistrationConfig"/> and <see cref="ApiRegistrationConfig"/> configs.
        /// </summary>
        /// <param name="appRegistrationConfig">Config for Azure App Registraiton</param>
        /// <param name="apiRegistrationConfig">Config for the API Subscription Key from Customer Insights.</param>
        private static void LoadConfigs(AppRegistrationConfig appRegistrationConfig, ApiRegistrationConfig apiRegistrationConfig)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            configuration.GetSection("AppRegistration").Bind(appRegistrationConfig);
            configuration.GetSection("ApiRegistration").Bind(apiRegistrationConfig);
            appRegistrationConfig.Authority = "https://login.microsoftonline.com/" + appRegistrationConfig.Tenant;
        }

        /// <summary>
        /// Builds a Confidential Client Application for authorizing an App Registration with API.
        /// </summary>
        /// <param name="appRegistrationConfig"></param>
        /// <returns></returns>
        private static IConfidentialClientApplication BuildClientApplication(AppRegistrationConfig appRegistrationConfig)
        {
            return ConfidentialClientApplicationBuilder.Create(appRegistrationConfig.ClientId)
               .WithClientSecret(appRegistrationConfig.ClientSecret)
               .WithAuthority(new Uri(appRegistrationConfig.Authority))
               .Build();
        }

        /// <summary>
        /// Creates the Customer Insights API by acquiring an access token from the app and constructing the client with proper HTTP headers.
        /// </summary>
        /// <param name="apiRegistrationConfig">The app registration config.</param>
        /// <param name="app">The app configured with client secrets and authority.</param>
        /// <returns>An instantiated and authorized Customer Insights API</returns>
        private static async Task<CustomerInsights> CreateCustomerInsightsApi(ApiRegistrationConfig apiRegistrationConfig, IConfidentialClientApplication app)
        {
            var accessToken = await AcquireAccessTokenAsync(app);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {accessToken}");
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiRegistrationConfig.ApiSubscriptionKey);

            var customerInsightsApi = new CustomerInsights(httpClient, false);
            return customerInsightsApi;
        }

        /// <summary>
        /// Acquires an access token from the app registration.
        /// </summary>
        /// <param name="app">The app configured with client secrets and authority.</param>
        /// <returns>The access token for authorization.</returns>
        private static async Task<string> AcquireAccessTokenAsync(IConfidentialClientApplication app)
        {
            // With client credentials flows, the scope is always of the shape "resource/.default" because the
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator.
            var scopes = new string[] { "38c77d00-5fcb-4cce-9d93-af4738258e3c/.default" };

            AuthenticationResult result;
            try
            {
                result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                return result.AccessToken;
            }
            catch (MsalUiRequiredException)
            {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application?
                throw;
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
                throw;
            }
        }

        internal static async Task<CustomerInsights> ConnectAsync()
        {
            AppRegistrationConfig appRegistrationConfig = new AppRegistrationConfig();
            ApiRegistrationConfig apiRegistrationConfig = new ApiRegistrationConfig();

            LoadConfigs(appRegistrationConfig, apiRegistrationConfig);

            IConfidentialClientApplication app = BuildClientApplication(appRegistrationConfig);

            CustomerInsights customerInsightsApi = await CreateCustomerInsightsApi(apiRegistrationConfig, app);

            api = customerInsightsApi;


            return customerInsightsApi;

            //var sampleInteractions = new SampleApiInteractions(customerInsightsApi);
            //await sampleInteractions.StartAsync();
        }
    }
}
