namespace Racing_Club.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            // Clubs
            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(new List<Club>()
                {
                    new Club()
                    {
                        Title = "Racing Club A",
                        Image =
                            "https://media.istockphoto.com/vectors/checkered-flag-for-car-racing-or-rally-club-modern-illustration-vector-id1339105507",
                        Description = "This is the description of the first club",
                        ClubCategory = ClubCategory.City,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Belo Horizonte",
                            State = "MG"
                        }
                    },

                    new Club()
                    {
                        Title = "Racing Club B",
                        Image =
                            "https://media.istockphoto.com/vectors/racing-club-round-linear-logo-of-speed-racing-on-black-background-vector-id1185938729?s=612x612",
                        Description = "This is the description of the second club",
                        ClubCategory = ClubCategory.NatureSide,
                        Address = new Address()
                        {
                            Street = "694 Principal St",
                            City = "Niteroi",
                            State = "RJ"
                        }
                    },

                    new Club()
                    {
                        Title = "Racing Club C",
                        Image =
                            "https://media.istockphoto.com/vectors/design-of-racing-car-team-badge-vector-id1250970452?s=612x612",
                        Description = "This is the description of the third club",
                        ClubCategory = ClubCategory.PrivateProperty,
                        Address = new Address()
                        {
                            Street = "Lacemakers Court 22 St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    }
                });
                context.SaveChanges();
            }

            // Races
            if (!context.Races.Any())
            {
                context.Races.AddRange(new List<Race>()
                {
                    new Race()
                    {
                        Title = "Racing 1",
                        Image =
                            "https://images.pexels.com/photos/10342583/pexels-photo-10342583.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                        Description = "This is the description of the first race",
                        RaceCategory = RaceCategory.RaceTrack,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Belo Horizonte",
                            State = "MG"
                        }
                    },
                    new Race()
                    {
                        Title = "Racing 2",
                        Image =
                            "https://images.pexels.com/photos/9843281/pexels-photo-9843281.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                        Description = "This is the description of the second race",
                        RaceCategory = RaceCategory.Rally,
                        AddressId = 2,
                        Address = new Address()
                        {
                            Street = "Lacemakers Court 22 St",
                            City = "Charllote",
                            State = "NC"
                        }
                    }
                });
                context.SaveChanges();
            }
        }
    }


    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "teddysmithdeveloper@gmail.com"; // Here you can use your email (test purposes)

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    UserName = "jgvm22", // Here you can use a personalized nickname
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    Address = new Address
                    {
                        Street = "St Somewhere 85",
                        City = "RJ",
                        State = "Rio de Janeiro"
                    }
                };
                await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }


            string appUserEmail = "user@etickets.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new AppUser()
                {
                    UserName = "app-user",
                    Email = appUserEmail,
                    EmailConfirmed = true,
                    Address = new Address
                    {
                        Street = "St Somewhere 85",
                        City = "RJ",
                        State = "Rio de Janeiro"
                    }
                };
                await userManager.CreateAsync(newAppUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}