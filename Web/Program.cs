using Core.Services;
using Domain.Models;
using Infrasitructure.Data;
using Infrasitructure.IRepository;
using Infrasitructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient(); // new object for evry request
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>)); // new object for One request
builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UintOfWork)); // new object for One request
builder.Services.AddScoped(typeof(IPostRepo),typeof(PostRepo)); // new object for One request
//builder.Services.AddSingleton<PostService>(); // one object for all request
builder.Services.AddScoped<PostService>(); 
builder.Services.AddScoped<CategoryService>(); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con"))
);
// add identity

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
