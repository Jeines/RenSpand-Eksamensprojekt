using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Pages.Shared;
using RenspandWebsite.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ProfileService, ProfileService>();
builder.Services.AddTransient<DbService<Profile>>();
builder.Services.AddTransient<ProfileDbService, ProfileDbService>();

builder.Services.AddTransient<DbService<User>>();
builder.Services.AddTransient<DbService<Order>>();
builder.Services.AddTransient<DbService<Address>>();
builder.Services.AddTransient<DbService<AddressItem>>();
builder.Services.AddTransient<DbService<Work>>();
builder.Services.AddTransient<DbService<ServiceItem>>();

builder.Services.AddSingleton<IWorkService, WorkService>();
builder.Services.AddTransient<JsonFileService<Work>>();

//TODO: working code if other code break
// builder.Services.AddTransient(typeof(JsonFileService<>));
builder.Services.AddTransient<JsonFileService<Profile>>();
//adds dbservice to program.cs
builder.Services.AddDbContext<RenSpandDbContext>();

builder.Services.AddSingleton<CleaningService, CleaningService>();
builder.Services.AddSingleton<JsonFileService<Order>>();
builder.Services.AddSingleton<JsonFileService<Work>>();
builder.Services.AddScoped<CleaningService>();
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
