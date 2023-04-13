using System.Configuration;
using System.Diagnostics;
using Clean.Architecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Clean.Architecture.Web.Controllers;

/// <summary>
/// A sample MVC controller that uses views.
/// Razor Pages provides a better way to manage view-based content, since the behavior, viewmodel, and view are all in one place,
/// rather than spread between 3 different folders in your Web project. Look in /Pages to see examples.
/// See: https://ardalis.com/aspnet-core-razor-pages-%E2%80%93-worth-checking-out/
/// </summary>
public class HomeController : Controller
{
  protected readonly IOptions<AppSettingsModel> options;
  public static string? ServiceUrl { get; set; }

  public HomeController(IOptions<AppSettingsModel> appSettings)
  {
    options = appSettings;
    ServiceUrl = options.Value.MainServiceUrl;
  }
  
  public IActionResult Index()
  {
    ViewData["MainServiceUrl"] = ServiceUrl;
    ViewBag.MainServiceUrl = ServiceUrl;
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
