using FinalProject.Jobs;
using FinalProject.Services;
using Identity.DAL.Context;
using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);
var configure = builder.Configuration;

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<IdentityDB>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
#if DEBUG
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
#endif
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "MailSenderWebApp";
    options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromDays(1);

    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddSingleton<ISendMessageService,SendMessageService>();
builder.Services.AddSingleton<MessageGateway>();
builder.Services.AddSingleton(new MailGatewayOptions()
{
    Password = "****",
    SenderName = "****@mail.ru",
    SMTPServer = "smtp.mail.ru",
    Sender = "*****",
    Port = 465
});
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddSingleton<SendMessageJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(SendMessageJob),
    cronExpression: "0 42 10 ? * WED")); //every wenthday at 10:42

builder.Services.AddDbContext<IdentityDB>(options =>
options.UseSqlServer(configure.GetConnectionString("Identity")), ServiceLifetime.Singleton);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
