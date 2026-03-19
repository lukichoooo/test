using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tmp;

[ApiController]
[Route("api/[controller]")]
public class UserController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll() =>
        await context.Users.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get([FromRoute] int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpGet("/with-notebook")]
    public async Task<ActionResult<User>> GetWithNotebook()
    {
        var user = await context.Users
            .Include(u => u.Notebook)
            .FirstOrDefaultAsync(u => u.Id == 1);

        if (user == null) return NotFound();
        return user;
    }

    [HttpGet("/with-notebook/2")]
    public async Task<ActionResult<User?>> GetWithNotebook2()
        => await Fetch2(1, u => u.Notebook!);

    public Task<User?> Fetch2(int id, params Expression<Func<User, object>>[] includeProperties)
    {
        IQueryable<User> query = context.Users;
        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);

        return query.FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpGet("/with-notebook/3")]
    public async Task<ActionResult<User?>> GetWithNotebook3()
        => await Fetch3(1, u => u.Notebook!);

    public Task<User> Fetch3(int id, params Expression<Func<User, object>>[] includeProperties)
      => context.Users.Include(includeProperties).FirstOrDefaultAsync(e => e.Id == id)!;
}
