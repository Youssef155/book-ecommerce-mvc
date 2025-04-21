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

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderFromDB = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);

        if (orderFromDB != null)
        {
            orderFromDB.OrderStatus = orderStatus;
            if(!string.IsNullOrEmpty(paymentStatus))
                orderFromDB.PaymentStatus = paymentStatus;
        }
    }

    public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
    {
        var orderFromDB = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);

        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            orderFromDB.PaymentIntentId = paymentIntentId;
            orderFromDB.PaymentDate = DateTime.Now;
        }

        if (!string.IsNullOrEmpty(sessionId))
            orderFromDB.SessionId = sessionId;

    }
}
