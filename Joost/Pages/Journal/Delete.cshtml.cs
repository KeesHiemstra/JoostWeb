using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Joost.Data;

namespace Joost.Pages.Journal
{
	public class DeleteModel : PageModel
	{
		//Reference to database
		private readonly JournalDbContext _db;
		public DeleteModel(JournalDbContext db) { _db = db; }

		[BindProperty]
		public Model.Journal Journal { get; set; }

		#region Delete button
		public async Task<IActionResult> OnGet(int ID)
		{
			Journal = await _db.Journals.FindAsync(ID);

			if (Journal == null)
			{
				return RedirectToPage("/Journal/Index");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int ID)
		{
			var journal = await _db.Journals.FindAsync(ID);

			if (journal != null)
			{
				_db.Journals.Remove(journal);
				await _db.SaveChangesAsync();
			}

			return RedirectToPage();
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