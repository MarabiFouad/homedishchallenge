using HomeDishTest.Models;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public interface IBasketCalculationService
    {
        double MinimumGrandTotal(Basket basket);
    }
}