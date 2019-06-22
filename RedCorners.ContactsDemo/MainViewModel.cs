using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using RedCorners.Components;

namespace RedCorners.ContactsDemo
{
    public class MainViewModel : BindableModel
    {
        public List<DeviceContact> Items { get; set; } = new List<DeviceContact>();

        public MainViewModel()
        {
            Status = Models.TaskStatuses.Success;
        }

        public Command FetchCommand => new Command(async () =>
        {
            var contacts = new DeviceContacts();
#if __ANDROID__
            contacts.QueryEmails = false;
            contacts.QueryPhoneNumbers = false;
#endif
            Status = Models.TaskStatuses.Busy;
            UpdateProperties();
            List<DeviceContact> raw = null;
            try
            {
                raw = await contacts.GetAllAsync();
                var items = raw.Where(x => x.PostalAddresses != null && x.PostalAddresses.Length > 0).ToList();
                Items = items;
            }
            catch (Exception ex)
            {
                App.Instance.DisplayAlert("Error", ex.ToString(), "OK");
            }
            Status = Models.TaskStatuses.Success;
            UpdateProperties();

            if (raw == null)
                App.Instance.DisplayAlert("Done", $"Operation Completed, raw=null", "OK");
            else
                App.Instance.DisplayAlert("Done", $"Operation Completed, Count={raw.Count}", "OK");
        });
    }
}
