using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages.Shared;
using RenspandWebsite.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ProfileService, ProfileService>();
builder.Services.AddTransient<ProfileDbService, ProfileDbService>();
builder.Services.AddTransient<DbService<Profile>, DbService<Profile>>(); // Assuming you have a DbService for Profile   
builder.Services.AddSingleton<IWorkService, WorkService>();
builder.Services.AddTransient<JsonFileService<Work>>();

//TODO: working code if other code break
// builder.Services.AddTransient(typeof(JsonFileService<>));
builder.Services.AddTransient<JsonFileService<Profile>>();

builder.Services.AddSingleton<CleaningService, CleaningService>();
builder.Services.AddSingleton<JsonFileService<Order>>();
builder.Services.AddSingleton<JsonFileService<Work>>();
builder.Services.AddScoped<CleaningService>();
builder.Services.AddTransient<OrderSystemDbService, OrderSystemDbService>();
builder.Services.AddTransient<DbService<Order>, DbService<Order>>();

builder.Services.AddSingleton<OrderService, OrderService>();
builder.Services.AddTransient<JsonFileService<Order>>();
builder.Services.AddTransient<OrderServices>();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
