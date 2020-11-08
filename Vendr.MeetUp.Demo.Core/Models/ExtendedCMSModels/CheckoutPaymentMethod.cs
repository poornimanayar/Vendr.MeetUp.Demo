using System.Collections.Generic;
using Vendr.Core.Api;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
	public partial class CheckoutPaymentMethod {
		
		public IEnumerable<PaymentMethodReadOnly> PaymentMethods => VendrApi.Instance.GetPaymentMethods(this.GetStore().Id);
		
		public virtual OrderReadOnly Order => this.GetCurrentOrder();
	}
}
