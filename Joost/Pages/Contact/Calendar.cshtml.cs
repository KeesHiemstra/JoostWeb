using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Joost.Data;

namespace Joost.Pages.Contact
{
	public class CalendarModel : PageModel
	{
		//Reference to database
		private readonly ContactDbContext _context;
		public CalendarModel(ContactDbContext context) { _context = context; }

		//All record from Calender table
		public IList<Model.Contact> Contact { get; private set; }

		public void OnGet()
		{
			//It can't await and an extension together
			Contact = _context.Contact
				.AsNoTracking()
				.AsEnumerable()
				.OrderBy(m => m.BirthDate.OrderByMonthDayRotering())
				.ToList();
		}
	}
}
