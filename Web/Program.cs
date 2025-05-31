using Business;
using Data;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework - CONFIGURACIÓN CORREGIDA
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions =>
        {
            sqlServerOptions.MigrationsAssembly("Web"); // Especifica que las migraciones van en Web
            sqlServerOptions.CommandTimeout(60);
        }
    ));

// Register repositories
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();

// Register services
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IDetalleFacturaService, DetalleFacturaService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(builder.Configuration["OrigenesPermitidos"] ?? "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ejecutar migraciones automáticamente con mejor logging
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("🔄 Verificando estado de la base de datos...");

        // Verificar si puede conectar
        var canConnect = await context.Database.CanConnectAsync();
        logger.LogInformation($"📡 Conexión a BD: {(canConnect ? "✅ Exitosa" : "❌ Fallida")}");

        if (!canConnect)
        {
            logger.LogWarning("⚠️ No se puede conectar a la base de datos. Verifica la cadena de conexión.");
        }
        else
        {
            // Verificar migraciones pendientes
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();

            logger.LogInformation($"📋 Migraciones aplicadas: {appliedMigrations.Count()}");
            logger.LogInformation($"⏳ Migraciones pendientes: {pendingMigrations.Count()}");

            if (pendingMigrations.Any())
            {
                logger.LogInformation("🔧 Aplicando migraciones pendientes...");
                foreach (var migration in pendingMigrations)
                {
                    logger.LogInformation($"   - {migration}");
                }

                await context.Database.MigrateAsync();
                logger.LogInformation("✅ Todas las migraciones aplicadas correctamente");
            }
            else
            {
                logger.LogInformation("✅ Base de datos actualizada - No hay migraciones pendientes");
            }

            // Verificar que las tablas existan
            var facturaTableExists = await context.Database.SqlQueryRaw<int>(
                "SELECT COUNT(*) as Value FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Facturas'"
            ).FirstOrDefaultAsync();

            var detalleTableExists = await context.Database.SqlQueryRaw<int>(
                "SELECT COUNT(*) as Value FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DetallesFactura'"
            ).FirstOrDefaultAsync();

            logger.LogInformation($"📊 Tabla Facturas: {(facturaTableExists > 0 ? "✅ Existe" : "❌ No existe")}");
            logger.LogInformation($"📊 Tabla DetallesFactura: {(detalleTableExists > 0 ? "✅ Existe" : "❌ No existe")}");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Error durante la configuración de base de datos: {Message}", ex.Message);

        if (app.Environment.IsDevelopment())
        {
            logger.LogError("🔍 Detalles completos del error:");
            logger.LogError(ex.ToString());
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();
app.MapControllers();

app.Run();