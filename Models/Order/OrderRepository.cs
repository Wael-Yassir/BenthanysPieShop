using System;

namespace BenthanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            this._appDbContext = appDbContext;
            this._shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Order = order,
                    Price = shoppingCartItem.Pie.Price
                };

                _appDbContext.OrderDetails.Add(orderDetail);
            }

            _appDbContext.SaveChanges();
        }

        // Using transaction to solve saving entity problem.
        private void _CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            using (var transaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var shoppingCartItems = _shoppingCart.ShoppingCartItems;
                    _appDbContext.Orders.Add(order);
                    _appDbContext.SaveChanges();

                    foreach (var shoppingCartItem in shoppingCartItems)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Amount = shoppingCartItem.Amount,
                            PieId = shoppingCartItem.Pie.PieId,
                            Order = order,
                            Price = shoppingCartItem.Pie.Price
                        };

                        _appDbContext.OrderDetails.Add(orderDetail);
                    }

                    _appDbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
