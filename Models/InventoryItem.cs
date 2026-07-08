namespace Inventory_System.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; } = 10;
        
        // Pricing fields
        public decimal UnitPrice { get; set; }
        public decimal TotalValue => Quantity * UnitPrice;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool IsLowStock => Quantity <= LowStockThreshold;
    }
}
