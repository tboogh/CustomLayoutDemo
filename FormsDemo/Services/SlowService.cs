using System.Threading;
using System.Threading.Tasks;

namespace FormsDemo.Services
{
    public class SlowService : ISlowService
    {
        public async Task<double> Average(double first, double second, double third, CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine("Delay start");
            await Task.Delay(2000, token);
            System.Diagnostics.Debug.WriteLine("Delay end");
            return (first + second + third)/3;
        }
    }
}