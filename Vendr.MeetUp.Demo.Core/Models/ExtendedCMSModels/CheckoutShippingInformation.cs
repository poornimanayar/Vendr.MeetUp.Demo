using System.Collections.Generic;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
    public partial class CheckoutShippingInformation
    {
        //get the order for persisting form fields during end user next and prev button clicks
        public List<ShippingMethodReadOnly> ShippingMethods => this.GetShippingMethods();
        
        //get the order for persisting the form fields on next and previous button clicks
        public OrderReadOnly Order => this.GetCurrentOrder();
    }
}