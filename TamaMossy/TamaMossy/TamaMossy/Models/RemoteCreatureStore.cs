using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TamaMossy.Models
{
    public class RemoteCreatureStore : IDataStore<CreatureData>
    {
		private HttpClient client = new HttpClient();

		public async Task<bool> CreateItem(CreatureData item)
		{
			string creatureAsText = JsonConvert.SerializeObject(item);

			try
			{
				var response = await client.PostAsync("https://tamagotchi.hku.nl/api/Creatures", new StringContent(creatureAsText, Encoding.UTF8, "application/json"));
				if (response.IsSuccessStatusCode)
				{
					string postedCreatureAsText = await response.Content.ReadAsStringAsync();

					CreatureData postedCreature = JsonConvert.DeserializeObject<CreatureData>(postedCreatureAsText);

					Preferences.Set("ID", postedCreature.ID);

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

		public async Task<bool> DeleteItem(CreatureData item)
		{
			throw new NotImplementedException();
		}

		public async Task<CreatureData> ReadItem()
		{
			int creatureID = Preferences.Get("ID", 0);
			if (creatureID == 0)
			{
				return null;
			}

			var response = await client.GetAsync("https://tamagotchi.hku.nl/api/Creatures/2");
			if (response.IsSuccessStatusCode)
			{
				string creatureAsText = await response.Content.ReadAsStringAsync();

				CreatureData creature = JsonConvert.DeserializeObject<CreatureData>(creatureAsText);

				Preferences.Set("ID", creature.ID);

				return creature;
			}

			return null;
		}

		public async Task<bool> UpdateItem(CreatureData item)
		{
			throw new NotImplementedException();
		}
	}
}
