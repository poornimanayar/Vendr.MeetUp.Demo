using System.Collections.Generic;
using Vendr.Core.Models;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName{get;set;}

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Telephone { get; set; }
        
        public bool MarketingOptIn { get; set; }

        public IEnumerable<CountryReadOnly> Countries { get; set; }
    }
}