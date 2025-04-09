using BookEcommerce.Models;

namespace BookEcommerce.DataAccess.Repository.Interfaces
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
