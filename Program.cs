using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Services;
using CryptoCurrencyDemoProject.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
//configuration
builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection(nameof(ExternalApiSettings)));
builder.Services.AddSingleton<IExternalApiSettings>(cd => cd.GetRequiredService<IOptions<ExternalApiSettings>>().Value);
//dependancies
builder.Services.AddTransient<IExternalApiService, ExternalApiService>();
builder.Services.AddTransient<HttpClient>(provider =>
{
    var httpClient = new HttpClient();
    return httpClient;
});

builder.Services.AddControllersWithViews();

//configuration
builder.Services.Configure<CurrenciesDatabaseSettings>(builder.Configuration.GetSection(nameof(CurrenciesDatabaseSettings)));
builder.Services.AddSingleton<ICurrenciesDatabaseSettings>(cd => cd.GetRequiredService<IOptions<CurrenciesDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("CurrenciesDatabaseSettings:ConnectionString")));
//dependancies
builder.Services.AddScoped<ICurrenciesService, CurrencyService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
