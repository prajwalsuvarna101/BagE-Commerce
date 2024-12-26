# Bag-E-Commerce
# .NET MVC Project

## Overview

This project is a web application built using the ASP.NET MVC framework. It uses Entity Framework Core for database interactions and supports migrations to manage database schema changes.

---

## Setup Instructions

### Install Dependencies
Ensure the required .NET SDK is installed on your system.

### Update Connection String
Update the `ConnectionStrings` section in the `appsettings.json` file with your database details:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
}
```

### Run Database Migrations
Apply migrations to create the database schema by running the following command:

```bash
dotnet ef database update
```


### 2. **Run the Application**

Use the following command to start the application:

```bash
dotnet run
```

### 3. **Entity Framework Core Migrations**

#### Adding a Migration


### Adding a Migration
When you make changes to your models and need to update the database schema, create a new migration with the following command:

```bash
dotnet ef migrations add <MigrationName>
```

#### Applying Migrations
To apply the latest migrations and update the database, use:

```bash
dotnet ef database update
```

### Checking Migration History
You can view the applied migrations by running:

```bash
dotnet ef migrations list
```

### 4. **Folder Structure**

```plaintext
├── Controllers      // Application controllers
├── Models           // Application models
├── Views            // Razor views
├── Migrations       // EF Core migration files
├── wwwroot          // Static files (CSS, JS, images)
├── appsettings.json // Configuration file
├── Program.cs       // Application entry point
└── Startup.cs       // Middleware and service configuration
```



