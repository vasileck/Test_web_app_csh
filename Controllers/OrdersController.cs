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
                order.OrderNumber = Guid.NewGuid().ToString();  // Генерация UUID, если OrderNumber не задан
            }

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);  // Добавление записи в базу данных
                await _context.SaveChangesAsync();  // Сохранение изменений
                return RedirectToAction(nameof(Index));  // Перенаправление на список заказов
            }

            return View(order);  // Возврат представления с моделью, если валидация не пройдена
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
