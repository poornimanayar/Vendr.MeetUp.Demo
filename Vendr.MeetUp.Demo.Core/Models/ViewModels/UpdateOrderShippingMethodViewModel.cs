using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class UpdateOrderShippingMethodViewModel
    {
        public Guid ShippingMethod { get; set; }

        public int? NextStep { get; set; }
    }
}