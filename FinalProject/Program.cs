using FinalProject.DAL;
using FinalProject.Repositories;
using FinalProject.Repositories.Utility;
using FinalProject.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddDbContext<BankContext>(options =>

    options.UseSqlServer("Data Source = 192.168.2.185\\beta; Initial Catalog = SmallOffice; Integrated Security = True; Encrypt = False")
);
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//      builder =>
//      {
//          builder.WithOrigins("http://localhost:5231", "http://localhost:4200")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//      });
//});
builder.Services.AddTransient<SavingsAccount>();
builder.Services.AddTransient<CurrentAccount>();
builder.Services.AddTransient<Func<string,IAccountType>>(ServiceProvider => key =>
{
    switch (key)
    {
        case "saving":
            return ServiceProvider.GetService<SavingsAccount>();
        case "current":
            return ServiceProvider.GetService<CurrentAccount>();
        default:
            return null;

    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//app.UseCors(builder =>
//{
//    builder
//    .AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader();
//});

app.Run();
