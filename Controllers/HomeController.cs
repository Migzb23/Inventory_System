using Microsoft.AspNetCore.Mvc;
using Inventory_System.Data;

namespace Inventory_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var stats = new
            {
                TotalItems = _context.InventoryItems.Count(),
                TotalQuantity = _context.InventoryItems.Sum(i => i.Quantity),
                TotalValue = _context.InventoryItems.Sum(i => i.TotalValue),
                LowStockCount = _context.InventoryItems.Count(i => i.IsLowStock)
            };

            ViewBag.Stats = stats;
            return View();
        }
    }
}
