using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FindMyPG.Core.Configs;
using FindMyPG.Core.Entities;
using FindMyPG.Data;
using FindMyPG.Infrastructure;
using FindMyPG.Service.States;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new DependencyRegistrarModule());
    });

// Add services to the container.
var config = builder.Configuration.GetSection("FindMyPGConfig").Get<FindMyPGConfig>();
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
    RequiredLength = 8,
    RequiredUniqueChars = 0,
    RequireLowercase = true,
    RequireNonAlphanumeric = true,
    RequireUppercase = true
})
.AddEntityFrameworkStores<PGDBContext>()
.AddDefaultTokenProviders();
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
