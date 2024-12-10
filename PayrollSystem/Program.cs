using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
using PayrollSystem.Data.Common;
using PayrollSystem.Filters;
using PayrollSystem.Injection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using ExceptionHandling;
using PayrollSystem.Core.HR;

var builder = WebApplication.CreateBuilder(args);

string AllowedSpecificOrigins = "CorsPolicy";

string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if(env==null)
{
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "local");
    builder.Configuration.AddJsonFile($"appsetting.json", true, true).AddEnvironmentVariables();
}
else
{
    builder.Configuration.AddJsonFile($"appsettings.json", true, true).AddJsonFile($"appsettings.{env}.json",true
        ,true).AddEnvironmentVariables();
}

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequiredRolePolicy", policy =>
//    {
//        policy.RequireClaim("Role", "2", "3", "4", "5");
//    });
//});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddOptions();
builder.Services.AddHttpContextAccessor();
DependencyInjection.Injectctor(builder.Services);

var sqlconnection = builder.Configuration.GetConnectionString("ConnectionLink");

builder.Services.AddDbContext<DbsContext>(options =>options.UseSqlServer(sqlconnection, b =>b.MigrationsAssembly("PayrollSystem")));
builder.Services.AddDbContext<DbsContext>(options => options.UseSqlServer(sqlconnection));


builder.Services.AddSingleton<DapperDbContext>(new DapperDbContext(sqlconnection));
builder.Services.AddMvc(options =>
{
    options.Filters.Add<AuthenticationFilter>();
}).AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ContractResolver = new DefaultContractResolver();
    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["AuthConfig:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p =>
{
    p.RequireHttpsMetadata = false;
    p.SaveToken = true;
    p.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthConfig:Issuser"],
        ValidAudience = builder.Configuration["AuthConfig:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(key),
        RoleClaimType="Role",
    };
});

string[] origins = builder.Configuration["ApiSettings:AllowedDomains"].Split(',');
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedSpecificOrigins, policy =>
    {
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiKeyAuthenticateFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowedSpecificOrigins);
app.UseMiddleware(typeof(ExcetionHandlingMiddleware));
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
