using BookBazaar.Models;

namespace BookBazaar.DataAccess.Repository.IRepository
{
    public interface IShoppingCardRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
    }
}
