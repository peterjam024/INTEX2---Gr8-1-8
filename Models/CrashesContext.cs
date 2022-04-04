using System;
using Microsoft.EntityFrameworkCore;

namespace CrashySmashy.Models
{
    public class CrashesContext : DbContext
    {
        //constructor
        public CrashesContext (DbContextOptions<CrashesContext> options) : base(options)
        {
            //leave blank
        }

        public DbSet<Crash> Crashes { get; set; }
    }
}
