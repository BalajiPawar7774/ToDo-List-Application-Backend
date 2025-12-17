using Microsoft.EntityFrameworkCore;
using ToDoApplication.Dal;
using ToDoApplication.Helper;
using ToDoApplication.Model;
using ToDoApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =====================
// Add services to the container
// =====================

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
builder.Services.AddTransient<ICommonRepository<User>, CommonRepository<User>>();
builder.Services.AddTransient<ICommonRepository<Todo>, CommonRepository<Todo>>();
builder.Services.AddTransient<UserRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") // Angular URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =====================
// Configure the HTTP request pipeline
// =====================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
