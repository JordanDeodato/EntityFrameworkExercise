using EntityFrameworkExercise.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var services = builder.Services;

var connectionString = configuration.GetConnectionString("StoreContext")
    ?? throw new InvalidOperationException("Connection string 'StoreContext' not found.");

services.AddScoped<SoftDeleteInterceptor>();
services.AddDbContext<StoreContext>((serviceProvider, options) =>
{
    options.UseSqlite(connectionString, p =>
    {
        p.MigrationsHistoryTable("__EFMigrationsHistory", "store");
    });

    var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();
    options.AddInterceptors(interceptor);
});

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    context.Database.Migrate();
}
app.Run();