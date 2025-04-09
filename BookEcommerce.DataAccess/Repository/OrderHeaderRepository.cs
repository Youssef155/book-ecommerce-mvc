using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;

namespace BookEcommerce.DataAccess.Repository;

public class OrderHeaderRepository : RepositoryBase<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderHeaderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderHeader orderHeader)
    {
        _context.Update(orderHeader);
    }
}
