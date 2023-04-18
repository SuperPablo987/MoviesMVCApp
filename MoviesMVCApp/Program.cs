using Microsoft.EntityFrameworkCore;
using MoviesData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// get the connection string from appsettings.json
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MoviesContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
