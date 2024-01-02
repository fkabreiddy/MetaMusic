using BlazorAnimation;
using MetaMusic.Data;
using MetaMusic.Data.Context;
using MetaMusic.Data.ThemeManagement;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.Configure<AnimationOptions>(Guid.NewGuid().ToString(), c => { });
builder.Services.AddScoped<ITheme, Theme>();

builder.Services.AddDbContext<MetaMusicDbContext>();
builder.Services.AddScoped<IMetaMusicDbContext, MetaMusicDbContext>();
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
