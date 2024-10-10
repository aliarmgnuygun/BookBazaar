using BookBazaar.DataAccess.Data;
using BookBazaar.DataAccess.Repository.IRepository;
using BookBazaar.Models;

namespace BookBazaar.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
