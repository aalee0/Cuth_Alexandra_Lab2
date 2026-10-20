using Microsoft.EntityFrameworkCore;

public class Cuth_Alexandra_Lab2Context(DbContextOptions<Cuth_Alexandra_Lab2Context> options) : DbContext(options)
{
    public DbSet<Cuth_Alexandra_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Cuth_Alexandra_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Cuth_Alexandra_Lab2.Models.Author> Author { get; set; } = default!;
}

