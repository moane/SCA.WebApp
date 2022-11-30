using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SCA.WebApp.Data;
using SCA.WebApp.Data.InicializeDb;
using SCA.WebApp.Services;
using SCA.WebApp.Services.Contracts;

//Classe de configura��o da aplica��o

var builder = WebApplication.CreateBuilder(args);


//Adicionando refer�ncia a URL das apis a serem consumidas pela aplica��o
builder.Services.AddHttpClient("ScaRegisterApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ScaRegisterApi"]);
});

//Integra��o com o banco de dados do Identity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Adicionando o Identity como provedor de autentica��o e autoriza��o
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

//Adicionando politicas e perfis de autoriza��o
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Engenharia",
       policy => policy.RequireRole("Engenharia"));

    options.AddPolicy("Engenharia",
      policy => policy.RequireRole("Engenharia"));
});

//Configurando URLs de redirecionamento padr�o
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/LogOut";

});

//Adicionando servi�os ao escopo da aplica��o
builder.Services.AddScoped<IAssetTypeService, AssetTypeService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

////builder.Services.ConfigureApplicationCookie(options =>
////{
////    options.AccessDeniedPath = "/Error";
////});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Adicionando tratamento de redirecionamento em caso de retorno de erros HTTP: 404, 500...
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("Home/AccessDenied");
    }
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Habilitando autentica��o e autoriza��o na aplica��o web
app.UseAuthentication();
app.UseAuthorization();

SeedDatabaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",    
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

//Adicionando m�todo para popular usu�rios de teste do Identity
void SeedDatabaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var initRolesUsers = serviceScope.ServiceProvider
                               .GetService<IDataBaseInitializer>();

        initRolesUsers.InitializeRoles();
        initRolesUsers.InitializeUsers();
    }
}