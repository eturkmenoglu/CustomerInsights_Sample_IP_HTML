using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInsights_Console_NewAPI_V1
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class UnifiedActivity
    {
        public string CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public object EndTime { get; set; }
        public object Duration { get; set; }
        public string DurationUnit { get; set; }
        public string IconCode { get; set; }
        public object ImageURL { get; set; }
        public string ActivityType { get; set; }
        public object ExternalUrl { get; set; }
        public DateTime ActivityTime { get; set; }
        public string ActivityId { get; set; }
        public string ActualActivityId { get; set; }
        public string Title { get; set; }
        public string EntityName { get; set; }
        public string ActivityTypeDisplay { get; set; }
        public string ActivityName { get; set; }
        public object EventDate { get; set; }
    }

    public class CustomerMeasure
    {
        public string CustomerId { get; set; }
        public double? AverageOnlineSpent { get; set; }
        public double? AverageStoreSpent { get; set; }
        public double? ChurnScore { get; set; }
        public double? CLTVScore { get; set; }
        public double? LoyaltyPoints { get; set; }
        public double? TotalSpent { get; set; }
    }

    public class Customer
    {
        [JsonProperty("@microsoft.customer360.search.Score")]
        public string MicrosoftCustomer360SearchScore { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PostCode { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Headshot { get; set; }
        public string ContactId { get; set; }
        public int? RewardPoints { get; set; }
        public string CreditCard { get; set; }
        public string ECommerce_eCommerceContacts_ContactId { get; set; }
        public string ECommerce_eCommerceContacts_ContactId_Alternate { get; set; }
        public string Loyalty_loyCustomers_LoyaltyId { get; set; }
        public string Loyalty_loyCustomers_LoyaltyId_Alternate { get; set; }
        public object Dynamics365_Contact_contactid { get; set; }
        public object Dynamics365_Contact_contactid_Alternate { get; set; }
        public List<UnifiedActivity> UnifiedActivity { get; set; }
        public CustomerMeasure Customer_Measure { get; set; }
    }


}
