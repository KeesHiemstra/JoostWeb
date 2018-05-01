using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Joost.Data;
using Joost.Model;

namespace Joost.Pages.Contact
{
	public class EditModel : PageModel
	{
		private readonly ContactDbContext _context;

		public EditModel(ContactDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Model.Contact Contact { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null) { return NotFound(); }

			Contact = await _context.Contact.SingleOrDefaultAsync(m => m.ContactID == id);

			if (Contact == null)
			{
				return RedirectToPage("/Contact/Index");
			}
			return Page();
		}

		#region Save button
		public async Task<IActionResult> OnPostSaveAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Contact).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException e)
			{
				//Handle deleted row
				var exceptionEntry = e.Entries.Single();
				var databaseEntry = exceptionEntry.GetDatabaseValues();
				if (databaseEntry == null)
				{
					ModelState.AddModelError(string.Empty, "Unable to save. " +
							"The contact entity was deleted by another user.");
					return Page();
				}

				ModelState.AddModelError(string.Empty, "Unable to save. " +
						"The contact entity was changed by another user.");
				return Page();
			}

			return RedirectToPage("/Contact/Index");
		}
		#endregion

		#region Cancel button
		public IActionResult OnPostCancelAsync()
		{
			return Redirect("/Journal/Index");
		}
		#endregion

		//private bool ContactExists(int id)
		//{
		//	return _context.Contact.Any(e => e.ContactID == id);
		//}
	}
}
