using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                order.OrderNumber = Guid.NewGuid().ToString();  
            }

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);  
                await _context.SaveChangesAsync();  
                return RedirectToAction(nameof(Index));  
            }

            return View(order);  
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
