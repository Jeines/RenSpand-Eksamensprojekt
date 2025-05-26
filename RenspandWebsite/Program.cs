using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.MockData;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.AboutServices;
using RenspandWebsite.Service.AboutUsService;
using RenspandWebsite.Service.EmployeeServices;
using RenspandWebsite.Service.FaqServices;
using RenspandWebsite.Service.OrderServices;
using RenspandWebsite.Service.ProfileServices;
using RenspandWebsite.Service.WorkServices;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// add services profile
builder.Services.AddSingleton<ProfileService, ProfileService>();
builder.Services.AddTransient<DbService<Profile>, DbService<Profile>>(); // Assuming you have a DbService for Profile   
builder.Services.AddTransient<ProfileDbService, ProfileDbService>();
builder.Services.AddTransient<DbService<Profile>>();

// add services employee
builder.Services.AddTransient<DbService<Employee>, DbService<Employee>>(); // Assuming you have a DbService for Employee
builder.Services.AddSingleton<EmployeeService, EmployeeService>();
builder.Services.AddTransient<EmployeeDbService, EmployeeDbService>();



// add services aboutUs
builder.Services.AddSingleton<AboutUsService, AboutUsService>();
builder.Services.AddTransient<DbService<AboutUs>, DbService<AboutUs>>(); // Assuming you have a DbService for AboutUs
builder.Services.AddTransient<AboutUsDbServices, AboutUsDbServices>();

// add services FAQ
builder.Services.AddTransient<DbService<FAQ>, DbService<FAQ>>();
builder.Services.AddSingleton<FaqService, FaqService>();
builder.Services.AddTransient<FaqDbService, FaqDbService>();

// add services Work
builder.Services.AddTransient<WorkService, WorkService>();
builder.Services.AddTransient<WorkDbService, WorkDbService>();
builder.Services.AddTransient<DbService<Work>, DbService<Work>>(); // Assuming you have a DbService for Work

//builder.Services.AddTransient<DbService<Order>, DbService<Order>>();

// add services Order
builder.Services.AddSingleton<OrderService, OrderService>();
builder.Services.AddTransient<OrderDbService, OrderDbService>();
builder.Services.AddTransient<DbService<Order>, DbService<Order>>(); // Assuming you have a DbService for Order


// add services JsonFileService
builder.Services.AddSingleton<JsonFileService<Work>>();
builder.Services.AddTransient<JsonFileService<Profile>>();
builder.Services.AddTransient<JsonFileService<Employee>>();
builder.Services.AddTransient<JsonFileService<Order>>();

// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

builder.Services.AddSingleton<EmailServicecs, EmailServicecs>();
builder.Services.AddScoped<EmailServicecs>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions =>
{
    cookieOptions.LoginPath = "/Login/LogInPage";
});

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
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

//MockProfiles.GetMockProfiles();

app.Run();


