using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GloboClimaPlatform.Infra;
using GloboClimaPlatform.Infra.Extensions.ServiceRegistration;
using GloboClimaPlatform.Infra.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://localhost:5000");
}

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
        ValidAudience = builder.Configuration["Jwt:Audience"]!,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero // Optional: adjust token expiration tolerance
    };
    
    options.MapInboundClaims = false;
    
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Skip the default behavior of returning a 401 Unauthorized response
            context.HandleResponse();

            // Redirect to the login page
            context.Response.Redirect("/auth/login");
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("SqliteConn");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

// Register the AmazonDynamoDBClient with DI
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddDynamoDbServices();


builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddCitiesServices();
builder.Services.AddCountriesApiServices();
builder.Services.AddUsersServices();
builder.Services.AddJwtServices();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<JwtCookieMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();