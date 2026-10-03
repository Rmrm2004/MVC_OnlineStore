using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_OnlineStore.Data;
using MVC_OnlineStore.Models;
using MVC_OnlineStore.ViewModels;

namespace MVC_OnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Products = _context.products.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerName = model.CustomerName,
                    CustomerAddress = model.CustomerAddress,
                    OrderDate = DateTime.Now,
                    TotalPrice = 0
                };

                _context.Orders.Add(order);

                _context.SaveChanges();

                decimal total = 0;

                foreach (var item in model.Items)
                {
                    var product = _context.products.Find(item.ProductId);

                    if (product != null)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            UnitPrice = product.Price
                        };

                        total += product.Price * item.Quantity;

                        _context.OrderItems.Add(orderItem);
                    }
                }

                order.TotalPrice = total;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Products = _context.products.ToList();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);

                _context.Orders.Remove(order);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}