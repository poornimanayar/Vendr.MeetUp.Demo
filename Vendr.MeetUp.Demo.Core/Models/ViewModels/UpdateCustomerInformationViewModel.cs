using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class UpdateCustomerInformationViewModel
    {
        public string Email { get; set; }

        public bool MarketingOptIn { get; set; }

        public OrderAddressViewModel BillingAddress { get; set; }

        public OrderAddressViewModel ShippingAddress { get; set; }

        public bool ShippingSameAsBilling { get; set; }

        public string Comments { get; set; }

        public int? NextStep { get; set; }
        
        public bool GiftWrap { get; set; }
        
        public string GiftMessage { get; set; }
    }

    public class OrderAddressViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public Guid Country { get; set; }

        public string Telephone { get; set; }
    }
}
