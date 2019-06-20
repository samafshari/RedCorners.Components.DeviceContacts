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

        public Command FetchCommand => new Command(() =>
        {
            var contacts = new DeviceContacts();
            var raw = contacts.GetAll().ToList();
            Items = raw;
            UpdateProperties();
        });
    }
}
