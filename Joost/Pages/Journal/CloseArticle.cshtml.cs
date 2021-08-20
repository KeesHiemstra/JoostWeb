using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Joost.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Joost.Pages.Journal
{
    public class CloseArticleModel : PageModel
    {
		//Reference to database
		private readonly JournalDbContext _db;

		[BindProperty]
		public Model.Journal Journal { get; set; }
		
		public SelectList Articles;

		public CloseArticleModel(JournalDbContext db) 
		{ 
			_db = db;

			List<string> articles = _db.Journals
				.AsNoTracking()
				.Where(x => x.Event == "Aangebroken")
				.Select(x => x.Message)
				.Distinct()
				.OrderBy(x => x)
				.ToList();
			
			Articles = new SelectList(articles);
		}

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
