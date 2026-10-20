using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cuth_Alexandra_Lab2.Models;

namespace Cuth_Alexandra_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Cuth_Alexandra_Lab2Context _context;

    public IndexModel(Cuth_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Publisher = await _context.Publisher
            .Include(p => p.Books)
            .ToListAsync();
    }
}
