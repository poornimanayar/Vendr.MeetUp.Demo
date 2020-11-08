using System.Collections.Generic;
using Vendr.Core.Api;
using Vendr.Core.Models;
using Vendr.MeetUp.Demo.Core.Extensions;

namespace Vendr.MeetUp.Demo.CMSModels
{
    public partial class Register
    {
        public IEnumerable<CountryReadOnly> Countries => VendrApi.Instance.GetCountries(this.GetStore().Id);
    }
}