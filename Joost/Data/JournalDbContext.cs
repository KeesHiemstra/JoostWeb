using Microsoft.EntityFrameworkCore;
using Joost.Model;

namespace Joost.Data
{
	public class JournalDbContext : DbContext
	{
		public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) { }

		public DbSet<Journal> Journals { get; set; }
	}
}