using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext,
             ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var cartItems = _shoppingCart.ShoppingCartItems;

            foreach(var items in cartItems)
            {
                var orderDetails = new OrderDetail()
                {
                    Amount = items.Amount,
                    OrderId = order.Id,
                    PieId = items.Pie.Id,
                    Price = items.Pie.Price
                };
                _appDbContext.OrderDetails.Add(orderDetails);
            }
            _appDbContext.SaveChanges();
        }
    }
}
