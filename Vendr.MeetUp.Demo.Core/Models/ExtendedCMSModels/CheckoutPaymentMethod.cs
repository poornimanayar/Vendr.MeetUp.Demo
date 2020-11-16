using System.Collections.Generic;
using Vendr.Core.Api;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
	public partial class CheckoutPaymentMethod
	{

		public IEnumerable<PaymentMethodReadOnly> PaymentMethods => this.GetPaymentMethods();
		
		public OrderReadOnly Order => this.GetCurrentOrder();
	}
}
