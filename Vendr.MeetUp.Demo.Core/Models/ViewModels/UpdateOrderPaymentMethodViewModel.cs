using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class UpdateOrderPaymentMethodViewModel
    {
        public Guid PaymentMethod { get; set; }

        public int? NextStep { get; set; }
    }
}