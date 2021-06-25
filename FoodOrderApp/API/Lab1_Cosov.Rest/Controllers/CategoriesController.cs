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
    public class CategoriesController : Controller
    {
        private readonly AzureDbContext _azureDbContext;

        public CategoriesController(AzureDbContext azureDbContext)
        {
            _azureDbContext = azureDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _azureDbContext.Categories.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category category)
        {
            _azureDbContext.Categories.Add(category);
            await _azureDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = category.CategoryID });
        }
    }
}
