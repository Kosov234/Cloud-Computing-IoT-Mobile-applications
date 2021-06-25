using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AzureDbContext : IdentityDbContext

{
	public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<FoodItem> FoodItems { get; set; }
	public DbSet<OrderDetails> OrderDetails { get; set; }
	public DbSet<Order> Orders { get; set; }
}
