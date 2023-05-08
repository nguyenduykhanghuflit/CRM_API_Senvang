
using Microsoft.IdentityModel.Tokens;
using Quartz.Util;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Hosting;
using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using CRM_Api_Senvang.Repositories.Login;
using CRM_Api_Senvang.Repositories.Quotes;
using CRM_Api_Senvang.Repositories.Statuses;
using CRM_Api_Senvang.Repositories.Org;
using Microsoft.Extensions.Options;

using Microsoft.OpenApi.Models;
using System.Reflection;
using CRM_Api_Senvang.Middleware;
using CRM_Api_Senvang.Repositories.Customer;
using CRM_Api_Senvang.Repositories.Deal;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectionString = builder.Configuration.GetConnectionString("DB_TTS");


//DI
builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddSignalRCore();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddSingleton<DatabaseManager>();

builder.Services.AddSingleton<SqlHelper>();

builder.Services.AddSingleton<TokenHelper>();
builder.Services.AddSingleton<Utils>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IQuotesRepository, QuotesRepository>();
builder.Services.AddScoped<IStatusesRepository, StatusesRepository>();
builder.Services.AddScoped<IOrgRepository, OrgRepository>();
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {

        //tự cấp token
        ValidateIssuer = false,
        ValidateAudience = false,

        //ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
});

builder.Services.AddAuthorization();

builder.Services.AddSignalR();



var app = builder.Build();




app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
