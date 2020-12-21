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
    public class SalesOrderService : ICrudDataService<SalesOrder>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public SalesOrderService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task<SalesOrder> Get(params object[] keys)
        {
            int salesOrderId;
            int.TryParse(keys[0].ToString(), out salesOrderId);
            return await _httpClient.GetFromJsonAsync<SalesOrder>(_baseUri + $"api/SalesOrders/{salesOrderId}");
        }

        public async Task Insert(SalesOrder salesOrder)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUri + $"api/SalesOrders", salesOrder);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-01", "Error creating the salesOrder");
            }
        }

        public async Task Update(SalesOrder salesOrder)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUri + $"api/SalesOrders/{salesOrder.ID}", salesOrder);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-02", "Error updating the salesOrder");
            }
        }

        public async Task Delete(params object[] keys)
        {
            int salesOrderId;
            int.TryParse(keys[0].ToString(), out salesOrderId);
            var response = await _httpClient.DeleteAsync(_baseUri + $"api/SalesOrders/{salesOrderId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-03", "Error deleting the salesOrder");
            }
        }
    }
}

