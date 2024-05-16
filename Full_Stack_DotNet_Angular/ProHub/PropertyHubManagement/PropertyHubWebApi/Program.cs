using Microsoft.EntityFrameworkCore;
using DATA_ACCESS_LAYER.Data_Models;
using BUSINESS_LOGIC_LAYER.Services;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.Repositories;
using PropertyHubWebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using PropertyHubWebApi.Controllers;
using PropertyHubWebApi.MappingProfiles;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
 
 
 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// enabling CORS
builder.Services.AddCors(option =>

{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});



// Registering  your all Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();

// Registering Your All Services
  builder.Services.AddScoped<IUserService, UserService>();
  builder.Services.AddScoped<IPropertyService, PropertyService>();
  builder.Services.AddScoped<IBookingService, BookingService>();
  builder.Services.AddScoped<IFeedbackService, FeedbackService>();
  builder.Services.AddScoped<IWishlistService, WishlistService>();

// Now, Confugering your Automapping Profiles classes
builder.Services.AddAutoMapper(typeof(Program));

//Confuguirng Db Context
builder.Services.AddDbContext<PropertyHubDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PropertyHubWebApiConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
// enabling CORS
//app.UseCors(options =>
//{
//    options.AllowAnyHeader();
//    options.AllowAnyOrigin();
//    options.AllowAnyMethod();
//});
app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
