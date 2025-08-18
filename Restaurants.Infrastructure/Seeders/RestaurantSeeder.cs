using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
                new()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dished =
                    [
                        new ()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M,
                        },
                        new ()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M,
                        },
                    ],
                    Address = new ()
                    {
                        Street = "123 Chicken St",
                        City = "Louisville",
                        PostalCode = "40202"
                    }
                },
                new ()
                {
                    Name = "Pizza Hut",
                    Category = "Pizza",
                    Description = "Pizza Hut is an American restaurant chain and international franchise known for pizza, pasta, and side dishes.",
                    ContactEmail = "contact@pizzahut.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        Street = "456 Pizza",
                        City = "Dallas",
                        PostalCode = "75201"
                    },
                }
                ];
            return restaurants;
        }
    }
}
