using Umbraco.Core.Models.PublishedContent;
using Vendr.Core.Api;
using Vendr.Core.Models;

namespace Vendr.MeetUp.Demo.Core.Extensions
{
    public static class OrderExtensions
    {
        public static PaymentMethodReadOnly GetPaymentMethod(this OrderReadOnly order)
        {
            return order?.PaymentInfo.PaymentMethodId != null
                ? VendrApi.Instance.GetPaymentMethod(order.PaymentInfo.PaymentMethodId.Value)
                : null;
        }
        
        public static ShippingMethodReadOnly GetShippingMethod(this OrderReadOnly order)
        {
            return order?.ShippingInfo.ShippingMethodId != null
                ? VendrApi.Instance.GetShippingMethod(order.ShippingInfo.ShippingMethodId.Value)
                : null;
        }
        
        public static CountryReadOnly GetPaymentCountry(this OrderReadOnly order)
        {
           return VendrApi.Instance.GetCountry(order.PaymentInfo.CountryId.Value);
        }
        
        public static CountryReadOnly GetShippingCountry(this OrderReadOnly order)
        {
            return VendrApi.Instance.GetCountry(order.ShippingInfo.CountryId.Value);
        }
    }
}