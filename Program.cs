using ef_tpc.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/dividends", async (AppDbContext context) =>
{
    var dividends = await context.Dividends.ToListAsync();

    return Results.Ok(dividends);
});

app.MapGet("/yields", async (AppDbContext context) =>
{
    var yields = await context.Yields.ToListAsync();

    return Results.Ok(yields);
});

app.MapGet("/yields/by-investment/{investmentId}", async (AppDbContext context, int investmentId) =>
{
    var yields = await context.Yields
        .Include(x => x.Investment)
        .Where(x => x.Investment.Id == investmentId)
        .ToListAsync();

    return Results.Ok(yields);
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
