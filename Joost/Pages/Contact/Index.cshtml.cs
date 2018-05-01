using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Joost.Data;
using Joost.Model;

namespace Joost.Pages.Contact
{
	public class IndexModel : PageModel
	{
		private readonly ContactDbContext _context;
		public IndexModel(ContactDbContext context) { _context = context; }

		public IList<Model.Contact> Contact { get; private set; }

		public async Task OnGetAsync()
		{
			Contact = await _context.Contact
				.AsNoTracking()
				.OrderBy(m => m.Name)
				.ToListAsync();
		}
	}
}
