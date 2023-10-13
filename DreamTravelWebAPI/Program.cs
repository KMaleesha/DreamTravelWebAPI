// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: BookingsController
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Controller for managing bookings in the Dream Travel Web API
// --------------------------------------------------------------

using DreamTravelWebAPI;
using DreamTravelWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var settings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
builder.Services.AddSingleton(settings);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITravelerService, TravelerService>();
builder.Services.AddSingleton<IBookingService, BookingService>();

// Configure services
builder.Services.AddControllers();
builder.Services.AddTransient<ITrainService, TrainService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication Setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(); // Use the CORS middleware here

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();
