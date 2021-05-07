using System;
using Umbraco.Core;
using Vendr.Core.Discounts.Rewards;
using Vendr.Core.Models;

namespace Vendr.MeetUp.Demo.Core.Discounts
{
    [DiscountRewardProvider("automaticAddProductReward", "Automatic Add Product Reward")]
    public class AutomaticAddProductReward : DiscountRewardProviderBase<AutomaticAddProductRewardSettings>
    {
        public override DiscountRewardCalculation CalculateReward(DiscountRewardContext context, AutomaticAddProductRewardSettings settings)
        {
            var result = new DiscountRewardCalculation();

            var price = new Price(0M, 0M, Guid.Parse("2d95ff9a-e322-4914-8ec5-c2748af420e1"));//currency id

            result.TotalPriceAdjustments.Add(new DiscountAdjustment(context.Discount, price));

            return result;
        }
    }

    public class AutomaticAddProductRewardSettings
    {
        [DiscountRewardProviderSetting(Key = "priceType",
        Name = "Price Type",
        Description = "The price that will be affected by this reward")]
        public OrderPriceType PriceType { get; set; }

        [DiscountRewardProviderSetting(Key = "nodeId",
       Name = "Product Node",
       Description = "The product to discount the price of",
       View = "contentpicker",
       Config = "{ startNodeId: -1, multiPicker: false, idType: 'udi' }")]
        public Udi NodeId { get; set; }
    }
}
