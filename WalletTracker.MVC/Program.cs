using Microsoft.EntityFrameworkCore;
using WalletTracker.Infrastructure.Persistence;
using WalletTracker.Infrastructure.Extensions;
using WalletTracker.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WalletTracker");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Infrastructure project - services configuration by extension method
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Add pending migrations
using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<WalletTrackerDbContext>();

var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

// Seed data
var incomeCategoriesDefaultSeeder = scope.ServiceProvider.GetRequiredService<IncomeCategoriesDefaultSeeder>();
await incomeCategoriesDefaultSeeder.Seed();

var expenseCategoriesDefaultSeeder = scope.ServiceProvider.GetRequiredService<ExpenseCategoriesDefaultSeeder>();
await expenseCategoriesDefaultSeeder.Seed();

var paymentMethodsDefaultSeeder = scope.ServiceProvider.GetRequiredService<PaymentMethodsDefaultSeeder>();
await paymentMethodsDefaultSeeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
