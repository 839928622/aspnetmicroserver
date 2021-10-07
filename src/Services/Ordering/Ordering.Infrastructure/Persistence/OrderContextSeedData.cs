using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
   public class OrderContextSeedData
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeedData> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext));
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {

            yield return new Order()
            {
                UserName = "Leo", FirstName = "Marsha", LastName = "Ozkaya", EmailAddress = "Leo@gmail.com",
                AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350
            };

        }
    }
}
