using api;
using api.Database; // for database context
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add the ApplicationDBContext to the services collection, so that it can be injected into the controllers.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add the controllers to the services collection
builder.Services.AddControllers();

// Add the IUserRepository interface and its implementation to the services collection, so that it can be injected into the controllers.
builder.Services.AddScoped<IUserRepository, UserRepo>();
// Add the ICommentRepository interface and its implementation to the services collection, so that it can be injected into the controllers.
builder.Services.AddScoped<ICommentRepository, CommentRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map the controllers to the endpoints
app.MapControllers();

app.Run();

