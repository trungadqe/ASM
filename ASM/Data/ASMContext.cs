#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM.Models;

namespace ASM.Data
{
    public class ASMContext : DbContext
    {
        public ASMContext (DbContextOptions<ASMContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Book> Book { get; set; } = null!;
        public object Categories { get; internal set; }
    }
}
