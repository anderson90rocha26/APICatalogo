using APICatalogo.Context;
using APICatalogo.DTO.Mappings;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
       // builder.Services.AddControllers(options =>
       // {
        //    options.Filters.Add(typeof(ApiExceptionFilter));
       //})
        //.AddJsonOptions(options =>
       // {
        //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
       // }).AddNewtonsoftJson();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        builder.Services.AddScoped<ApiLoggingFilter>();

        builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


        builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
        {
            LogLevel = LogLevel.Information
        }));

        builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
           // app.ConfigureExceptionHandler();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void AddNewTonsoftJson()
    {
        throw new NotImplementedException();
   }
}