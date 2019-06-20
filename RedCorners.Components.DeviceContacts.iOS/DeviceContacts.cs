using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts;
using Foundation;
using UIKit;
using System.Reflection;

namespace RedCorners.Components
{
    public class DeviceContacts
    {
        public IEnumerable<CNContact> GetAllRaw()
        {
            var values = new[]
            {
                CNContactKey.Birthday,
                CNContactKey.Dates,
                CNContactKey.DepartmentName,
                CNContactKey.EmailAddresses,
                CNContactKey.FamilyName,
                CNContactKey.GivenName,
                CNContactKey.Identifier,
                CNContactKey.ImageData,
                CNContactKey.ImageDataAvailable,
                CNContactKey.InstantMessageAddresses,
                CNContactKey.JobTitle,
                CNContactKey.MiddleName,
                CNContactKey.NamePrefix,
                CNContactKey.NameSuffix,
                CNContactKey.Nickname,
                CNContactKey.NonGregorianBirthday,
                CNContactKey.Note,
                CNContactKey.OrganizationName,
                CNContactKey.PhoneNumbers,
                CNContactKey.PhoneticFamilyName,
                CNContactKey.PhoneticGivenName,
                CNContactKey.PhoneticMiddleName,
                CNContactKey.PhoneticOrganizationName,
                CNContactKey.PostalAddresses,
                CNContactKey.PreviousFamilyName,
                CNContactKey.Relations,
                CNContactKey.SocialProfiles,
                CNContactKey.ThumbnailImageData,
                CNContactKey.Type,
                CNContactKey.UrlAddresses
            };

            return GetAllRaw(values);
        }

        public IEnumerable<CNContact> GetAllRaw(NSString[] keys)
        {
            List<CNContact> results = new List<CNContact>();
            using (var store = new CNContactStore())
            {
                var allContainers = store.GetContainers(null, out var error);

                if (error != null)
                    throw new Exception($"iOS Exception: {error}");

                foreach (var container in allContainers)
                {
                    try
                    {
                        using (var predicate = CNContact.GetPredicateForContactsInContainer(container.Identifier))
                        {
                            var containerResults = store.GetUnifiedContacts(predicate, keys, out error);
                            results.AddRange(containerResults);
                        }
                    }
                    catch
                    {
                        // ignore missed contacts from errors
                    }
                }
            }

            return results;
        }

        public List<DeviceContact> GetAll()
        {
            return GetAllRaw().Select(x => CNContactToDeviceContact(x)).ToList();
        }

        static readonly CNPostalAddressFormatter formatter = new CNPostalAddressFormatter();

        public static DeviceContact CNContactToDeviceContact(CNContact item)
        {
            return new DeviceContact
            {
                Birthday = item.Birthday?.ToString(),
                DepartmentName = item.DepartmentName,
                EmailAddresses = item.EmailAddresses?.Select(x => x.Value?.ToString()).ToArray(),
                FamilyName = item.FamilyName,
                GivenName = item.GivenName,
                Identifier = item.Identifier,
                JobTitle = item.JobTitle,
                MiddleName = item.MiddleName,
                NamePrefix = item.NamePrefix,
                NameSuffix = item.NameSuffix,
                Nickname = item.Nickname,
                Note = item.Note,
                OrganizationName = item.OrganizationName,
                PhoneNumbers = item.PhoneNumbers?.Select(x => x.Value?.ToString()).ToArray(),
                PostalAddresses = item.PostalAddresses?.Select(x => formatter.GetStringFromPostalAddress(x.Value)).ToArray(),
                Tag = item
            };
        }
    }
}