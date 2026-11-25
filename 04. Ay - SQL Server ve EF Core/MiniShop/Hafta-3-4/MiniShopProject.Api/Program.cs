using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiniShopDb"));

    // Sadece geliştirmede: SQL loglarında parametre değerlerini de gösterir (@p='0' gibi)
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

var app = builder.Build();

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
