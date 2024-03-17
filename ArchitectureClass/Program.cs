using DataAccessLayer.BootcampDbContext;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PresentationLayer.Infrastucture.AutoMapper;
using System.Reflection;
using PresentationLayer.Infrastucture.Services;

//Builder
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<BootcampDbContext>();
builder.Services.AddDbContext<BootcampDbContext>(
    options => options.UseSqlServer("Data Source=LAPTOP-GTCNNS43\\SQL2019;Initial Catalog=TalentosDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
);

builder.Services.AddIdentityCore<User>(options =>
    {
        //options.Password.RequireDigit = false;
        //options.Password.RequireLowercase = false;
        //options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        //options.Password.RequiredLength = 6;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<IdentityRole<int>>()
    .AddRoleManager<RoleManager<IdentityRole<int>>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
    .AddEntityFrameworkStores<BootcampDbContext>();
    //.AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();

//Transient: A new instance is created every time the service is requested.
//builder.Services.AddTransient<IPetService, PetService>();
//Scoped: A new instance is created once per request, and is shared within a request, taking into account "THE SCOPED"
builder.Services.AddScoped<IPetService, PetService>();
//Singleton: A new instance is created the first time the service is requested, and the same instance is used for all subsequent requests.
//builder.Services.AddSingleton<IPetService, PetService>();

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).Assembly });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    setup =>
    {
        setup.SwaggerDoc("v1", new OpenApiInfo { Title = "ArchitectureClass", Version = "v1" });
        setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Input your bearer token in this format - Bearer {your token} to access this API"
        });
        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new string[] {}
            }
        });
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//App
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var context = services.GetRequiredService<BootcampDbContext>();
    //context.Database.Migrate();
    //if (context.Pets.Count() == 0)
    //{
    //    context.Add(new Pet { Name = "Rex", Type = "Dog", Birth = DateTime.Now, Description = "Pet 1", UserId = 1 });
    //    context.Add(new Pet { Name = "Mittens", Type = "Cat", Birth = DateTime.Now, Description = "Pet 2", UserId = 1 });
    //    context.SaveChanges();
    //}
    //Add Roles
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new IdentityRole<int>();
            role.Name = "Admin";
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync("Owner").Result)
        {
            var role = new IdentityRole<int>();
            role.Name = "Owner";
            roleManager.CreateAsync(role).Wait();
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod() //POST GET PUT
    //.WithOrigins("https://www.pets.com.ar")
    //.AllowAnyOrigin()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
