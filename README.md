# Inventory Management System

A comprehensive .NET Core web application for managing inventory with pricing, stock tracking, and detailed reporting.

## Features

✅ **Complete CRUD Operations**
- Create, Read, Update, and Delete inventory items
- Add detailed item descriptions and categories

✅ **Pricing Management**
- Set unit prices for each item
- Automatic calculation of total inventory value
- Track total value per category

✅ **Stock Tracking**
- Real-time quantity management
- Configurable low-stock thresholds
- Visual alerts for low stock items

✅ **Advanced Search & Filtering**
- Search items by name or description
- Filter by category
- Sort by name, quantity, price, or total value

✅ **Comprehensive Reports**
- Dashboard with inventory statistics
- Low stock alerts
- Category-wise breakdown
- Total inventory value tracking

✅ **Responsive Design**
- Mobile-friendly interface
- Bootstrap 5 styling
- Clean and intuitive UI

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQLite (lightweight and portable)
- **ORM**: Entity Framework Core
- **Frontend**: Razor Views, Bootstrap 5
- **Icons**: Bootstrap Icons

## Project Structure

```
Inventory_System/
├── Models/
│   └── InventoryItem.cs          # Data model with pricing
├── Data/
│   └── ApplicationDbContext.cs    # Database context
├── Controllers/
│   ├── InventoryController.cs     # Inventory operations
│   └── HomeController.cs          # Dashboard
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Dashboard
│   ├── Inventory/
│   │   ├── Index.cshtml           # Item list
│   │   ├── Create.cshtml          # Add item
│   │   ├── Edit.cshtml            # Edit item
│   │   ├── Delete.cshtml          # Delete confirmation
│   │   ├── Details.cshtml         # Item details
│   │   └── Report.cshtml          # Reports & analytics
│   └── Shared/
│       └── _Layout.cshtml         # Master layout
├── Program.cs                     # Application configuration
├── appsettings.json              # Settings
└── Inventory_System.csproj        # Project file
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio, VS Code, or any .NET IDE

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Migzb23/Inventory_System.git
   cd Inventory_System
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update the database**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   Navigate to `https://localhost:5001` or `http://localhost:5000`

## Usage

### Dashboard
- View quick statistics about your inventory
- See total items, quantities, values, and low stock count

### Add Items
1. Click "Add Item" from the navigation bar
2. Fill in the item details:
   - Name (required)
   - Category (required)
   - Quantity (required)
   - Unit Price (required) - pricing feature
   - Description (optional)
   - Low Stock Threshold (optional, default: 10)
3. Click "Create Item"

### View Inventory
1. Go to the Inventory page
2. Use search, category filters, and sorting options
3. View total value calculations with pricing

### Edit Items
1. Click "Edit" on any item
2. Modify the details
3. Total value updates automatically
4. Click "Update Item"

### Delete Items
1. Click "Delete" on any item
2. Review the item details
3. Confirm the deletion

### Generate Reports
1. Click "Reports" in the navigation
2. View:
   - Low stock items
   - Category breakdown with values
   - Complete inventory listing

## InventoryItem Model

```csharp
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Quantity { get; set; }
    public int LowStockThreshold { get; set; } = 10;
    
    // Pricing fields
    public decimal UnitPrice { get; set; }
    public decimal TotalValue => Quantity * UnitPrice;
    
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsLowStock => Quantity <= LowStockThreshold;
}
```

## Key Features Explained

### Pricing System
- **Unit Price**: Price per single item
- **Total Value**: Automatically calculated (Quantity × Unit Price)
- **Category Values**: Aggregate totals shown in reports

### Low Stock Management
- Set custom thresholds per item
- Visual alerts in inventory list
- Dedicated low stock report

### Search & Sort
- Full-text search on name and description
- Filter by category
- Sort by name, quantity, price, or total value
- Quick access to low stock items

## Future Enhancements

- Export to Excel/PDF
- Inventory history and audit trail
- Transaction logging (add/remove stock)
- Email notifications for low stock
- User authentication and roles
- API endpoints for integration
- Dark mode support
- Mobile app

## License

This project is open source and available for educational and commercial use.

## Support

For issues or questions, please open an issue on GitHub.

## Author

Migzb23

---

**Made with ❤️ using .NET Core**
