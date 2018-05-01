using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Joost.Data;

namespace Joost.Pages.Journal
{
	public class EditModel : PageModel
	{
		//Reference to database
		private readonly JournalDbContext _db;
		public EditModel(JournalDbContext db) { _db = db; }

		[BindProperty]
		public Model.Journal Journal { get; set; }

		#region Save button
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null) { return NotFound(); }

			Journal = await _db.Journals.SingleOrDefaultAsync(m => m.LogID == id);

			if (Journal == null)
			{
				return RedirectToPage("/Journal/Index");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_db.Attach(Journal).State = EntityState.Modified;

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException e)
			{
				//Handle deleted row
				var exceptionEntry = e.Entries.Single();
				var databaseEntry = exceptionEntry.GetDatabaseValues();
				if (databaseEntry == null)
				{
					ModelState.AddModelError(string.Empty, "Unable to save. " +
							"The journal entity was deleted by another user.");
					return Page();
				}

				ModelState.AddModelError(string.Empty, "Unable to save. " +
						"The journal entity was changed by another user.");
				return Page();
			}

			return Redirect("/Journal/Index");
		}
		#endregion

		#region Cancel button
		public IActionResult OnPostCancelAsync()
		{
			return Redirect("/Journal/Index");
		}
		#endregion
	}
}


