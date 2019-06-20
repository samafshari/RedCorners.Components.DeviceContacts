using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

// Partially based on: https://github.com/enisn/Xamarin.Forms.Contacts/blob/master/Plugin/ContactService/Platforms/Android/ContactServiceImplementation.cs

namespace RedCorners.Components
{
    public class DeviceContacts : IDeviceContacts
    {
        public List<DeviceContact> GetAll()
        {
            return GetAllRaw().Select(x => new DeviceContact
            {
                Name = x.DisplayName,
                PhoneNumbers = x.Numbers,
                EmailAddresses = x.Emails
            }).ToList();
        }

        public IEnumerable<AndroidContact> GetAllRaw()
        {
            var context = Application.Context;
            var cursor = context.ApplicationContext.ContentResolver
                .Query(ContactsContract.Contacts.ContentUri, null, null, null, null);
            if (cursor.Count == 0) yield break;

            while (cursor.MoveToNext())
            {
                var contact = GetContact(cursor, context);

                if (contact != null)
                    yield return contact;
            }
        }

        AndroidContact GetContact(ICursor cursor, Context context)
        {
            var contactId = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.Id);
            //            var hasNumbers = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber) == "1";

            var numbers = GetNumbers(context, contactId);
            //.ToList();
            var emails = GetEmails(context, contactId);
            //.ToList();

            var contact = new AndroidContact
            {
                DisplayName = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.DisplayName),
                PhotoUri = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.PhotoUri),
                PhotoUriThumbnail = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri),
                Emails = emails.ToArray(),
                Numbers = numbers.ToArray(),
            };

            return contact;
        }

        private static IEnumerable<string> GetNumbers(Context ctx, string contactId)
        {
            var key = ContactsContract.CommonDataKinds.Phone.Number;

            var cursor = ctx.ApplicationContext.ContentResolver.Query(
                ContactsContract.CommonDataKinds.Phone.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + " = ?",
                new[] { contactId },
                null
            );

            return ReadCursorItems(cursor, key);
        }

        private static IEnumerable<string> GetEmails(Context ctx, string contactId)
        {
            var key = ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data;

            var cursor = ctx.ApplicationContext.ContentResolver.Query(
                ContactsContract.CommonDataKinds.Email.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Email.InterfaceConsts.ContactId + " = ?",
                new[] { contactId },
                null);

            return ReadCursorItems(cursor, key);
        }

        private static IEnumerable<string> ReadCursorItems(ICursor cursor, string key)
        {
            while (cursor.MoveToNext())
            {
                var value = GetString(cursor, key);
                yield return value;
            }

            cursor.Close();
        }

        private static string GetString(ICursor cursor, string key)
        {
            return cursor.GetString(cursor.GetColumnIndex(key));
        }
    }
}