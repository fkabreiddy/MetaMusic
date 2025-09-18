using MetaMusic;
using BlazorAnimation;
using MetaMusic.Authentication;
using MetaMusic.Data;
using MetaMusic.Data.Context;
using MetaMusic.Data.OtherEntities;
using MetaMusic.Data.Services;
using MetaMusic.Data.ThemeManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ApexCharts;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddMudServices();
builder.Services.AddHttpClient("GoogleAuth", client => client.BaseAddress = new Uri("https://localhost:7277/"));
builder.Services.Configure<AnimationOptions>(Guid.NewGuid().ToString(), c => { });
builder.Services.AddScoped<ITheme, MetaMusic.Data.ThemeManagement.Theme>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();


builder.Services.AddDbContext<MetaMusicDbContext>();
builder.Services.AddScoped<IMetaMusicDbContext, MetaMusicDbContext>();

builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();

builder.Services.AddScoped<IAsignDataService, AsignDataService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddScoped<ICalificacionService, CalificacionService>();
builder.Services.AddScoped<IMessageBox, MessageBox>();

builder.Services.AddScoped<INotaService, NotaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>(); //can be placed among other "AddScoped" - above: var app = builder.Build();   
builder.Services.AddScoped<INotificacionService, NotificacionService>();

builder.Services.AddScoped<IPeticionService, PeticionService>();
builder.Services.AddScoped<IBusquedaService, BusquedaService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

builder.Services.AddMudServices(config =>
{


    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
    
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
   


});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    
    opts =>
    {

        opts.LogoutPath = "/";
        opts.AccessDeniedPath = "/login-error";
       
        
    }
    );
builder.Services.AddAuthentication().AddGoogle(option =>
{

    var clientid = builder.Configuration["Google:ClientId"];
    option.ClientId = builder.Configuration["Google:ClientId"] ?? "";
    option.ClientSecret = builder.Configuration["Google:ClientSecret"] ?? "";
    option.ClaimActions.MapJsonKey("urn:google:profile", "link");
    option.Scope.Add("profile");
    option.CallbackPath = "/signin";
   




    option.Events.OnCreatingTicket = (context) =>
    {
        var picture = context.User.GetProperty("picture").GetString() ?? "";

        context.Identity!.AddClaim(new Claim("picture", picture ));

        return Task.CompletedTask;
    };

    
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.WebHost.UseStaticWebAssets();
builder.WebHost.UseUrls("https://localhost:7277/");


//lista de blogs en el cache


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
await SeedDatabase(); //can be placed above app.UseStaticFiles();
app.UseStaticFiles();
app.MapControllers();
app.UseCookiePolicy();


app.UseAuthentication();
app.UseAuthorization();


app.UseRouting();

app.UseAntiforgery(); 

app.UseStatusCodePagesWithRedirects("/404");
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();


async Task SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.Initialize();
    }
}


