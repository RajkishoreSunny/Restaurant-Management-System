using Microsoft.EntityFrameworkCore;
using Repositories.TableRepo;
using Models.Category;
using Repositories.Category;
using Repositories.CustomerRepository;
using Repositories.MenuRepo;
using Services.Category;
using Services.CustomerService;
using Services.MenuService;
using Services.TableSvc;
using Repositories.OrderRepository;
using Services.OrderSvc;
using Repositories.PaymentRepository;
using Services.PaymentSvc;
using Repositories.PaymentRepo;
using Repositories.AboutRepo;
using Services.AboutSvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Repositories.ReservationRepository;
using Swashbuckle.AspNetCore.Swagger;
using Services.ReservationSvc;
using Repositories.ManagerRepository;
using Services.ManagerSvc;
using Microsoft.OpenApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme 
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
    });

#region CORS
builder.Services.AddCors(options => {
	options.AddPolicy(name: "AllowOrigin", builder => {
		builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
	});
});
#endregion

#region DBContext Region
builder.Services.AddDbContext<RMSDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);
#endregion

#region JWTToken

string jwtSecretKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
			};
		});

#endregion

#region Dependency Injection Region
builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddScoped<IMenuRepo, MenuRepository>();
builder.Services.AddScoped<MenuService>();

builder.Services.AddScoped<ICustomer, CustomerRepo>();
builder.Services.AddScoped<CustomerSvc>();

builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<TableService>();
builder.Services.AddHostedService<TableStatusUpdateService>();

builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<IPayment, PaymentRepository>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddScoped<IAbout, AboutRepository>();
builder.Services.AddScoped<AboutService>();

builder.Services.AddScoped<IReservationRepo, ReservationRepository>();
builder.Services.AddScoped<ReservationService>();

builder.Services.AddScoped<IManager, ManagerRepo>();
builder.Services.AddScoped<ManagerService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
