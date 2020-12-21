using GridShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JINIApp.Shared.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using GridShared.Utility;

namespace JINIApp.Client.Services
{
    public class ItemService : ICrudDataService<Item>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public ItemService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task<Item> Get(params object[] keys)
        {
            int itemId;
            int.TryParse(keys[0].ToString(), out itemId);
            return await _httpClient.GetFromJsonAsync<Item>(_baseUri + $"api/Items/{itemId}");
        }

        public async Task Insert(Item item)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUri + $"api/Items", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-01", "Error creating the item");
            }
        }

        public async Task Update(Item item)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUri + $"api/Items/{item.ID}", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-02", "Error updating the item");
            }
        }

        public async Task Delete(params object[] keys)
        {
            int itemId;
            int.TryParse(keys[0].ToString(), out itemId);
            var response = await _httpClient.DeleteAsync(_baseUri + $"api/Items/{itemId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-03", "Error deleting the item");
            }
        }
    }
}

