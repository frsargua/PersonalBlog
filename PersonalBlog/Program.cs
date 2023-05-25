using System;
using System.Net;
using System.Text.Json;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using PersonalBlog.Data;
using PersonalBlog.Data.Services;
using PersonalBlog.Models;

var builder = WebApplication.CreateBuilder(args);

//var credential = GoogleCredential.FromFile("./firebase_credentials.json");

//Environment.SetEnvironmentVariable(
//         "GOOGLE_APPLICATION_CREDENTIALS", "./firebase_credentials.json");

//var jsonss = @"{
//  ""type"": ""service_account"",
//  ""project_id"": ""personalblog-4f98f"",
//  ""private_key_id"": ""e7d3a935aa11f95b8b6aa42a7258838327436571"",
//  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCRJ9/AClWn62Vp\npS3y821Wv0jwEYMyE4/T+bE2Txq6j+p7En/JB+zjJEM8s9isX270VkZpBnxKY20g\n+xIAWEw7VQV1tHx3Tqpq47esu36y2RJUuPp5cTLikvcJS/J9qXrGbDJNmZe+IDpf\nLZAznonHMWCdDjxzPBBJX8dfEIVvcahz3ZNnywcoczshEr7zUEmn8UtFdIvLrLlm\nP+3BoEHORS89cny3O/frK7Y6623447Ze+ufrAl2l13GuhrBe4N+dPUorkoq7pJGQ\nrf74M+u90l7Lkh5lSwGq+1JVO8xDUwwNJB0z2ZboXt3bLVGH5Y9DSlN7pXbSyQPH\nIOJB1PCrAgMBAAECggEAQT+2zRj4Kvzd/9C/6GNzRVjD/SIqItL3RraJadvCLJWv\ntfX5WhEFc48j6EAByG43rn6vtNs0+K1b60Tg8SW/0rXjt+bTQkAqy9SkBnHbwKJi\nIpJqCTgQwxeEE3o/vSv3ZAVT0V0XAGB1TL5rAUElNtSj1MWG99fKjGkQ8lM3YAH+\ngKh55FJlfO558tW+ucYItQ+eM1rn8L9lN3+3hJ3ys9JJ5mLe9ccnA72uHkuCPqb9\ni8olNvQu4fJYbwAbbmuxe5zda2H42M0DTINTRroUlcUguG1GSLyIppJKuqJAqh99\nfcIGcmBllJosBxG34QqOMC4a8TQ1BWfORneM660RBQKBgQDDNIcemgL1Lk40Zm0u\n187kEG2RfhFPn3og49l08t0YVlwOpmSXW3APk4ZJThm8gUOCOzexyOLSyXyYUF7k\nONWWwU9Onc17GDID4RqMDRPOlpsyjqdKufMghexMJpbexu8Ryu+26201IuYISs2I\nPqhbpfruywX/JHPIq1lGIp1qbwKBgQC+XPdSvKJEOCFF9ndfCi8qHgx3ttLidg8Q\nJOON2yVPxEt/1Yp9GI2ZNo2qCFs14wogn8Uhw2iYcAZPe3HENcw7R0pO40MMr5WJ\nwf49qXdA4gvjdahzmjeSSHUdYocF7WSBtLPerqlOocNV/5/aXSO2UBW2i8s5OKxZ\nkY4vA5QrhQKBgQC1NX/xcnsoa7IBhsv4XjIbGPz9wRAE4ECZY6qsm/+O5ixTFTfw\nIdvnOcXBKxVNHpoyvRI4ogmeL5jQNFvCdNgiOJWMn4TurklPvJnORR2L+9damX7H\nKdN+75OqJXCMohOwarkZD3ezihAMxpQB1Fipq21EW4fXkFlDgX1AVEXZVQKBgC1U\nifiDvsgr7ZBxbl+NV0naOfHP8UvH/TJE9oLzKmFiRvA782xyilVvrjBNKRsd4219\nXj36AFA8bOREawTkIqwC/+jlKTQ+I4fYUChy0Fj8+wPBIsnUcWM+KdVDfw+kRgi0\n9RgT03FMB+3Un0YqY7SoQWNkypHnsvLpPEhpu8DZAoGACBpTMEZtxZ9ZMAt892YA\nbejHOF8i5s2YCs1iiAHSuIKpsO4z35nEtq6HjJQHOPH7WapKrqgwh6CIDcRqA9ND\noIG7L9Djbpk/Q6dIar6HGK9osmqExmIY/SSHykCxdhKLatRomXgF+xT76ts4BknN\nAMxR+NZeYPOEZn0leQ7O7gQ=\n-----END PRIVATE KEY-----\n"",
//  ""client_email"": ""firebase-adminsdk-gbwjs@personalblog-4f98f.iam.gserviceaccount.com"",
//  ""client_id"": ""103700813835427502257"",
//  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
//  ""token_uri"": ""https://oauth2.googleapis.com/token"",
//  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
//  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-gbwjs%40personalblog-4f98f.iam.gserviceaccount.com"",
//  ""universe_domain"": ""googleapis.com""
//}";

Environment.SetEnvironmentVariable(
         "GOOGLE_APPLICATION_CREDENTIALS", builder.Configuration.GetValue<string>("GOOGLE_APPLICATION_CREDENTIALS"));

var credential = await GoogleCredential.GetApplicationDefaultAsync();

//var credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("GOOGLE_APPLICATION_CREDENTIALS"));
//string GOOGLE_APPLICATION_CREDENTIALS = @"{
//  ""type"": ""service_account"",
//  ""project_id"": ""personalblog-4f98f"",
//  ""private_key_id"": ""e7d3a935aa11f95b8b6aa42a7258838327436571"",
//  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCRJ9/AClWn62Vp\npS3y821Wv0jwEYMyE4/T+bE2Txq6j+p7En/JB+zjJEM8s9isX270VkZpBnxKY20g\n+xIAWEw7VQV1tHx3Tqpq47esu36y2RJUuPp5cTLikvcJS/J9qXrGbDJNmZe+IDpf\nLZAznonHMWCdDjxzPBBJX8dfEIVvcahz3ZNnywcoczshEr7zUEmn8UtFdIvLrLlm\nP+3BoEHORS89cny3O/frK7Y6623447Ze+ufrAl2l13GuhrBe4N+dPUorkoq7pJGQ\nrf74M+u90l7Lkh5lSwGq+1JVO8xDUwwNJB0z2ZboXt3bLVGH5Y9DSlN7pXbSyQPH\nIOJB1PCrAgMBAAECggEAQT+2zRj4Kvzd/9C/6GNzRVjD/SIqItL3RraJadvCLJWv\ntfX5WhEFc48j6EAByG43rn6vtNs0+K1b60Tg8SW/0rXjt+bTQkAqy9SkBnHbwKJi\nIpJqCTgQwxeEE3o/vSv3ZAVT0V0XAGB1TL5rAUElNtSj1MWG99fKjGkQ8lM3YAH+\ngKh55FJlfO558tW+ucYItQ+eM1rn8L9lN3+3hJ3ys9JJ5mLe9ccnA72uHkuCPqb9\ni8olNvQu4fJYbwAbbmuxe5zda2H42M0DTINTRroUlcUguG1GSLyIppJKuqJAqh99\nfcIGcmBllJosBxG34QqOMC4a8TQ1BWfORneM660RBQKBgQDDNIcemgL1Lk40Zm0u\n187kEG2RfhFPn3og49l08t0YVlwOpmSXW3APk4ZJThm8gUOCOzexyOLSyXyYUF7k\nONWWwU9Onc17GDID4RqMDRPOlpsyjqdKufMghexMJpbexu8Ryu+26201IuYISs2I\nPqhbpfruywX/JHPIq1lGIp1qbwKBgQC+XPdSvKJEOCFF9ndfCi8qHgx3ttLidg8Q\nJOON2yVPxEt/1Yp9GI2ZNo2qCFs14wogn8Uhw2iYcAZPe3HENcw7R0pO40MMr5WJ\nwf49qXdA4gvjdahzmjeSSHUdYocF7WSBtLPerqlOocNV/5/aXSO2UBW2i8s5OKxZ\nkY4vA5QrhQKBgQC1NX/xcnsoa7IBhsv4XjIbGPz9wRAE4ECZY6qsm/+O5ixTFTfw\nIdvnOcXBKxVNHpoyvRI4ogmeL5jQNFvCdNgiOJWMn4TurklPvJnORR2L+9damX7H\nKdN+75OqJXCMohOwarkZD3ezihAMxpQB1Fipq21EW4fXkFlDgX1AVEXZVQKBgC1U\nifiDvsgr7ZBxbl+NV0naOfHP8UvH/TJE9oLzKmFiRvA782xyilVvrjBNKRsd4219\nXj36AFA8bOREawTkIqwC/+jlKTQ+I4fYUChy0Fj8+wPBIsnUcWM+KdVDfw+kRgi0\n9RgT03FMB+3Un0YqY7SoQWNkypHnsvLpPEhpu8DZAoGACBpTMEZtxZ9ZMAt892YA\nbejHOF8i5s2YCs1iiAHSuIKpsO4z35nEtq6HjJQHOPH7WapKrqgwh6CIDcRqA9ND\noIG7L9Djbpk/Q6dIar6HGK9osmqExmIY/SSHykCxdhKLatRomXgF+xT76ts4BknN\nAMxR+NZeYPOEZn0leQ7O7gQ=\n-----END PRIVATE KEY-----\n"",
//  ""client_email"": ""firebase-adminsdk-gbwjs@personalblog-4f98f.iam.gserviceaccount.com"",
//  ""client_id"": ""103700813835427502257"",
//  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
//  ""token_uri"": ""https://oauth2.googleapis.com/token"",
//  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
//  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-gbwjs%40personalblog-4f98f.iam.gserviceaccount.com"",
//  ""universe_domain"": ""googleapis.com""
//}";
//var credential = GoogleCredential.FromJson(GOOGLE_APPLICATION_CREDENTIALS);


builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = credential
})); 

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

