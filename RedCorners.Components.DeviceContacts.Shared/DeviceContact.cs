using System;
using System.Collections.Generic;
using System.Text;

namespace RedCorners.Components
{
    public class DeviceContact
    {
        public string Name { get; set; }
        public string[] EmailAddresses { get; set; }
        public string[] PhoneNumbers { get; set; }
        public string[] PostalAddresses { get; set; }
        //public string Birthday { get; set; }
        //public string DepartmentName { get; set; }
        //public string FamilyName { get; set; }
        //public string GivenName { get; set; }
        //public string Identifier { get; set; }
        //public string JobTitle { get; set; }
        //public string MiddleName { get; set; }
        //public string NamePrefix { get; set; }
        //public string NameSuffix { get; set; }
        //public string Nickname { get; set; }
        //public string Note { get; set; }
        //public string OrganizationName { get; set; }
        public object Tag { get; set; }
    }
}
