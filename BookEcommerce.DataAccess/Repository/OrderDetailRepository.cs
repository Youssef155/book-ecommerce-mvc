using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;

namespace BookEcommerce.DataAccess.Repository;

public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _context;
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderDetail orderDetail)
    {
        _context.Update(orderDetail);
    }
}