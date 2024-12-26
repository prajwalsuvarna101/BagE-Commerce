using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Services;
using Microsoft.AspNetCore.Builder;
using Bag_E_Commerce.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the DbContext with the connection string from appsettings.json
builder.Services.AddDbContext<BagDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controllers

var serviceInterfaceType = typeof(ICategoryService);  // Adjust this to the base type of your services
var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(serviceInterfaceType));

foreach (var serviceType in serviceTypes)
{
    var interfaceType = serviceType.GetInterfaces().First();
    builder.Services.AddScoped(interfaceType, serviceType);
}


builder.Services.AddControllers();

// Register  Services
builder.Services.AddScoped<AuthService>();  
builder.Services.AddScoped<IBagService, BagService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IShoppingCartService,ShoppingCartService>();


// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authentication and configure JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourapp.com",
            ValidAudience = "yourapp.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("welcometoegdkmanglorewelcometoegdkmanglorewelcometoegdkmanglore"))
        };
    });

// Add Authorization policies (this must be before builder.Build())
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User", "Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller actions to the request pipeline
app.MapControllers();

// Run the application
app.Run();
