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

// Profile service intialization
builder.Services.AddScoped<DbService<Profile>>();
builder.Services.AddScoped<ProfileDbService>();
builder.Services.AddScoped<ProfileService>();

// Employee service initialization
builder.Services.AddScoped<DbService<Employee>>();
builder.Services.AddScoped<EmployeeDbService>();
builder.Services.AddScoped<EmployeeService>();

// About Us service initialization
builder.Services.AddScoped<DbService<AboutUs>>();
builder.Services.AddScoped<AboutUsDbServices>();
builder.Services.AddScoped<AboutUsService>();

// FAQ service initialization
builder.Services.AddScoped<DbService<FAQ>>();
builder.Services.AddScoped<FaqService>();
builder.Services.AddScoped<FaqService>();

// Work service initialization
builder.Services.AddScoped<DbService<Work>>();
builder.Services.AddScoped<WorkDbService>();
builder.Services.AddScoped<WorkService>();

// Order service initialization
builder.Services.AddScoped<DbService<Order>>();
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

builder.Services.AddSingleton<EmailServicecs, EmailServicecs>();




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


