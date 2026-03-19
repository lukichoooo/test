using Microsoft.EntityFrameworkCore;

namespace tmp;

public class SeedService(AppDbContext context)
{
    private static readonly Random _random = new();

    public async Task SeedAsync()
    {
        if (await context.Users.AnyAsync()) return;

        var users = Enumerable.Range(1, 10).Select(i => new User
        {
            Id = i,
            UpdatedAt = DateTime.UtcNow.AddDays(-_random.Next(1, 30))
        }).ToList();

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        var notebooks = users.Select(u => new Notebook
        {
            UserId = u.Id,
            Notes = $"Notes for user {u.Id}: {Guid.NewGuid().ToString()[..8]}"
        }).ToList();

        context.Notebooks.AddRange(notebooks);
        await context.SaveChangesAsync();
    }
}
