using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Lab3.Web.Context;
using Mobile_Lab3.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Lab3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AzureDbContext _azureDbContext;

        public PeopleController(AzureDbContext azureDbContext)
        {
            _azureDbContext = azureDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _azureDbContext.People.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create([FromBody] Person person)
        {
            _azureDbContext.People.Add(person);
            await _azureDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = person.Id });
        }

        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetPhoto([FromRoute]int id)
        {
            var p = await _azureDbContext.People.FindAsync(id);

            return base.File(Convert.FromBase64String(p.PictureBase64), "image/jpeg");
        }
    }
}
