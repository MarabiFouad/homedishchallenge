using HomeDishTest.Models;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public class HomeDishDBService : IBasketService
    {
        public Task<Basket> GetBasketAsync()
        {
            //Get basket data from db

            return Task.FromResult(default(Basket));

        }
    }
}
