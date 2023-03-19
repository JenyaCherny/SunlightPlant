using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PlantDb>(opt => opt.UseInMemoryDatabase("PlantList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/plants", async (PlantDb db) =>
    await db.Plants.ToListAsync());

app.MapGet("/plants/{id}", async (int id, PlantDb db) =>
    await db.Plants.FindAsync(id)
        is Plant plant
            ? Results.Ok(plant)
            : Results.NotFound());

app.MapPost("/plants", async (Plant plant, PlantDb db) =>
{
    db.Plants.Add(plant);
    await db.SaveChangesAsync();

    return Results.Created($"/plants/{plant.Id}", plant);
});

app.MapPut("/plants/{id}", async (int id, Plant inputPlant, PlantDb db) =>
{
    var plant = await db.Plants.FindAsync(id);

    if (plant is null) return Results.NotFound();

    plant.PlantName = inputPlant.PlantName;
    plant.PlantDescription = inputPlant.PlantDescription;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/plants/{id}", async (int id, PlantDb db) =>
{
    if (await db.Plants.FindAsync(id) is Plant plant)
    {
        db.Plants.Remove(plant);
        await db.SaveChangesAsync();
        return Results.Ok(plant);
    }

    return Results.NotFound();
});

app.Run();