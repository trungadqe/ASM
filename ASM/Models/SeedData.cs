using Microsoft.EntityFrameworkCore;
using ASM.Data;

namespace ASM.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ASMContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ASMContext>>()))
            {
                // Look for any movies.
                if (context.Category.Any())
                {
                    return;   // DB has been seeded
                }

                context.Category.AddRange(
                    new  Category 
                    {
                        Name = "Commic",
                        Description = null,
                    },
                    new Category
                    {
                        Name = "Novel",
                        Description = null,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}