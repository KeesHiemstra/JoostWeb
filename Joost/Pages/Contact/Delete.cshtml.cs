using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Joost.Data;

namespace Joost.Pages.Contact
{
	public class DeleteModel : PageModel
	{
		private readonly ContactDbContext _context;
		public DeleteModel(ContactDbContext context) { _context = context; }

		[BindProperty]
		public Model.Contact Contact { get; set; }

		#region Delete button
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

		public async Task<IActionResult> OnPostDeleteAsync(int? id)
		{
			if (id == null) { return NotFound(); }

			Contact = await _context.Contact.FindAsync(id);

			if (Contact != null)
			{
				_context.Contact.Remove(Contact);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("/Contact/Index");
		}
		#endregion

		#region Cancel button
		public IActionResult OnPostCancelAsync()
		{
			return Redirect("/Contact/Index");
		}
		#endregion
	}
}
