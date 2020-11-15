using System.Collections.Generic;
using System.Linq;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
    public partial class CheckoutPersonalInformation
    {
        //get the order for persisting form fields during end user next and prev button clicks
        public OrderReadOnly Order => this.GetCurrentOrder();
        
        //get list of countries
        public IEnumerable<CountryReadOnly> Countries => this.GetCountries();
    }
}