using System;
using System.Threading.Tasks;

using Joost.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Joost.Pages.Journal
{
	public class CreateModel : PageModel
	{
		//Reference to database
		private readonly JournalDbContext _db;
		public CreateModel(JournalDbContext db) { _db = db; }

		[BindProperty]
		public Model.Journal Journal { get; set; }

		#region Save button
		public async Task<IActionResult> OnPostSaveAsync()
		{
			if (!ModelState.IsValid) { return Page(); }

			Journal.DTCreation = DateTime.Now;
			_db.Journals.Add(Journal);

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				ModelState.AddModelError(string.Empty, "Unable to save. " +
						"The journal entity was changed by another user.");
				return Page();
			}
			return Redirect("/Journal/Index");
		}
		#endregion

		#region Cancel button
		public IActionResult OnPostCancel()
		{
			return Redirect("/Journal/Index");
		}
		#endregion
	}
}