using ToDo.Web.Service;
using ToDo.Web.Service.IService;
using ToDo.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// configure HttpContextAccessor 
builder.Services.AddHttpContextAccessor();
// configure HttpClient
builder.Services.AddHttpClient();
// configure HttpClient for AuthService
builder.Services.AddHttpClient<IAuthService, AuthService>();
// configure AuthAPIBase
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

// add service lifetime for services
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
