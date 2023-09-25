

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task2.Data;
using Task2.Repositories;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly BookStoreDbContext Context;

    public Repository(BookStoreDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}