using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Vendr.Core;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.CMSModels;
using Vendr.Core.Api;

namespace Vendr.MeetUp.Demo.Core.Extensions
{
    public static class PublishedContentExtensions
    {
        public static Home GetHome(this IPublishedContent content)
        {
            return (Home)content.Root();
        }

        public static StoreReadOnly GetStore(this IPublishedContent content)
        {
            return content.GetHome().Store;
        }

        //gets the Key of the Product/IPublishedContent
        public static string GetProductReference(this IPublishedContent content)
        {
            return content.Key.ToString();
        }

        //gets the content as a Vendr product using the product reference.
        //VendrApi is a singleton access to Vendr Api
        //.Instance gets the IVendrApi
        public static IProductSnapshot AsVendrProduct(this IPublishedContent content)
        {
            return VendrApi.Instance.GetProduct(content.GetProductReference());
        }

        public static Price CalculatePrice(this IPublishedContent content)
        {
            return content.AsVendrProduct()?.CalculatePrice();
        }

        //gets whether the product has any variant
        public static bool HasVariants(this IPublishedContent content)
        {
            return content.Children<ProductVariant>() !=null && content.Children<ProductVariant>().Any();
        }

        //gets the variant prices with tax (prices are set to include tax in the store)
        public static List<Price> GetVariantPrices(this Product product)
        {
            return product.Children.Select(x => x.CalculatePrice()).OrderBy(p => p.WithTax).ToList();
        }

        //check whether the prices of all variants are same
        public static bool CheckWhetherVariantPricesSame(this Product product)
        {
            var variantPrices = product.GetVariantPrices();
            return variantPrices.All(p => p.WithTax == variantPrices[0].WithTax);
        }

        //gets product variants
        public static List<ProductVariant> GetVariants(this IPublishedContent content)
        {
            return content.Children<ProductVariant>().ToList();
        }
        
        //gets current order
        public static OrderReadOnly GetCurrentOrder(this IPublishedContent content)
        {
            return VendrApi.Instance.GetCurrentOrder(content.GetStore().Id);
        }
        
        public static Checkout GetCheckoutPage(this IPublishedContent content)
        {
            return content.GetHome().FirstChild<Checkout>();
        }
        
        public static List<ICheckoutStep> GetCheckoutSteps(this Checkout content)
        {
            return content.Children<ICheckoutStep>().ToList();
        }
        
        public static List<ShippingMethodReadOnly> GetShippingMethods(this IPublishedContent content)
        {
            return VendrApi.Instance.GetShippingMethods(content.GetStore().Id).ToList();
        }
        
        public static IEnumerable<PaymentMethodReadOnly> GetPaymentMethods(this IPublishedContent content)
        {
            return VendrApi.Instance.GetPaymentMethods(content.GetStore().Id);
        }
        
        
        
        public static IEnumerable<CountryReadOnly> GetCountries(this IPublishedContent content)
        {
            return VendrApi.Instance.GetCountries(content.GetStore().Id).ToList();
        }
        
        public static OrderReadOnly GetFinalisedOrder(this IPublishedContent content)
        {
            return VendrApi.Instance.GetCurrentFinalizedOrder(content.GetStore().Id);
        }
        
        public static List<string> GetGiftCardProductReferences(this IPublishedContent content)
        {
            return content.GetHome().FirstChild<Products>().FirstChild<Product>(c => c.IsGiftCard).Children
                .Select(v => v.Key.ToString()).ToList();
        }
    }
}
