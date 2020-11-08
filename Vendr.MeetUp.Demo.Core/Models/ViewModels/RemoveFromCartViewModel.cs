using System;

namespace Vendr.MeetUp.Demo.Core.Models.ViewModels
{
    public class RemoveFromCartViewModel
    {
        //orderline id
        public Guid OrderLineId { get; set; }

        public string ProductName { get; set; }
    }
}