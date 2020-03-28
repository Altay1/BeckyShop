using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCardId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string cardId = session.GetString("cardId") ?? Guid.NewGuid().ToString();

            session.SetString("cardId", cardId);

            return new ShoppingCart(context) { ShoppingCardId = cardId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCardItemToAdd =
                _appDbContext.shoppingCartItems.SingleOrDefault(p => p.Pie.Id == pie.Id &&
                p.ShoppingCartId == ShoppingCardId);

            if(shoppingCardItemToAdd == null)
            {
                shoppingCardItemToAdd = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCardId,
                    Pie = pie,
                    Amount = 1
                };
                _appDbContext.shoppingCartItems.Add(shoppingCardItemToAdd);
            }
            else
            {
                shoppingCardItemToAdd.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shopingCartItemToRemove =
                _appDbContext.shoppingCartItems.FirstOrDefault(p => p.Pie.Id == pie.Id &&
                p.ShoppingCartId == ShoppingCardId);

            var localAmount = 0;
            if(shopingCartItemToRemove != null)
            {
                if(shopingCartItemToRemove.Amount > 1)
                {
                    shopingCartItemToRemove.Amount--;
                    localAmount = shopingCartItemToRemove.Amount;
                }
                else
                {
                    _appDbContext.Remove(shopingCartItemToRemove);
                }
            }
            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems = _appDbContext.shoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCardId)
                .Include(s => s.Pie)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .shoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCardId);

            _appDbContext.shoppingCartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalAmount = _appDbContext
                .shoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCardId)
                .Select(p => p.Pie.Price * p.Amount).Sum();

            return totalAmount;
        }
    }
}
