
using Microsoft.EntityFrameworkCore;
using System;
using UnicomTIC_LMS_BE.DataBase;
using UnicomTIC_LMS_BE.Entities.Email;
using UnicomTIC_LMS_BE.IRepositories;
using UnicomTIC_LMS_BE.IServices;
using UnicomTIC_LMS_BE.Repositories;
using UnicomTIC_LMS_BE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace UnicomTIC_LMS_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register DbContext before building the application
            builder.Services.AddDbContext<LMSDbContext>(opt =>
                 opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register EmailConfig
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

            // Register Email services
            builder.Services.AddScoped<SendMailService>();
            builder.Services.AddScoped<SendMailRepository>();
            builder.Services.AddScoped<EmailServiceProvider>();


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<ITokenRepository, TokenRepository>();


            // JWT Authentication Configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
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


            // Ensure EmailConfig is available as a singleton if needed
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);


            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
