using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// for authentication 
// when any request hits [Authorize] endpoint, it will authenticate the request(sent in auth headers) using jwt tokens 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // uses authentications, sets jwt as default authentication method
    .AddJwtBearer(options => // configures how jwt tokens should be read and validated (read jet, extract user info)
    {
        options.TokenValidationParameters = new TokenValidationParameters // creates the rulebook
        {
            ValidateIssuer = true, // if this is false, app would accept tokens from any issuer
            ValidateAudience = true,
            ValidateLifetime = true, // checks if token is expired
            ValidateIssuerSigningKey = true, /// uses token created using our security key only
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // compares the value in appsettings and in token, if true then proceed
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // converts our secret key into cryptographic key
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication-Shreya");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
