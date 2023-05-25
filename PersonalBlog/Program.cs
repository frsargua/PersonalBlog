using System;
using System.Net;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Data;
using PersonalBlog.Data.Services;
using PersonalBlog.Models;

var builder = WebApplication.CreateBuilder(args);

//var credential = GoogleCredential.FromFile("./firebase_credentials.json");

Environment.SetEnvironmentVariable(
         "GOOGLE_APPLICATION_CREDENTIALS", "./firebase_credentials.json");

var credential = await GoogleCredential.GetApplicationDefaultAsync();

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = credential
}));;

Console.WriteLine("Hello");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 


//Authentication and authorization
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Seed data

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();


app.Run();

