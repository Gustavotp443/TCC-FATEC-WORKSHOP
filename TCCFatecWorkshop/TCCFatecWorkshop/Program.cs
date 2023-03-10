using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Repositories;
using TCCFatecWorkshop.Repositories.Interfaces;

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


builder.Services.AddScoped<IUserRepository, UserRepository>();

//JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                /*ValidateIssuer = true,
                ValidateAudience = true,*/
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                /*ValidIssuer = "mydomain.com",
                ValidAudience = "mydomain.com",*/
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sadmifnrn01043nr9fn2fbfn9s1-asdçç"))
            };
        });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
