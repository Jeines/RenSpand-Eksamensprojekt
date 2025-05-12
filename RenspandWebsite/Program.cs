using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages.Shared;
using RenspandWebsite.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ProfileService, ProfileService>();
builder.Services.AddSingleton<IWorkService, WorkService>();
builder.Services.AddTransient<JsonFileService<Work>>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<JsonFileService<Employee>>();

//TODO: working code if other code break
// builder.Services.AddTransient(typeof(JsonFileService<>));
builder.Services.AddTransient<JsonFileService<Profile>>();

builder.Services.AddSingleton<CleaningService, CleaningService>();
builder.Services.AddSingleton<JsonFileService<Order>>();
builder.Services.AddSingleton<JsonFileService<Work>>();
builder.Services.AddScoped<CleaningService>();
builder.Services.AddSingleton<OrderService, OrderService>();
builder.Services.AddTransient<JsonFileService<Order>>();
builder.Services.AddTransient<OrderServices>();
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
