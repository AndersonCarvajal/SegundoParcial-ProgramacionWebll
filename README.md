# Productos API

API REST para gestionar productos con ASP.NET Core, Entity Framework Core, SQL Server y Swagger.

## Requisitos
- .NET 9 SDK
- SQL Server (preferiblemente LocalDB o una instancia accesible)

## Configuración
1. Ajusta la cadena de conexión en appsettings.json si tu SQL Server no usa la instancia local por defecto.
2. Ejecuta las migraciones:
   - dotnet ef migrations add InitialCreate
   - dotnet ef database update
3. Inicia la API:
   - dotnet run
4. Abre Swagger en: https://localhost:5001/swagger/index.html o http://localhost:5000/swagger/index.html

## Endpoints
- GET /api/productos
- GET /api/productos/{id}
- GET /api/productos/buscar?nombre=nombre_producto
- POST /api/productos
- PUT /api/productos/{id}
- DELETE /api/productos/{id}

## Evidencias de prueba

### GET /api/productos
- Respuesta esperada: 200 OK con una lista de productos.

### GET /api/productos/1
- Respuesta esperada: 200 OK con el producto solicitado.

### POST /api/productos
Ejemplo de body:
```json
{
  "nombre": "Laptop",
  "descripcion": "Laptop para desarrollo",
  "precio": 1200,
  "stock": 10
}
```

### PUT /api/productos/1
Ejemplo de body:
```json
{
  "id": 1,
  "nombre": "Laptop Gamer",
  "descripcion": "Laptop para desarrollo y gaming",
  "precio": 1450,
  "stock": 8
}
```

### DELETE /api/productos/1
- Respuesta esperada: 204 No Content.

### GET /api/productos/buscar?nombre=lap
- Respuesta esperada: 200 OK con coincidencias parciales.
