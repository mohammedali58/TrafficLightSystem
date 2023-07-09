using TrafficLight.Models;
using TrafficLight.Options;
using TrafficLight.Services;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false)
      .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSingleton<ITrafficLight, TrafficLight.Models.TrafficLight>();
builder.Services.AddSingleton<TrafficLight.Models.IConfigurationProvider, TrafficLight.Models.ConfigurationProvider>();
builder.Services.AddOptions<TrafficLightOptions>().Bind(config.GetSection("TrafficLightOptions"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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


app.UseCors(a =>
{
    a.AllowAnyHeader();
    a.AllowAnyMethod();
    a.AllowAnyOrigin();
    a.WithExposedHeaders();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.MapHub<NotificationHub>("/Notify");

app.Run();
