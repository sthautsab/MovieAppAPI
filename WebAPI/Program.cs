using DAL;
using DAL.Dependency;
using DAL.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Dependency;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MovieContextConnection") ?? throw new InvalidOperationException("Connection string 'MovieContextConnection' not found.");

builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MovieContext>();
//var connectionString = builder.Configuration.GetConnectionString("MovieConnect") ?? throw new InvalidOperationException("Connection string 'MovieContextConnection' not found.");

//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MovieContext>();
var configurations = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDALServices(configurations);
builder.Services.AddServiceServices(configurations);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var role = scope.ServiceProvider.GetRequiredService<ISeedRole>();
    await role.Initialize();
}

app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapControllers();

app.Run();
