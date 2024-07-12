using Dapper.Project.Core.Entity;
using Dapper.Project.Data;
using Dapper.Project.Data.DapperRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Users>, UserRepository>();
builder.Services.AddScoped<IRepository<Products>,ProductRepository>();
builder.Services.AddScoped<IRepository<Comments>, CommentRepository>();
builder.Services.AddScoped<IRepository<Suppliers>, SupplierRepository>();
builder.Services.AddScoped<IRepository<Sales_Titles>, SalesTitlesRepository>();
builder.Services.AddScoped<IRepository<Sales_Details>, SalesDetailsRepository>();
builder.Services.AddScoped<IRepository<Finance>, FinanceRepository>();
builder.Services.AddScoped<IRepository<Cargo>,CargoRepository>();
var app = builder.Build();

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

app.Run();