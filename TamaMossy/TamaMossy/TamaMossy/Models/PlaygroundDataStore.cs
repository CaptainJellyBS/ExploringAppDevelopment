using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Essentials;
using System.Net.Http;

namespace TamaMossy.Models
{
    class PlaygroundDataStore : IDataStore<PlaygroundEntry>
    {
        private HttpClient client = new HttpClient();

        public async Task<bool> CreateItem(PlaygroundEntry item)
        {
            string playgroundEntryAsText = JsonConvert.SerializeObject(item);

            try
            {
                var response = await client.PostAsync("https://tamagotchi.hku.nl/api/playground/" + Preferences.Get("ID", 0), new StringContent(playgroundEntryAsText, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string postedEntryAsText = await response.Content.ReadAsStringAsync();

                    PlaygroundEntry postedEntry = JsonConvert.DeserializeObject<PlaygroundEntry>(postedEntryAsText);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItem(PlaygroundEntry item)
        {
            try
            {
                var response = await client.DeleteAsync("https://tamagotchi.hku.nl/api/playground/" + Preferences.Get("ID", 0));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }

        public async Task<PlaygroundEntry> ReadItem()
        {
            int creatureID = Preferences.Get("ID", 0);
            if (creatureID == 0)
            {
                return null;
            }

            try
            {
                var response = await client.GetAsync("https://tamagotchi.hku.nl/api/playground/" + creatureID);
                if (response.IsSuccessStatusCode)
                {
                    string entryAsText = await response.Content.ReadAsStringAsync();

                    PlaygroundEntry entry = JsonConvert.DeserializeObject<PlaygroundEntry>(entryAsText);          

                    return entry;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<PlaygroundEntry>> ReadAllItems()
        {
            try
            {
                var response = await client.GetAsync("https://tamagotchi.hku.nl/api/Playground/");
                if (response.IsSuccessStatusCode)
                {
                    string entryAsText = await response.Content.ReadAsStringAsync();

                    List<PlaygroundEntry> entry = JsonConvert.DeserializeObject<List<PlaygroundEntry>>(entryAsText);

                    return entry;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<bool> UpdateItem(PlaygroundEntry item)
        {
            throw new NotImplementedException("Items in the Playground should never be updated");
        }
    }
}
