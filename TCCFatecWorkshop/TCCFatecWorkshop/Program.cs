using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TCCFatecWorkshop;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories;
using TCCFatecWorkshop.Repositories.Interfaces;
using TCCFatecWorkshop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/*builder.Services.AddTransient<SeedService>();*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<WorkshopProjectDBContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase"))
    );

//REPOSITORY ENTITIES
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();

//JWT

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userId");
        policy.RequireAssertion(context =>
        {
            var httpContext = context.Resource as HttpContext;
            var requestedUserId = httpContext.Request.RouteValues["userId"].ToString();
            var userIdClaim = context.User.FindFirst("userId")?.Value;
            return requestedUserId == userIdClaim;
        });
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

//CORS POLICY

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(setting =>
    {
        setting.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

/*
if (args.Length == 1 && args[0].ToLower() == "seeddata") SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using(var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedService>();
        service.SeedDataContext();
    }
}*/




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
