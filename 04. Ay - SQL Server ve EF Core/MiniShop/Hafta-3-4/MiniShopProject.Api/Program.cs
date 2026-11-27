using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniShopDbContext>(options =>
{
    // Bağlantı cümlesi User Secrets'tan gelir; ayarlanmamışsa anlaşılır bir hata verelim
    var connectionString = builder.Configuration.GetConnectionString("MiniShopDb")
        ?? throw new InvalidOperationException("MiniShopDb bağlantı cümlesi bulunamadı. User Secrets ayarlı mı?");
    options.UseSqlServer(connectionString);

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
