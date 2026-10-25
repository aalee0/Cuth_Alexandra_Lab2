using Cuth_Alexandra_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cuth_Alexandra_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Cuth_Alexandra_Lab2Context _context;

    public EditModel(Cuth_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories).ThenInclude(b => b.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);

        var authorList = _context.Author.Select(x => new
        {
            x.ID,
            FullName = x.FirstName + " " + x.LastName
        });

        ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName", Book.AuthorID);
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName", Book.PublisherID);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i => i.Author)
            .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title, i => i.AuthorID,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        UpdateBookCategories(_context, selectedCategories, bookToUpdate);
        PopulateAssignedCategoryData(_context, bookToUpdate);

        var authorList = _context.Author.Select(x => new
        {
            x.ID,
            FullName = x.FirstName + " " + x.LastName
        });

        ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName", bookToUpdate.AuthorID);
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName", bookToUpdate.PublisherID);

        return Page();
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}