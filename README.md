# Restaurants Clean Architecture API

A .NET solution following Clean Architecture to manage restaurants, exposing a RESTful API with Swagger UI.

## Solution Structure

- `Restaurants.Domain` (Entities only)
  - Core business objects without dependencies on other projects
  - Entities: `Restaurant`, `Dish`, `Address`
- `Restaurants.Application` (Use-case contracts and DTOs)
  - Interfaces used by the presentation layer
  - Request/response models (DTOs) decoupled from EF entities
  - Files:
    - `Restaurants/Interfaces/IRestaurantService.cs`
    - `Restaurants/Models/{CreateRestaurantRequest, UpdateRestaurantRequest, RestaurantDto}.cs`
    - `Extensions/ServiceCollectionExtensions.cs` (Application DI entrypoint)
- `Restaurants.Infrastructure` (Implementations and persistence)
  - EF Core `RestaurantsDbContext`, migrations, seeders
  - Service implementations that fulfill application interfaces
  - Files:
    - `Persistence/RestaurantsDbContext.cs`
    - `Restaurants/RestaurantService.cs` (implements `IRestaurantService`)
    - `Extensions/ServiceCollectionExtensions.cs` (registers EF, seeders, and services)
    - `Seeders/{IRestaurantSeeder, RestaurantSeeder}.cs`
- `Restaurants.API` (Presentation layer)
  - ASP.NET Core Web API controllers
  - Configures Swagger and DI
  - Files:
    - `Controllers/RestaurantsController.cs` (CRUD endpoints)
    - `Program.cs` (adds Swagger, Application, and Infrastructure)

Dependency rules:

- API → Application (+ Infrastructure for DI and EF)
- Infrastructure → Application (to implement contracts)
- Application → Domain
- Domain → none

## Domain Model

- `Restaurant`
  - `Id`, `Name`, `Description`, `Category`, `HasDelivery`, `ContactEmail`, `ContactNumber`
  - `Address` (owned type: `City`, `Street`, `PostalCode`)
  - `Dished` (collection of `Dish`) – name kept to match existing DB/migrations
- `Dish`
  - `Id`, `Name`, `Description`, `Price`, `RestaurantId`

## Application Contracts and DTOs

- `IRestaurantService`
  - `CreateAsync(CreateRestaurantRequest)` → returns new restaurant Id
  - `GetByIdAsync(id)` → `RestaurantDto?`
  - `GetAllAsync()` → list of `RestaurantDto`
  - `UpdateAsync(id, UpdateRestaurantRequest)` → bool
  - `DeleteAsync(id)` → bool
- `RestaurantDto` includes `AddressDto` and `List<DishDto>`

## Infrastructure Implementation

- `RestaurantsDbContext`
  - Tables: `Restaurants`, `Dishes`
  - Configures `Restaurant.Address` as owned and `Restaurant.Dished` relationship
- `RestaurantService` (Infrastructure)
  - Implements `IRestaurantService` using `RestaurantsDbContext`
  - Maps entities ↔ DTOs
  - Includes dishes when reading (`Include(r => r.Dished)`)
- DI registration (Infrastructure):
  - DB context with `UseSqlServer(connectionString)`
  - `IRestaurantSeeder` and `IRestaurantService` registrations

## API Layer

- `RestaurantsController` (route: `api/restaurants`)
  - `GET /api/restaurants` → list all
  - `GET /api/restaurants/{id}` → get by id
  - `POST /api/restaurants` → create; returns `201 Created` with location
  - `PUT /api/restaurants/{id}` → update; returns `204 No Content`
  - `DELETE /api/restaurants/{id}` → delete; returns `204 No Content`
- `Program.cs`
  - `AddControllers()`
  - Swagger via `AddEndpointsApiExplorer()` and `AddSwaggerGen()`
  - Registers `AddApplication()` and `AddInfrastructure(configuration)`
  - Runs seeding on startup via `IRestaurantSeeder`

## Running the API

1. Configure connection string in `Restaurants.API/appsettings.json` (key: `ConnectionStrings:RestaurantsDb`).
2. Apply migrations (if needed):
   - `dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.API`
3. Build:
   - `dotnet build`
4. Run API:
   - `dotnet run --project Restaurants.API`
5. Swagger UI (Development environment):
   - Navigate to `/swagger` on the running API base URL

### Example Request Body (POST/PUT)

```json
{
  "name": "My Cafe",
  "description": "Great coffee",
  "category": "Cafe",
  "hasDelivery": true,
  "contactEmail": "info@mycafe.com",
  "contactNumber": "123456789",
  "address": { "city": "Hanoi", "street": "1 Main", "postalCode": "10000" }
}
```

## Seeding

- On startup the API calls `IRestaurantSeeder.Seed()` which inserts initial restaurants (e.g., KFC, Pizza Hut) if the database is empty.

## Notes and Conventions

- The property `Restaurant.Dished` name is retained to match existing migrations and seeding. Changing this would require a new migration.
- Application project remains independent of Infrastructure; only contracts and DTOs live there.
- Mapping is explicit and performed in Infrastructure to keep controllers thin.

## Extending Further

- Validation: add FluentValidation in Application and register validators.
- Paging/filtering: extend `IRestaurantService.GetAllAsync` to accept query params.
- Authentication/Authorization: add policies in API and propagate user context to services.
- Testing: add unit tests for mapping and service logic; integration tests for controllers.
