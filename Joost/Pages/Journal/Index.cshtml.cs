using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Joost.Data;

namespace Joost.Pages.Journal
{
	public class IndexModel : PageModel
	{
		//Reference to database
		private readonly JournalDbContext _context;
		public IndexModel(JournalDbContext context) { _context = context; }

		//All records from table
		public IList<Model.Journal> Journals { get; private set; }
		public SelectList Events;
		public string SearchEvent;
		
		#region Filter on message and event
		public async Task OnGetAsync(string searchEvent, string searchString)
		{
			//List the existing events
			IQueryable<string> eventQuery = from e in _context.Journals
																			orderby e.Event
																			select e.Event;

			//Reference to the query, no data collected yet
			var _journals = from db in _context.Journals
											select db;

			if (!string.IsNullOrEmpty(searchString))
			{
				//Extend query
				_journals = _journals.Where(s => s.Message.Contains(searchString));
			}

			if (!string.IsNullOrEmpty(searchEvent))
			{
				_journals = _journals.Where(s => s.Event == searchEvent);
			}

			//Run distinct events
			Events = new SelectList(await eventQuery.Distinct().ToListAsync());
			
			//Run query in SQL
			Journals = await _journals
				.AsNoTracking()
				.OrderByDescending(l => l.DTStart)
				.OrderByDescending(l => l.DTCreation)
				.ToListAsync();
		}
		#endregion
	}
}