using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class UpdateCartLineItemViewModel
    {
        //orderline id 
        public Guid LineItemId { get; set; }

        //quantity of the item
        public decimal Quantity { get; set; }
    }
}