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
    }
}