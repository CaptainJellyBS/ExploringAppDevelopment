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
			try
			{
				var response = await client.DeleteAsync("https://tamagotchi.hku.nl/api/Creatures" + Preferences.Get("ID", 0));
				if (response.IsSuccessStatusCode)
				{
					Preferences.Set("ID", 0);
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

		public async Task<CreatureData> ReadItem()
		{
			int creatureID = Preferences.Get("ID", 0);
			if (creatureID == 0)
			{
				return null;
			}

			try
			{
				var response = await client.GetAsync("https://tamagotchi.hku.nl/api/creatures/" + creatureID);
				if (response.IsSuccessStatusCode)
				{
					string creatureAsText = await response.Content.ReadAsStringAsync();

					CreatureData creature = JsonConvert.DeserializeObject<CreatureData>(creatureAsText);

					Preferences.Set("ID", creature.ID);

					return creature;
				}

				return null;
			}
			catch(Exception e)
            {
				return null;
            }
		}

		public async Task<bool> UpdateItem(CreatureData item)
		{
			string creatureAsText = JsonConvert.SerializeObject(item);

			try
			{
				var response = await client.PutAsync("https://tamagotchi.hku.nl/api/Creatures/" + Preferences.Get("ID",0), new StringContent(creatureAsText, Encoding.UTF8, "application/json"));
				if (response.IsSuccessStatusCode)
				{
					string postedCreatureAsText = await response.Content.ReadAsStringAsync();

					CreatureData postedCreature = JsonConvert.DeserializeObject<CreatureData>(postedCreatureAsText);

					if(Preferences.Get("ID", 0) != postedCreature.ID) { Console.WriteLine("YOU JUST FUCKED UP BIG TIME"); }
					//Preferences.Set("ID", postedCreature.ID);

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
	}
}
