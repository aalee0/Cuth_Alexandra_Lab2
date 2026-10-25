using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cuth_Alexandra_Lab2.Models;

namespace Cuth_Alexandra_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Cuth_Alexandra_Lab2Context _context;

    public IndexModel(Cuth_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
