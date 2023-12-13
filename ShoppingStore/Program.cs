using CoreLayer.Models;
using DataLayer.DBModels;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
 


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SQLServerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AspNetUser>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedPhoneNumber = true;
})
    .AddEntityFrameworkStores<SQLServerContext>()
    .AddErrorDescriber<CustomIdentityErrors>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(configureOptions =>
           {
               configureOptions.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = builder.Configuration["JWtConfig:issuer"],
                   ValidAudience = builder.Configuration["JWtConfig:audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWtConfig:Key"])),
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = false,
               };
               configureOptions.SaveToken = true; // HttpContext.GetTokenAsunc();
           });


builder.Services.AddScoped<IJWTAuthManager, JWTAuthManager>();
builder.Services.AddScoped<IProductServices, ProductServices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("EnableCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
