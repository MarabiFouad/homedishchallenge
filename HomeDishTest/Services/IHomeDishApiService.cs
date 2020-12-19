using HomeDishTest.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(CancellationToken cancellatointoken);
    }
}