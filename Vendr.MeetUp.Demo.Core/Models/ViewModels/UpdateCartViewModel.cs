using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class UpdateCartViewModel
    {
        //order lines in the order to be updated
        public UpdateCartLineItemViewModel[] OrderLines { get; set; }
    }
    
    public class UpdateCartLineItemViewModel
    {
        //orderline id 
        public Guid LineItemId { get; set; }

        //quantity of the item
        public decimal Quantity { get; set; }
    }
}