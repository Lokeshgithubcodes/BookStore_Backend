using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Services for Repository and Business
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<IBookRespository, BookRespository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressbusiness, AddressBusiness>();
builder.Services.AddScoped<IOrderRespository, OrderRespository>();
builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
builder.Services.AddScoped<IWhishListRepository, WhishListRepository>();
builder.Services.AddScoped<IWhishListBusiness, WhishListBusiness>();
builder.Services.AddScoped<IReviewRespository, ReviewRespository>();
builder.Services.AddScoped<IReviewBusiness, ReviewBusiness>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartBusiness, CartBusiness>();

//For Genarate Token use this
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookStoreApplication",
        Version = "v1"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    }); ;
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
});


builder.Services.AddControllers();


builder.Services.AddDistributedMemoryCache();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

builder.Services.AddSwaggerGen();


//rabbit mq registration code

//builder.Services.AddMassTransit(x =>
//{
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
//    {
//        config.UseHealthCheck(provider);
//        config.Host(new Uri("rabbitmq://localhost"), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });

//    }));
//    builder.Services.AddMassTransitHostedService();

//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication(); //this is added for RabbitMq

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
