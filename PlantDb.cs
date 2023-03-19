using Microsoft.EntityFrameworkCore;

class PlantDb : DbContext
{
    public PlantDb(DbContextOptions<PlantDb> options)
        : base(options) { }

    public DbSet<Plant> Plants => Set<Plant>();

}