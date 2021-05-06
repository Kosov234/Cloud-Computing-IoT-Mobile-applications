using Microsoft.EntityFrameworkCore;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AzureDbContext : DbContext
{
	public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected AzureDbContext()
    {

    }

	public DbSet<Person> People { get; set; }
}
