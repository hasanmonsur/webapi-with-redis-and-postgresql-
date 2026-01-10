# Product API - .NET Core 8 Web API with PostgreSQL & Redis

A high-performance Web API for product management with PostgreSQL database and Redis caching.

## 🚀 Features

- **CRUD Operations**: Full Create, Read, Update, Delete functionality for products
- **Search Functionality**: Search products by name or description
- **PostgreSQL Integration**: Robust relational database backend
- **Redis Caching**: High-speed caching for frequently accessed products
- **Swagger UI**: Interactive API documentation and testing
- **Performance Optimized**: Designed for large-scale transactional workloads

## 📦 Technologies Used

- **.NET Core 8**: Latest .NET framework
- **Entity Framework Core**: ORM for database operations
- **PostgreSQL**: Relational database (Host=postgres;Port=5432;Database=dbproduct)
- **Redis**: In-memory caching (localhost:6379)
- **Swashbuckle/Swagger**: API documentation
- **Npgsql**: PostgreSQL provider for .NET

## 🏗️ Architecture

```
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│   Client    │───▶│   Web API   │───▶│  PostgreSQL  │
└─────────────┘    └─────────────┘    └─────────────┘
       ▲                  │
       │                  ▼
       │            ┌─────────────┐
       └───────────│    Redis    │
                   └─────────────┘
```

## 🔧 Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/product-api.git
   cd product-api
   ```

2. **Install dependencies**:
   ```bash
   dotnet restore
   ```

3. **Set up database**:
   ```bash
   dotnet ef database update
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID (cached) |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update product |
| DELETE | `/api/products/{id}` | Delete product |
| GET | `/api/products/search?q={term}` | Search products |

## 📦 Project Structure

```
ProductApi/
├── Controllers/          # API controllers
├── Data/                 # Database context
├── Models/               # Data models
├── Services/             # Business logic and caching
├── Migrations/           # Database migrations
├── appsettings.json      # Configuration
└── Program.cs            # Application entry point
```

## 🔧 Configuration

**Database Configuration** (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgres;Port=5432;Database=dbproduct;Username=postgres;Password=postgres"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

## 🚀 Performance Features

- **Redis Caching**: 30-minute cache expiration for product data
- **Cache-Aside Pattern**: Read-through caching strategy
- **Write-Through Caching**: Automatic cache updates on writes
- **Optimized Queries**: Efficient database operations

## 📖 Product Model

```csharp
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
```

## 🔗 Dependencies

- `Npgsql.EntityFrameworkCore.PostgreSQL` - PostgreSQL provider
- `StackExchange.Redis` - Redis client
- `Swashbuckle.AspNetCore` - Swagger UI
- `Microsoft.EntityFrameworkCore.Design` - EF Core tools

## 🌐 Access the API

- **Base URL**: `http://localhost:5044`
- **Swagger UI**: `http://localhost:5044/swagger`
- **Health Check**: `http://localhost:5044/health`

## 🛠️ Development

1. **Add new migration**:
   ```bash
   dotnet ef migrations add MigrationName
   ```

2. **Apply migrations**:
   ```bash
   dotnet ef database update
   ```

3. **Run tests**:
   ```bash
   dotnet test
   ```

## 📝 License

MIT License

## 🤝 Contributing

Contributions are welcome! Please open an issue or submit a pull request.

---

**Built with ❤️ using .NET Core 8, PostgreSQL, and Redis**