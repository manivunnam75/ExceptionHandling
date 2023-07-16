using ExceptionHandlingAssignment;
using ExceptionHandlingAssignment.Interfaces;
using ExceptionHandlingAssignment.Repository;
using ExceptionHandlingAssignment.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("con");
builder.Services.AddDbContext<ExceptionContext>(options =>
{ options.UseSqlServer(connection); });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFileProcessing , FileProcess>();
builder.Services.AddScoped<IExceptionRepo, ExceptionRepository>();

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
