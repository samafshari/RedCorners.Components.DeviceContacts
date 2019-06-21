using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedCorners.Components
{
    public interface IDeviceContacts
    {
        Task<List<DeviceContact>> GetAllAsync();
    }
}
