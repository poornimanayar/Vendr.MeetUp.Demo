using Vendr.Core.Api;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
    public partial class CheckoutReview
    {
        public virtual OrderReadOnly Order => this.GetCurrentOrder();
        
        public CountryReadOnly PaymentCountry => this.Order.GetPaymentCountry();

        public CountryReadOnly ShippingCountry => this.Order.GetShippingCountry();
    }
}