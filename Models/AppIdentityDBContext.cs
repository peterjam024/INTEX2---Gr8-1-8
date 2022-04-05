using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrashySmashy.Models
{
    //inherits from login stuff!
    public class AppIdentityDBContext : IdentityDbContext<IdentityUser>
    {
        //contstructor
        public AppIdentityDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
