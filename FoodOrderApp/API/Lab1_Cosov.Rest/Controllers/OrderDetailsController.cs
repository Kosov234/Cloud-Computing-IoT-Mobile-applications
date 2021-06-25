using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class OrderDetailsController : Controller
    {

        private readonly AzureDbContext _azureDbContext;

        public OrderDetailsController(AzureDbContext azureDbContext)
        {
            _azureDbContext = azureDbContext;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<OrderDetails>> Get(string id)
        {
            var orderDetails =  await _azureDbContext.OrderDetails.ToListAsync();
            return orderDetails.Where(orderDetails => orderDetails.OrderId == id).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetails>> Create([FromBody] OrderDetails orderDetails)
        {
            _azureDbContext.OrderDetails.Add(orderDetails);
            await _azureDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = orderDetails.OrderDetailId});
        }
    }
}
