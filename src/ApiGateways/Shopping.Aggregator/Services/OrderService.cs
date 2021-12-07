using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{

    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/Order/{userName}");
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadFromJsonAsync<List<OrderResponseModel>>();
            if (res == null)
                return new List<OrderResponseModel>();
            return res;
        }
    }

}
