using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
    public partial class Cart
    {
        public OrderReadOnly Order => this.GetCurrentOrder();
    }
}