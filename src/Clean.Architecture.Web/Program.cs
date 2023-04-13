using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Clean.Architecture.Core;
using Clean.Architecture.Infrastructure;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.Web;
using FastEndpoints;
using FastEndpoints.Swagger.Swashbuckle;
using FastEndpoints.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Autofac.Core;
using System.Configuration;
using Clean.Architecture.Web.ViewModels;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  //Configuration.GetConnectionString("DefaultConnection");
string? apiMainServiceUrl = builder.Configuration.GetSection("ApiUrls").GetValue<string>("MainServiceUrl");

builder.Services.Configure<AppSettingsModel>(options => options.MainServiceUrl = apiMainServiceUrl);
builder.Services.AddDbContext(connectionString!);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();
builder.Services.AddFastEndpoints();
builder.Services.AddFastEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.OperationFilter<FastEndpointsOperationFilter>();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY5MzUxMkAzMjMxMmUzMTJlMzMzNVFiNkJ0cnZsSGdRSldyci90aHFtU292OTFtQjdRbjFmeFNaY2M3STV1Z289;Mgo+DSMBaFt+QHFqVkNrWE5Ff0BAXWFKblJ8RmdTfFdgBShNYlxTR3ZbQ19jSH1XdUxjWXld;Mgo+DSMBMAY9C3t2VFhhQlJBfVtdXGpWfFN0RnNYdV10flZPcC0sT3RfQF5jTX9RdkRgXXxXcHVcRQ==;Mgo+DSMBPh8sVXJ1S0d+X1RPckBDVHxLflF1VWJTfFp6d1xWESFaRnZdQV1nSHxTd0ZnWnZec3FW;MTY5MzUxNkAzMjMxMmUzMTJlMzMzNWNnNWxYVjEyQnVJZFpHVVZ0VUswNWZ6dlludmcwc012dlZTbzd3di9QMzA9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RGQmJAYVF2R2BJflRzcF9FaUwgOX1dQl9gSXpSdEViWntdeXRTQWM=;ORg4AjUWIQA/Gnt2VFhhQlJBfVtdXGpWfFN0RnNYdV10flZPcC0sT3RfQF5jTX9RdkRgXXxXcHNcRQ==;MTY5MzUxOUAzMjMxMmUzMTJlMzMzNVhGVUYyU1Z1aEJXQlBQdjl6MUk0Zk1jK3FSM0pTdzJPOE9qZTNzVEV2eW89;MTY5MzUyMEAzMjMxMmUzMTJlMzMzNVNxRXh5dHlqSUZtdlhVdWtjdkVGTm9TR3JZVVBRNXRhVUJTbml0dUZwVjA9;MTY5MzUyMUAzMjMxMmUzMTJlMzMzNW80V29QMDVJTHdxMEZoTFphTlN1ZEdVMExwUGFtOTJHa3NocXVzMkM0aG89;MTY5MzUyMkAzMjMxMmUzMTJlMzMzNWZ2cGlZeHh2QUpTbGZaWnJVRkxYVHhVcTJFWVcySEpuazh0QzNPMmNuYWs9;MTY5MzUyM0AzMjMxMmUzMTJlMzMzNVFiNkJ0cnZsSGdRSldyci90aHFtU292OTFtQjdRbjFmeFNaY2M3STV1Z289");

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseRouting();
app.UseFastEndpoints();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.MapDefaultControllerRoute();
app.MapRazorPages();

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<PersonDbContext>();
    //                    context.Database.Migrate();
    context.Database.EnsureCreated();
    //SeedData.Initialize(services);
    SeedPersonData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
