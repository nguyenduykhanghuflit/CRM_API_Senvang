
using Microsoft.IdentityModel.Tokens;
using Quartz.Util;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Hosting;
using CRM_Api_Senvang.Database;
using CRM_Api_Senvang.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectionString = builder.Configuration.GetConnectionString("DB_TTS");


//DI
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddAuthorization();

builder.Services.AddSignalR();



var app = builder.Build();





app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


app.Run();
