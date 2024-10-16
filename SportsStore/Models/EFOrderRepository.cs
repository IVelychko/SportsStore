using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public class EFOrderRepository : IOrderRepository
{
    private readonly StoreDbContext context;

    public EFOrderRepository(StoreDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Order> Orders => this.context.Orders
        .Include(o => o.Lines)
        .ThenInclude(cl => cl.Product);

    public void SaveOrder(Order order)
    {
        if (order is null)
        {
            return;
        }

        this.context.AttachRange(order.Lines.Select(l => l.Product));
        if (order.OrderID == 0)
        {
            this.context.Orders.Add(order);
        }

        this.context.SaveChanges();
    }
}
