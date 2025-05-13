using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Pages.Shared;
using RenspandWebsite.Service;
using RenspandWebsite.Service.ProfileServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IWorkService, WorkService>();
builder.Services.AddTransient<JsonFileService<Work>>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<JsonFileService<Employee>>();


//TODO: working code if other code break
//builder.Services.AddTransient(typeof(JsonFileService<>));

builder.Services.AddTransient<JsonFileService<Profile>>();

builder.Services.AddSingleton<JsonFileService<Order>>();
builder.Services.AddSingleton<JsonFileService<Work>>();
//builder.Services.AddTransient<DbService<Order>, DbService<Order>>();


// Profile services (Scoped for DB-tilgang)
builder.Services.AddScoped<DbService<Profile>, DbService<Profile>>();
builder.Services.AddScoped<ProfileDbService>();
builder.Services.AddScoped<ProfileService>();

// Order services (samme her)
builder.Services.AddScoped<DbService<Order>, DbService<Order>>();
builder.Services.AddScoped<OrderDbService>();
builder.Services.AddScoped<OrderService>();


// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

builder.Services.AddTransient<JsonFileService<Order>>();
builder.Services.AddSingleton<EmailServicecs, EmailServicecs>();
builder.Services.AddScoped<EmailServicecs>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogInPage";
});

builder.Services.AddMvc().AddRazorPagesOptions(options => {
    options.Conventions.AuthorizeFolder("/Item");
}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

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

// **Ensure UseSession is placed before UseRouting**
app.UseSession(); // Add this line before UseRouting

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
