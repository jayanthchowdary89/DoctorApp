using DoctorApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DoctorApp.UserServices;
using DoctorApp.Repository;

namespace DoctorApp
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
            builder.Services.AddDbContext<DoctorAppDbContext>(item  => item.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            builder.Services.AddTransient<IUsersLogic, UsersLogic>();
            builder.Services.AddTransient<IDocRepo,DocRepo>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthentication();

            app.UseAuthorization();


            app.UseStaticFiles();
            app.MapControllers();
            app.UseCors("corsapp");

            app.Run();
        }
    }
}