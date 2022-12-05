﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using currus.Data;
using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;
using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using currus.Middleware;

namespace currus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            Logger.createLogFile();

            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(p =>
                p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

            services.AddControllers().AddNewtonsoftJson(x =>
             x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
                );

            //services.AddScoped<ITripDbRepository, TripDbRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfig:Secret").Value);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // for dev
                    ValidateAudience = false, // for dev
                    RequireExpirationTime = false, // for dev -- needs to be update when refresh token is added
                    ValidateLifetime = true
                };
            });

            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => new ExceptionLogger());

            builder.RegisterType<TripDbRepository>().As<ITripDbRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(ExceptionLogger))
                .InstancePerDependency();

            builder.RegisterType<ExceptionLogger>().SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }

            app.UseCors("corsapp");
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/apisecure"), builder =>
            {
                builder.UseJwtAuthMiddleware();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddScoped<ITripDbRepository, TripDbRepository>();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    );

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // for dev
        ValidateAudience = false, // for dev
        RequireExpirationTime = false, // for dev -- needs to be update when refresh token is added
        ValidateLifetime = true
    };
});

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Create a LOG folder with file
Logger.createLogFile();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseCors("corsapp");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/apisecure"), builder =>
{
    builder.UseJwtAuthMiddleware();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();*/