using System.Collections.Generic;
using Vendr.Core.Api;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
	public partial class CheckoutPayment {
		
		public IEnumerable<PaymentMethodReadOnly> PaymentMethods => VendrApi.Instance.GetPaymentMethods(this.GetStore().Id);
		
		public OrderReadOnly Order => this.GetCurrentOrder();
	}
}
