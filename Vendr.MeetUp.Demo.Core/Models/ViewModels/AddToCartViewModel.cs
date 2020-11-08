using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class AddToCartViewModel
    {
        public Guid ProductReference { get; set; }

        public int Quantity { get; set; }
    }
}