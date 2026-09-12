using api;
using api.Database; // for database context
using api.Interfaces;
using api.Models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//this LoopHandling.Ignore is to avoid the circular reference error when serializing the objects to JSON.
//Basically, when you have two objects that reference each other
//it can create an infinite loop when trying to serialize them to JSON.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Add the ApplicationDBContext to the services collection, so that it can be injected into the controllers.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



//User Password Restrictions
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 10;

    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ApplicationDBContext>();

//schemes
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});


// Add the controllers to the services collection
builder.Services.AddControllers();

// Add the IUserRepository interface and its implementation to the services collection, so that it can be injected into the controllers.
builder.Services.AddScoped<IUserRepository, UserRepo>();
// Add the ICommentRepository interface and its implementation to the services collection, so that it can be injected into the controllers.
builder.Services.AddScoped<ICommentRepository, CommentRepo>();
// Add the IPostRepository interface and its implementation to the services collection, so that it can be injected into the controllers.
builder.Services.AddScoped<IPostRepository, PostRepo>();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//authenticate
app.UseAuthentication();
app.UseAuthorization();

// Map the controllers to the endpoints
app.MapControllers();

app.Run();

