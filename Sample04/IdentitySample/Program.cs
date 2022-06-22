using System.Configuration;
using System.Reflection;
using IdentitySample;
using IdentitySample.Entites;
using IdentitySample.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmailService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));


builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
    
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// 邮件服务
builder.Services.AddEmailService(builder.Configuration);

var app = builder.Build();

//app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// 认证
app.UseAuthentication();

// 授权
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
