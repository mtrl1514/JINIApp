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
    public class RevenueService : ICrudDataService<Revenue>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public RevenueService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task<Revenue> Get(params object[] keys)
        {
            int revenueId;
            int.TryParse(keys[0].ToString(), out revenueId);
            return await _httpClient.GetFromJsonAsync<Revenue>(_baseUri + $"api/Revenues/{revenueId}");
        }

        public async Task Insert(Revenue revenue)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUri + $"api/Revenues", revenue);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-01", "Error creating the revenue");
            }
        }

        public async Task Update(Revenue revenue)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUri + $"api/Revenues/{revenue.ID}", revenue);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-02", "Error updating the revenue");
            }
        }

        public async Task Delete(params object[] keys)
        {
            int revenueId;
            int.TryParse(keys[0].ToString(), out revenueId);
            var response = await _httpClient.DeleteAsync(_baseUri + $"api/Revenues/{revenueId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-03", "Error deleting the revenue");
            }
        }
    }
}

