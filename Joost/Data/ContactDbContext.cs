using Microsoft.EntityFrameworkCore;
using Joost.Model;

namespace Joost.Data
{
	public class ContactDbContext : DbContext
	{
		public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

		public DbSet<Contact> Contact { get; set; }
	}
}
