using System.Threading;
using System.Threading.Tasks;

namespace FormsDemo.Services
{
    public interface ISlowService
    {
        Task<double> Average(double first, double second, double third, CancellationToken token);
    }
}
