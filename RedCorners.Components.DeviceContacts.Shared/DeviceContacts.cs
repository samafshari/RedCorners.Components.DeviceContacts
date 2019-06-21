using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace RedCorners.Components
{
    public partial class DeviceContacts
    {
        List<DeviceContact> cache = null;

        void Cache(List<DeviceContact> results)
        {
            cache = results;
        }

        public void ClearCache()
        {
            cache = null;
        }

        public async Task<List<DeviceContact>> GetAllAsync(bool preferCached)
        {
            if (preferCached && cache != null && cache.Count > 0)
                return cache.ToList();

            return await GetAllAsync();
        }
    }
}
