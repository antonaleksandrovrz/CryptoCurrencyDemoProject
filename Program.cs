using CryptoCurrencyDemoProject.Data;
using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.Configure<CurrenciesDatabaseSettings>(builder.Configuration.GetSection(nameof(CurrenciesDatabaseSettings)));
builder.Services.AddSingleton<ICurrenciesDatabaseSettings>(cd => cd.GetRequiredService<IOptions<CurrenciesDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("CurrenciesDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<ICurrenciesService, CurrenciesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
