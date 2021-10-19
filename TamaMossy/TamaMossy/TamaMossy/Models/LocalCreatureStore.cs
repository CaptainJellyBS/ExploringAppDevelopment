using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TamaMossy.Models
{
    public class LocalCreatureStore : IDataStore<CreatureData>
    {
        public Task<bool> CreateItem(CreatureData item)
        {
            string creature = JsonConvert.SerializeObject(item);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteItem(CreatureData item)
        {
            throw new NotImplementedException();
        }

        public Task<CreatureData> ReadItem()
        {
            //return JsonConvert.DeserializeObject();
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItem(CreatureData item)
        {
            throw new NotImplementedException();
        }
    }
}
