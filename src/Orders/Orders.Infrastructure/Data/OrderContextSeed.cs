using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryCount = retry.Value;

            try
            {
                context.Database.Migrate();

                if (!await context.Orders.AnyAsync())
                {
                    context.Orders.AddRange(GetPreConfiguredOrders());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryCount < 3)
                {
                    retryCount++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryCount);
                }
            }
        }

        private static List<Order> GetPreConfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "AliShawky",
                    FirstName = "Ali",
                    LastName = "Shawky",
                    EmailAddress = "ali.shawky@gmail.com",
                    AddressLine = "Maadi",
                    Country = "Sweden"
                }
            };
        }
    }
}
