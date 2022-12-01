using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SuperHero.Models;

namespace SuperHero.Data {
	public class DataContext : DbContext {
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<SuperHeros> SuperHeroes { get; set; }
	}
}
