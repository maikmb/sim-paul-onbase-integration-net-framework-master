using Newtonsoft.Json;
using SimPaulOnbase.Core.Boundaries.Auth;
using SimPaulOnbase.Core.Boundaries.Customers;
using SimPaulOnbase.Core.Domain;
using SimPaulOnbase.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimPaulOnbase.Infraestructure.ApiDataAccess
{
    /// <summary>
    /// CustomerRepository class
    /// </summary>
    public class CustomerApiRepository : ICustomerRepository
    {
        private CustomerApiSettings _customerApiSettings;


        /// <summary>
        /// CustomerRepository constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="customerApiSettings"></param>
        public CustomerApiRepository(CustomerApiSettings customerApiSettings)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            _customerApiSettings = customerApiSettings;

        }

        public async Task<List<CustomerTransactional>> GetIncompleted()
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var responseMessage = _client.GetAsync(_customerApiSettings.IncompletedResource)
                .GetAwaiter()
                .GetResult();

            responseMessage.EnsureSuccessStatusCode();

            var contentResponse = responseMessage.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var divergedRegistrations = JsonConvert.DeserializeObject<List<CustomerTransactional>>(contentResponse);
            return divergedRegistrations;

        }

        /// <summary>
        /// Get authentication token
        /// </summary>
        /// <returns></returns>
        private async Task<LoginOutput> Login(LoginInput input)
        {

            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = _client.PostAsync(_customerApiSettings.LoginResource, content)
                .GetAwaiter()
                .GetResult();

            var contentResponse = await responseMessage.Content
                .ReadAsStringAsync();

            responseMessage.EnsureSuccessStatusCode();
            var output = JsonConvert.DeserializeObject<LoginOutput>(contentResponse);
            return output;
        }

        public async Task<LoginOutput> Authenticate()
        {
            var auth = await this.Login(new LoginInput
            {
                Login = _customerApiSettings.UserLogin,
                Password = _customerApiSettings.PasswordLogin,
            });

            return auth;
        }

        public async Task ApproveRegistration(CustomerApproveInput input)
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var responseMessage = _client.PostAsync(_customerApiSettings.ApproveResource, content)
                .GetAwaiter()
                .GetResult();

            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task ReproveRegistration(CustomerReproveInput input)
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var payload = JsonConvert.SerializeObject(new { status = input.Status });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var url = _customerApiSettings.ReproveResource.Replace("{id}", input.Id.ToString());
            var responseMessage = _client.PutAsync(url, content)
                .GetAwaiter()
                .GetResult();

            var contentResponse = await responseMessage.Content
               .ReadAsStringAsync();

            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<List<CustomerOnboard>> GetCustomer(string document = "")
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var url = $"{_customerApiSettings.CustomerResource}";

            if (!string.IsNullOrEmpty(document))
                url += $"?cpf={document}";

            var responseMessage = _client.GetAsync(url)
                .GetAwaiter()
                .GetResult();

            responseMessage.EnsureSuccessStatusCode();

            string responseContent = responseMessage.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var customer = JsonConvert.DeserializeObject<List<CustomerOnboard>>(responseContent);
            return customer;
        }

        public async Task<Suitability> GetCustomerSuitability(CustomerOnboard customer)
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var url = $"{_customerApiSettings.SuitabilityResource}".Replace("{id}", customer.Id.ToString());

            var responseMessage = _client.GetAsync(url)
                .GetAwaiter()
                .GetResult();

            responseMessage.EnsureSuccessStatusCode();

            string responseContent = responseMessage.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var suitability = JsonConvert.DeserializeObject<Suitability>(responseContent);
            return suitability;
        }

        public async Task<List<CustomerTransactional>> GetRegisterAgain()
        {
            var auth = await this.Authenticate();

            var _client = new HttpClient();
            _client.BaseAddress = new Uri(_customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", auth.Token);

            var responseMessage = _client.GetAsync(_customerApiSettings.IncompletedResource)
                .GetAwaiter()
                .GetResult();

            responseMessage.EnsureSuccessStatusCode();

            var contentResponse = responseMessage.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var divergedRegistrations = JsonConvert.DeserializeObject<List<CustomerTransactional>>(contentResponse);
            return divergedRegistrations;
        }
    }
}
