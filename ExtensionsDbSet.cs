using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static void EnsureAttached<T>(this DbSet<T> dbSet, T entity) where T : class
    {
        if (dbSet.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Add(entity);
        }
    }

    public static void EnsureDeleted<T>(this DbSet<T> dbSet, T entity) where T : class
    {
        dbSet.EnsureAttached(entity);

        if (dbSet.Entry(entity).State != EntityState.Deleted)
        {
            dbSet.Remove(entity);
        }
    }

    public static void MarkModified<T>(this DbSet<T> dbSet, T entity) where T : class
    {
        dbSet.Entry(entity).State = EntityState.Modified;
    }

    public static DbSet<T> Include<T>(this DbSet<T> dbSet, Expression<Func<T, object>>[] includeExpressions) where T : class
    {
        if (includeExpressions is not null)
        {
            foreach (var includeExpression in includeExpressions)
            {
                dbSet.Include(includeExpression);
            }
        }

        return dbSet;
    }
}

