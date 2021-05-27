using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_Cosov.Rest.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            return await _azureDbContext.People.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create([FromBody] Person person)
        {
            _azureDbContext.People.Add(person);
            await _azureDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = person.PersonId });
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, Person person)
        {

            if (id != person.PersonId) return BadRequest();

            _azureDbContext.Entry(person).State = EntityState.Modified;

            await _azureDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(await Get(id) == null) return NotFound();

            var personToDelete = await _azureDbContext.People.FindAsync(id);
            _azureDbContext.People.Remove(personToDelete);

            await _azureDbContext.SaveChangesAsync();

            return NoContent();
        }






    }
}
