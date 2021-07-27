using MassTransit;
using Shared.Models.Models;
using System.Threading.Tasks;

namespace Customer.Microservice.Consumers
{
    public class ProductConsumer : IConsumer<CustomerProduct>
    {
        public async Task Consume(ConsumeContext<CustomerProduct> context)
        {
            await Task.Run(()=> { var obj = context.Message; });
        }
    }
}
