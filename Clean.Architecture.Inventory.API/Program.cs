using Clean.Architecture.Inventory.Application.Behaviors;
using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Handlers;
using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Application.Queries;
using Clean.Architecture.Inventory.Domain.Entities;
using Clean.Architecture.Inventory.Persistence;
using Clean.Architecture.Inventory.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Configuração de Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Opcional
    .CreateLogger();

builder.Host.UseSerilog();

// Configurar DbContext com MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InventoryControlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registrar Repositórios e Serviços
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepositoy>();

// Registrar MediatR e Handlers com ServiceFactory
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Registrar Behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorLoggingBehavior<,>));

builder.Services.AddTransient<IRequestHandler<CreateProductCommand, int>, CreateProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteProductCommand, Unit>, DeleteProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateProductCommand, Unit>, UpdateProductCommandHandler>();
builder.Services.AddTransient<IRequestHandler<PathProductCommand, Unit>, PathProductCommandHandler>();

builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByTextQuery, IEnumerable<Product>>, GetProductByTextQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllProductPageQuery, PagedResult<Product>>, GetAllProductsPageQueryHandler>();


builder.Services.AddTransient<IRequestHandler<GetAllCategoryProductsQuery, IEnumerable<CategoryProduct>>, GetAllCategoryProductsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryProductByIdQuery, CategoryProduct>, GetCategoryProductByIdQueryHandler>();
// Adicionar suporte a validação com FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());


// Adicionar suporte à autenticação JWT
var key = builder.Configuration["Jwt:Key"]; // Chave secreta do JWT
var issuer = builder.Configuration["Jwt:Issuer"]; // Emissor do JWT
var audience = builder.Configuration["Jwt:Audience"]; // Audiência do JWT

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = false,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
	};
});

// Adicionar Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

	// Definição de segurança JWT
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT"
	});

	// Requisito de segurança JWT
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});


var app = builder.Build();

// Middleware de Logging de Requisições
app.UseSerilogRequestLogging();

// Verificar e aplicar migrações automaticamente após o app ser construído
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<InventoryControlDbContext>();
	dbContext.Database.Migrate(); // Aplica as migrações pendentes, se houver
}
// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
		options.DisplayRequestDuration();
	}); ;

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Iniciando a aplicação");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação falhou ao iniciar");
}
finally
{
    Log.CloseAndFlush();
}