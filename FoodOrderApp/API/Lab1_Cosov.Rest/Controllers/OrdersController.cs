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
    public class OrdersController : Controller
    {
        private readonly AzureDbContext _azureDbContext;

        public OrdersController(AzureDbContext azureDbContext)
        {
            _azureDbContext = azureDbContext;
        }


        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _azureDbContext.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Order> Get(int id)
        {
            return await _azureDbContext.Orders.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            _azureDbContext.Orders.Add(order);
            await _azureDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = order.OrderId });
        }


    }
}
