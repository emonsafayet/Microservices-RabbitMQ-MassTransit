using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;
using System;
using System.Threading.Tasks;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductController : ControllerBase
    {
        private readonly IBus _busService;

        public CustomerProductController(IBus busService)
        {
            _busService = busService;
        }
        [HttpPost]
        public async Task<string> CreateProduct(CustomerProduct product)
        {
            if(product != null)
            {
                product.AddOnDate = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/productQueue");
                var endpoint = await _busService.GetSendEndpoint(uri);
                await endpoint.Send(product);
                return "true";
            }
            return "false";
        }
    }
}
