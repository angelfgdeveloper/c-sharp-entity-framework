# Entity Framework
- dotnet run => ejecuta el código
- dotnet build => Verifica errores

- Crear proyecto en carpeta elegida vsc ```dotnet new web```

## Bases y conceptos
### Instalaciones de NuGet
```https://www.nuget.org/```

Ejecuciones en el .NET_CLI => Comprobar versión

1. Microsoft.EntityFrameworkCore 
```dotnet add package Microsoft.EntityFrameworkCore --version 6.0.9```
2. Microsoft.EntityFrameworkCore.InMemory 
```dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0.9```
3. Microsoft.EntityFrameworkCore.SqlServer
``` dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.9```

Uso de Fluent API

## Migraciones
### Instalaciones de NuGet
1. Microsoft.EntityFrameworkCore.Design

Uso inicial de comandos
- dotnet ef migrations add InitialCreate
- dotnet ef migrations add ColumnPesoCategoria
- dotnet ef migrations add InitialData
- dotnet ef database update