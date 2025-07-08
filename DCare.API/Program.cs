using Microsoft.EntityFrameworkCore;
using DCare.Data.Context;
using DCare.Repository.Implementations;
using DCare.Repository.Interfaces;
using DCare.Services.Implementations;
using DCare.Services.Interfaces;
using DCare.Services.MappingProfiles;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DCare.Data.Entites;
using DCare.Services.DTOs.Patient;
using Microsoft.AspNetCore.Identity;
using DCare.Services.Helper;
using DCare.Services.Configuration;
using Stripe;
using DCare.Services.BackgroundJobs;

namespace DCare.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
         

            builder.Services.AddControllers();
            //////////////DBCONTEXT////////////////////
            builder.Services.AddDbContext<DCareDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAutoMapper(typeof(PatientProfile));
            builder.Services.AddAutoMapper(typeof(AppointmentProfile));
            builder.Services.AddAutoMapper(typeof(DoctorProfile));
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
            builder.Services.AddAutoMapper(typeof(SpecialtyProfile));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();
            builder.Services.AddScoped<FileHelper>();
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddHostedService<ExpiredAppointmentCleaner>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DCare API", Version = "v1" });

                // ✅ إعداد التوثيق بـ JWT
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "أدخلي 'Bearer' فراغ ثم التوكن. مثال: Bearer abcdef12345"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            builder.Services.AddAutoMapper(typeof(PatientProfile));

            // ✅ Add Authentication Services (Step 8 - Part 1)
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer("Bearer", options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
         };
     });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
         


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.Environment.IsDevelopment();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            //app.UseCors("AllowFrontend");

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
