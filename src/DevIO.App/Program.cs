using DevIO.App.Configurations;
using DevIO.App.Data;
using DevIO.App.Extensions;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddUserSecrets<Program>();
    //para quando o ambiente for produção, trabalhe com User Secrets
    //ou seja, pegue a configuração de ambiente de produção de outro arquivo
}
else if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddDbContext<MeuDbContext>(options =>
    options.UseSqlServer(connectionString));


//Ao adicionar o AutoMapper no Servides do builder, ele vai procurar
//a classe de mapeamento (qualquer classe) que herdar de Profile
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ResolveDependencies();

builder.Services.AddMvcConfigutation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalizationConfig();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
