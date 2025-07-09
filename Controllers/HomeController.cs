using System.Diagnostics;
using Aasaan_Admin_Form.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aasaan_Admin_Form.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Homepage()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }


    [Authorize]
    public IActionResult Index()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      ViewBag.Username = User.Identity?.Name;
      return View();
    }



    [HttpGet]
    public IActionResult EditUserDetails()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }




    [HttpGet]
    public IActionResult Userprofile()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }

    [HttpGet]
    public IActionResult AppGroups()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }

    [HttpGet]
    public IActionResult EditAppGroups()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }

    [HttpGet]
    public IActionResult CheckPassword()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      string apiurl = _configuration["ApiSetting:BaseUser"];
      ViewBag.ApiUrl = apiurl;
      if (User.Identity != null && User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Homepage", "Home");
      }
      return View(new LoginModel());
    }


    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string token, string username)
    {
      if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
      {
        _logger.LogWarning("ExternalLoginCallback was called with missing token or username.");

        TempData["ErrorMessage"] = "Login failed. The authentication token was not provided.";
        return RedirectToAction("Login");
      }

      var claims = new List<Claim>
      {
          new Claim(ClaimTypes.Name, username),
          new Claim("ApiToken", token)
      };

      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

      var authProperties = new AuthenticationProperties
      {

        IsPersistent = true,
        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
      };


      await HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(claimsIdentity),
          authProperties);

      _logger.LogInformation($"User '{username}' successfully signed in via external API callback.");

      return RedirectToAction("Homepage", "Home");
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      _logger.LogInformation("User logged out.");
      return RedirectToAction("Login", "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}