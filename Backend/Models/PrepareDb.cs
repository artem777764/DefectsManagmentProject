using Backend.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public static class PrepareDb
{
    private static ApplicationDbContext? _context;
    
    public static void Prepare(IApplicationBuilder app)
    {

        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!;
            _context.Database.Migrate();
        }
    }
}