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
    public class SalesOrderItemService : ICrudDataService<SalesOrderItem>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public SalesOrderItemService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task<SalesOrderItem> Get(params object[] keys)
        {
            int salesOrderItemId;
            int.TryParse(keys[0].ToString(), out salesOrderItemId);
            return await _httpClient.GetFromJsonAsync<SalesOrderItem>(_baseUri + $"api/SalesOrderItems/{salesOrderItemId}");
        }

        public async Task Insert(SalesOrderItem salesOrderItem)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUri + $"api/SalesOrderItems", salesOrderItem);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-01", "Error creating the salesOrderItem");
            }
        }

        public async Task Update(SalesOrderItem salesOrderItem)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUri + $"api/SalesOrderItems/{salesOrderItem.ID}", salesOrderItem);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-02", "Error updating the salesOrderItem");
            }
        }

        public async Task Delete(params object[] keys)
        {
            int salesOrderItemId;
            int.TryParse(keys[0].ToString(), out salesOrderItemId);
            var response = await _httpClient.DeleteAsync(_baseUri + $"api/SalesOrderItems/{salesOrderItemId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-03", "Error deleting the salesOrderItem");
            }
        }
    }
}

