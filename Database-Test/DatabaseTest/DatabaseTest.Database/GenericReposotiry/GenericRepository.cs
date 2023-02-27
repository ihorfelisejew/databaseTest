using Microsoft.EntityFrameworkCore;

namespace DatabaseTest.Database.GenericReposotiry
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext _context;
        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public GenericRepository(DbContext context)
        {
            _context = context;
            _context.Set<TEntity>();
        }
        public IEnumerable<TEntity> Get()
        {
            return Table.AsNoTracking().ToList();
        }
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return Table.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity FindById(int id)
        {
            return Table.Find(id);
        }
        public void Create(TEntity item)
        {
            Table.Add(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            Table.Remove(item);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
