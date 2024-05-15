

using Abby.DataAccess.DbInitializer;
using DataAccess.Data;
using DataAccess.DbInitializer;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
SeedDatabase();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

void SeedDatabase()
{
    using (var scrope = app.Services.CreateScope())
    {
        var dbInitializer = scrope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
