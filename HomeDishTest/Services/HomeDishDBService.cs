using HomeDishTest.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public class HomeDishDBService : IBasketService
    {
        public Task<Basket> GetBasketAsync(CancellationToken cancellationToken)
        {
            //Get basket data from db
            return Task.FromResult(default(Basket));

        }
    }
}
