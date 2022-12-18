using System.Configuration;
using System.Reflection;
using System.Text;
using AdventureWorks.Interface;
using AdventureWorks.Models;
using AdventureWorks.Repository;
using AdventureWorks.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdventureWorksLt2016Context>(options => options.UseSqlServer("Server =.\\SQLEXPRESS;Database=AdventureWorksLT2016;Trusted_Connection=true; TrustServerCertificate=True;"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddScoped<IproductRepository,EfProductRepository >();
builder.Services.AddScoped<IprudectService,productService >();
builder.Services.AddScoped<ICustomerRepository,EFCustomerRepository >();
builder.Services.AddScoped<ICustomerService,CustomerService >();
builder.Services.AddScoped<ISalesOrderRepository,SalesOrderRepository >();
builder.Services.AddScoped<ISalesOrderService,SalesOrderService >();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();