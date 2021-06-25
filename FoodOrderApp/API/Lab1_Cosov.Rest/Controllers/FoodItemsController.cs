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
    public class FoodItemsController : Controller
    {
        private readonly AzureDbContext _azureDbContext;

        public FoodItemsController(AzureDbContext azureDbContext)
        {
            _azureDbContext = azureDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<FoodItem>> Get()
        {
            return await _azureDbContext.FoodItems.ToListAsync();
        }

        [HttpGet("{CategoryID}")]
        public async Task<IEnumerable<FoodItem>> Get(int CategoryID)
        {
            var products = await _azureDbContext.FoodItems.ToListAsync();
            return products.Where(product => product.CategoryID == CategoryID).ToList();
        }

        [HttpGet("latest")]
        public async Task<IEnumerable<FoodItem>> GetLatest()
        {
            var products = await _azureDbContext.FoodItems.ToListAsync();
            return products.OrderByDescending(product => product.ProductID).Take(3);
        }
    }
}
