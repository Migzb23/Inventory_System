using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_System.Data;
using Inventory_System.Models;

namespace Inventory_System.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ApplicationDbContext context, ILogger<InventoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Inventory
        public async Task<IActionResult> Index(string? search, string? category, string? sortBy = "name")
        {
            try
            {
                IQueryable<InventoryItem> query = _context.InventoryItems;

                // Search
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(i => i.Name.Contains(search) || i.Description.Contains(search));
                }

                // Filter by category
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(i => i.Category == category);
                }

                // Sort
                query = sortBy?.ToLower() switch
                {
                    "quantity" => query.OrderBy(i => i.Quantity),
                    "price" => query.OrderBy(i => i.UnitPrice),
                    "value" => query.OrderByDescending(i => i.TotalValue),
                    "lowstock" => query.Where(i => i.IsLowStock).OrderBy(i => i.Quantity),
                    _ => query.OrderBy(i => i.Name)
                };

                var items = await query.ToListAsync();
                var categories = await _context.InventoryItems
                    .Select(i => i.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToListAsync();

                ViewBag.Categories = categories;
                ViewBag.CurrentCategory = category;
                ViewBag.CurrentSort = sortBy;
                ViewBag.SearchTerm = search;

                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inventory items");
                return BadRequest("An error occurred while retrieving inventory items.");
            }
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Category,Quantity,LowStockThreshold,UnitPrice")] InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedDate = DateTime.Now;
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Item '{item.Name}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating inventory item");
                    ModelState.AddModelError("", "An error occurred while creating the item.");
                }
            }
            return View(item);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Category,Quantity,LowStockThreshold,UnitPrice,CreatedDate")] InventoryItem item)
        {
            if (id != item.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    item.UpdatedDate = DateTime.Now;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Item '{item.Name}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error updating inventory item");
                    ModelState.AddModelError("", "An error occurred while updating the item. The item may have been deleted.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating inventory item");
                    ModelState.AddModelError("", "An error occurred while updating the item.");
                }
            }
            return View(item);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var item = await _context.InventoryItems.FindAsync(id);
                if (item != null)
                {
                    _context.InventoryItems.Remove(item);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Item '{item.Name}' deleted successfully!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting inventory item");
                TempData["Error"] = "An error occurred while deleting the item.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Inventory/Report
        public async Task<IActionResult> Report()
        {
            try
            {
                var items = await _context.InventoryItems.ToListAsync();
                var report = new
                {
                    TotalItems = items.Count,
                    TotalQuantity = items.Sum(i => i.Quantity),
                    TotalValue = items.Sum(i => i.TotalValue),
                    LowStockItems = items.Where(i => i.IsLowStock).ToList(),
                    ByCategory = items.GroupBy(i => i.Category)
                        .Select(g => new { Category = g.Key, Count = g.Count(), Value = g.Sum(i => i.TotalValue) })
                        .ToList(),
                    AllItems = items
                };
                return View(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating inventory report");
                return BadRequest("An error occurred while generating the report.");
            }
        }
    }
}
