using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrashySmashy.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminUser2 = "admin2";

        private const string adminPassword = "Passw3rd!gr8i8";
        private const string adminPassword2 = "Passw3rd!gr8i8YEET";



        //check to see data is there --> (void means nothing will be returned)
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            //first pass = just try to kind of get it
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();


            //check to see if migrations and actually do them if there are!
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }


            //this is us creating the user manager = INSTANCE of it!
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


            //this gets the const user we defined above!
            //this is the name of the identiy user
            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            IdentityUser user2 = await userManager.FindByIdAsync(adminUser2);

            //if the above user is null THEN
            if (user == null)
            {
                user = new IdentityUser(adminUser);

                user.Email = "admin@yeet.com";

                user.PhoneNumber = "555-1234";

                //creates a new entry in database for this particular user and it uses this password
                await userManager.CreateAsync(user, adminPassword);
            }
            if (user2 == null)
            {
                user2 = new IdentityUser(adminUser2);

                user2.Email = "admin2@yeet.com";

                user2.PhoneNumber = "555-1235";

                //creates a new entry in database for this particular user and it uses this password
                await userManager.CreateAsync(user2, adminPassword2);
            }

        }
    }

}
