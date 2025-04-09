using BookEcommerce.Models;

namespace BookEcommerce.DataAccess.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepositoryBase<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
