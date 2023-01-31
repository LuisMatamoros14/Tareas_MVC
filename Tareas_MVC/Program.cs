using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Tareas_MVC;
using Microsoft.AspNetCore.Mvc.Razor;
using Tareas_MVC.Servicios;

var builder = WebApplication.CreateBuilder(args);
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (_, factoria) => factoria.Create(typeof(RecursoCompartido));
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name= DefaultConnection"));

builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
{
    options.ClientId = builder.Configuration["MicrosoftClientId"];
    options.ClientSecret = builder.Configuration["MicrosoftSecretId"];
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount= false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/usuarios/login";
    options.AccessDeniedPath= "/usuarios/login";
});

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Recursos";
});

var app = builder.Build();

app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es");
    options.SupportedUICultures = Constantes.CulturasUISoportadas.Select(cultura => new CultureInfo(cultura.Value)).ToList();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
