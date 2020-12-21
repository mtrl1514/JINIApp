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
    public class CustomerService : ICrudDataService<Customer>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public CustomerService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task<Customer> Get(params object[] keys)
        {
            int customerId;
            int.TryParse(keys[0].ToString(), out customerId);
            return await _httpClient.GetFromJsonAsync<Customer>(_baseUri + $"api/Customers/{customerId}");
        }

        public async Task Insert(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUri + $"api/Customers", customer);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-01", "Error creating the customer");
            }
        }

        public async Task Update(Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUri + $"api/Customers/{customer.ID}", customer);
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-02", "Error updating the customer");
            }
        }

        public async Task Delete(params object[] keys)
        {
            int customerId;
            int.TryParse(keys[0].ToString(), out customerId);
            var response = await _httpClient.DeleteAsync(_baseUri + $"api/Customers/{customerId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new GridException("ITEMSRV-03", "Error deleting the customer");
            }
        }
    }
}

