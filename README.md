Person Catalog
Esta es una aplicación web de ejemplo que utiliza la arquitectura limpia (Clean Architecture) y se conecta a una API para obtener información de personas.

Requisitos:

Visual Studio 2022
Componentes de Syncfusion (paquete NuGet)

Uso:

- En este repositorio se muestra una arquitectura limpia en ASP.NET core con un CRUD de Personas y con componentes SyncFusion
- La direccion del repositorio original: https://github.com/ardalis/cleanarchitecture
- Clonar este repositorio: git clone https://github.com/edreton/CleanArchitecture.git
- Compilar la solución en Visual Studio 2022 para descargar los paquetes NuGet necesarios.
- Modificar la cadena de conexión si es necesario en el archivo appsettings.json dentro del proyecto web de la solución.
- La primera ejecución creará la base de datos y agregará algunos registros iniciales.
- La aplicación está configurada para usar SQL Server, pero se puede modificar fácilmente para usar SQLite cambiando los siguientes archivos:
	En el archivo Startup.cs, descomentar la línea //options.UseSqlite(connectionString) y comentar la línea posterior.
	En el archivo Program.cs del proyecto Clean.Architecture.Web, cambiar la línea string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); por SqliteConnection.
- Dentro de appSettings.json en el proyecto web, también puede configurar la URL MainServiceUrl del servicio de la API.
- Puede navegar adicionalmente por la ayuda de la API para más información acerca de los endpoints disponibles.

Repositorio:
Este repositorio de GitHub se realiza como test tecnico y se encuentra diposnible en: https://github.com/edreton/CleanArchitecture