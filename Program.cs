using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Services;
using CryptoCurrencyDemoProject.Data.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

//externalApi
builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection(nameof(ExternalApiSettings)));
builder.Services.AddSingleton<IExternalApiSettings>(cd => cd.GetRequiredService<IOptions<ExternalApiSettings>>().Value);
builder.Services.AddTransient<IExternalApiService, ExternalApiService>();
builder.Services.AddTransient<HttpClient>(provider =>
{
    var httpClient = new HttpClient();
    return httpClient;
});

//authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
            ValidAudience = builder.Configuration["AuthSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]))
        };
    });

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));
builder.Services.AddSingleton<IAuthSettings>(cd => cd.GetRequiredService<IOptions<AuthSettings>>().Value);
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddControllersWithViews();

//Currencies
builder.Services.Configure<CurrenciesDatabaseSettings>(builder.Configuration.GetSection(nameof(CurrenciesDatabaseSettings)));
builder.Services.AddSingleton<ICurrenciesDatabaseSettings>(cd => cd.GetRequiredService<IOptions<CurrenciesDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("CurrenciesDatabaseSettings:ConnectionString")));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

//Needed for the tests
public partial class Program { }