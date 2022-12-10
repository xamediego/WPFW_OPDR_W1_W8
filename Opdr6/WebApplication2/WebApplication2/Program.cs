using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication2.Data;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = "https://localhost:8124",
        ValidAudience = "https://localhost:8124",
        
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"))
    };
});

builder.Services.AddSwaggerGen(c => 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var s = app.Services.CreateScope().ServiceProvider;

    var roleManager = s.GetRequiredService<RoleManager<IdentityRole<int>>>();
    string[] roleNames = { "Employee", "User" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }

    //Cant find a way to DI usermanager or so into context so tried to make a jpa create drop ala spring style
    var dbcontext = s.GetRequiredService<DatabaseContext>();
    var userManager = s.GetRequiredService<UserManager<ApplicationUser>>();
    
    dbcontext.Attracties.RemoveRange(dbcontext.Attracties);
    dbcontext.ApplicationUser.RemoveRange(dbcontext.ApplicationUser);

    for (int i = 1; i <= 10; i++)
    {
        var user = new ApplicationUser
        {
            Id = i,
            UserName = "User" + i,
            Email = "user" + i + "@live.com",
            PasswordHash = "User123!",
            Gender = Gender.Geheim,
        };
        
        var result = await userManager.CreateAsync(user, user.PasswordHash);
        if (result.Succeeded)
        {
            if (i == 1)
            {
                await userManager.AddToRoleAsync(user, "Employee");
            }
            else
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    
        dbcontext.Attracties.Add(new Attractie
        {
            Id = i,
            Name = "Attractie" + i,
            Engheid = i + 2,
            Bouwjaar = DateTime.Now
        });
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();