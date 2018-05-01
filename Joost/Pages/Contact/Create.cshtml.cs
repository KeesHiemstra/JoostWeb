using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Joost.Data;
using Joost.Model;
using Microsoft.EntityFrameworkCore;

namespace Joost.Pages.Contact
{
	public class CreateModel : PageModel
	{
		private readonly ContactDbContext _context;
		public CreateModel(ContactDbContext context) { _context = context; }

		[BindProperty]
		public Model.Contact Contact { get; set; }

		#region Save button
		public async Task<IActionResult> OnPostSaveAsync()
		{
			if (!ModelState.IsValid) { return Page(); }

			_context.Contact.Add(Contact);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				ModelState.AddModelError(string.Empty, "Unable to save. " +
						"The contact entity was changed by another user.");
				return Page();
			}

			return RedirectToPage("/Contact/Index");
		}
		#endregion

		#region Cancel button
		public IActionResult OnPostCancel()
		{
			return Redirect("/Contact/Index");
		}
		#endregion
	}
}