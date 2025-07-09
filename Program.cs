using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(30);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        
        options.LoginPath = "/Home/Login";

       
        options.AccessDeniedPath = "/Home/Error";

        
        options.LogoutPath = "/Home/Logout";

       
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

       
        options.SlidingExpiration = true;
    });


var app = builder.Build();


    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(

    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();