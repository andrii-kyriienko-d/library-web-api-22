using LibraryWebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using LibraryWebApi.Validators;
using LibraryWebApi.Middlewares;
using LibraryWebApi.Services;
using Microsoft.OpenApi.Models;
using LibraryWebApi.Services.Interfaces;
using LibraryWebApi.Repositories.Interfaces;
using AutoMapper;
using LibraryWebApi.MappingProfiles;
using LibraryWebApi.DatabaseContextFactory.Interfaces;
using LibraryWebApi.DatabaseContextFactory;
using LibraryWebApi.Resolvers.Interfaces;
using LibraryWebApi.Resolvers;
using System.Reflection;
using LibraryWebApi.Entities;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Services.TokenService;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LibraryWebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BooksValidator>());
           
        AddOptions(services);
        AddJwt(services);
        AddDbContexts(services);
        AddBaseServices(services);
        AddAuthorizationServices(services);
        AddRepositories(services);
        AddLogger(services);
        AddSwagger(services);
            
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddMaps(Assembly.GetAssembly(typeof(BookViewProfile)));
        });

        services.AddSingleton(mappingConfig.CreateMapper());

        services.AddMemoryCache();
        services.AddControllers();

    }

    private void AddDbContexts(IServiceCollection services)
    {
        services.AddScoped<IDbContext, MsSqlDbContext>();
        services.AddScoped<IDbContext, PostgreSqlDbContext>();
        services.AddScoped<IDbContextResolver, DbContextResolver>();
    }

    private void AddBaseServices(IServiceCollection services)
    {
        services.AddScoped<IBaseService<Books>, BooksService>();
        services.AddScoped<IBaseService<Booklets>, BookletsService>();
        services.AddScoped<IBaseService<Publishers>, PublishersService>();
        services.AddScoped<IBaseService<Readers>, ReadersService>();

        services.AddScoped<IBookExchange, BookExchangeService>();
        services.AddScoped<IBookFullInfo, BookFullInfoService>();
    }

    private void AddAuthorizationServices(IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();
    }

    private void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRepository<Books>, BookRepository>();
        services.AddScoped<IRepository<Booklets>, BookletRepository>();
        services.AddScoped<IRepository<Publishers>, PublisherRepository>();
        services.AddScoped<IRepository<Readers>, ReaderRepository>();
        services.AddScoped<IReaderBookRepository, ReaderToBookRepository>();
        services.AddScoped<IBookInfo, BookInfoRepository>();

    }

    private void AddLogger(IServiceCollection services)
    {
        var fileLoggerOptions = Configuration.GetSection("Logging:FileLoggerOptions").Get<FileLoggerOptions>();

        services.AddScoped<ILastLogGetter, LastLogGetterService>();

        services.AddLogging(loggingBuilder => {
            loggingBuilder.AddFile(Path.Combine(fileLoggerOptions.FolderPath, "log_{0:dd}_{0:MM}_{0:yyyy}_{0:HH}.log"), fileLoggerOpts => {
                fileLoggerOpts.FormatLogFileName = logfile => {
                    return String.Format(logfile, DateTime.UtcNow);
                };
                fileLoggerOpts.Append = bool.Parse(fileLoggerOptions.Append);
                fileLoggerOpts.MinLevel = Enum.Parse<LogLevel>(fileLoggerOptions.MinLevel);
            });
        });
    }

    private void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Library WebApi",
                Version = "v1"
            });
        });
    }

    private void AddJwt(IServiceCollection services)
    {
        var jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
        });
    }

    private void AddOptions(IServiceCollection services)
    {
        services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
        services.Configure<FileLoggerOptions>(Configuration.GetSection("Logging:FileLoggerOptions"));
        services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1/swagger.json", "Library WebApi");
        });

        app.UseMiddleware<BusinessExceptionMiddleware>();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}