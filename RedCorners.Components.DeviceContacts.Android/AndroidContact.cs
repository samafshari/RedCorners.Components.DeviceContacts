using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RedCorners.Components
{
    public class AndroidContact
    {
        public string DisplayName { get; set; }
        public string PhotoUri { get; set; }
        public string PhotoUriThumbnail { get; set; }
        public string[] Emails { get; set; }
        public string[] Numbers { get; set; }
        public AndroidPostalAddress[] Addresses { get; set; }
    }

    public class AndroidPostalAddress
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string Pobox { get; set; }
        public string Neighborhood { get; set; }
        public string FormattedAddress { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}