using Autofac;
using Autofac.Extensions.DependencyInjection;
using FindMyPG.Core.Configs;
using FindMyPG.Core.Entities;
using FindMyPG.Data;
using FindMyPG.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FindMyPG.Infrastructure.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new DependencyRegistrarModule());
    });

// Add services to the container.
var config = builder.Configuration.GetSection("FindMyPGConfig").Get<FindMyPGConfig>();
builder.Services.Configure<FindMyPGConfig>(builder.Configuration.GetSection("FindMyPGConfig"));
builder.Services.AddDbContext<PGDBContext>(options =>
options.UseLazyLoadingProxies().UseSqlServer(config.SqlConnectionString));

builder.Services.AddAutoMapper(typeof(Program));
var mvcBuilder = builder.Services.AddControllers();
mvcBuilder.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddIdentity<User, Role>(options => options.Password = new PasswordOptions()
{
    RequireDigit = false,
    RequiredLength = 0,
    RequiredUniqueChars = 0,
    RequireLowercase = false,
    RequireNonAlphanumeric = false,
    RequireUppercase = false
})
.AddEntityFrameworkStores<PGDBContext>()
.AddDefaultTokenProviders()
.AddPasswordValidator<PGPasswordValidator<User>>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwt =>
    {
        jwt.RequireHttpsMetadata = false;
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSigningKey)),
            ValidateIssuerSigningKey = true,
            ValidAudience = config.JwtValidAudience,
            ValidateAudience = true,
            ValidIssuer = config.JwtValidIssuer,
            ValidateIssuer = true,
            RequireSignedTokens = true,
            RequireExpirationTime = true
        };
        jwt.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
    };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.Run();
