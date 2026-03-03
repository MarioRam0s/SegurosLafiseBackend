# SegurosLafiseBackend

La API de SegurosLafiseBackend es un sistema backend diseñado para gestionar toda la información relacionada con clientes, vehículos y pólizas de seguros. Su objetivo es centralizar y automatizar los procesos de gestión de seguros, permitiendo realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) de manera eficiente y segura.

Clientes

La API permite administrar toda la información de los clientes, incluyendo datos personales, contacto y relaciones con pólizas y vehículos. Esto facilita la identificación rápida de clientes y la actualización de sus datos de manera centralizada, asegurando integridad y consistencia en el sistema.

Vehículos

Se puede gestionar el inventario de vehículos asociados a los clientes, incluyendo información de marca, modelo, año de fabricación y matrícula. Esto permite asociar vehículos con sus respectivos propietarios y con las pólizas correspondientes, asegurando un control preciso de la flota asegurada.

Pólizas

La gestión de pólizas abarca la creación, actualización y seguimiento de seguros activos o históricos. La API permite asignar clientes y vehículos a pólizas, definir coberturas, fechas de emisión y vencimiento, y calcular primas totales y sumas aseguradas. Esto garantiza un registro claro y confiable de todas las pólizas emitidas.

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
  ⚠️ Importante: si se tiene una versión inferior o superior a la 8, se debe actualizar a .NET 8.
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (Express)
- [Visual Studio 2022/2023](https://visualstudio.microsoft.com/) o VS Code con extensión C#

---

## Configuración del proyecto

1. **Clonar el repositorio**  
```bash
git clone https://github.com/MarioRam0s/SegurosLafiseBackend.git
cd SegurosLafiseBackend

2. Configurar la base de datos
Ejecutar el script SQL que se encuentra en Database/SegurosLafiseBD.sql

Configurar la cadena de conexión en appsettings.json:

 "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SegurosLafiseDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }

3. Instalar dependencias y restaurar paquetes

dotnet restore

4. Ejecutar la API

Desde Visual Studio:

Abrir el proyecto .sln

Presionar F5 o Ctrl+F5 para ejecutar

Notas importantes

⚠️ Antes de ejecutar la API, asegurarse de que SQL Server esté corriendo y que la referencia al servidor en la cadena de conexión sea correcta.

El proyecto requiere .NET 8, no se garantiza funcionamiento en versiones superiores o inferiores.

Se recomienda usar Postman para probar los endpoints

Autor

Mario Ramos – Desarrollador principal