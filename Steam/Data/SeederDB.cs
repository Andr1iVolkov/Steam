using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Constants;
using Steam.Data.Entities;
using Steam.Data.Entities.Identity;



namespace Steam.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;

                //Отримую посилання на наш контекст
                var context = service.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                #region Додавання користувачів та ролей

                if (!context.Roles.Any())
                {
                    foreach (var role in Roles.All)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity
                        {
                            Name = role
                        }).Result;
                    }
                }

                if (!context.Users.Any())
                {
                    var user = new UserEntity
                    {
                        NickName = "SteamAdmin",
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                        Password = "123456"
                    };
                    var result = userManager.CreateAsync(user, "123456").Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, "Admin").Result;
                    }
                }
                #endregion

                #region Додавання категорій

                if (context.Categories.Count() < 10)
                {
                    var gameCategories = new[]
                    {
                            "Action", "Adventure", "RPG", "Strategy", "Shooter",
                            "Puzzle", "Racing", "Simulation", "Sports", "Fighting"
                        };

                    foreach (var categoryName in gameCategories)
                    {
                        var category = new CategoryEntity
                        {
                            Name = categoryName
                        };
                        context.Categories.Add(category);
                    }
                    context.SaveChanges();
                }

                if (context.Games.Count() < 10)
                {
                    var categoriesId = context.Categories.Select(c => c.Id).ToList();

                    var fakeGame = new Faker<GameEntity>("uk")
                        .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                        .RuleFor(o => o.Price, f => Math.Round(f.Random.Decimal(10, 100), 2))
                        .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                        .RuleFor(o => o.DateOfRelease, f => f.Date.PastOffset(1).UtcDateTime)
                        .RuleFor(o => o.SystemRequirements, f => f.Lorem.Sentence());

                    var games = fakeGame.Generate(10);

                    foreach (var game in games)
                    {
                        context.Games.Add(game);
                    }
                    context.SaveChanges();

                    // Додавання зв'язків між іграми та категоріями
                    foreach (var game in games)
                    {
                        foreach (var categoryId in categoriesId)
                        {
                            context.GameCategory.Add(new GameCategoryEntity
                            {
                                GameId = game.Id,
                                CategoryId = categoryId
                            });
                        }
                    }
                    context.SaveChanges();
                }

                #endregion

                #region Додавання зображень ігор

                if (context.GameImages.Count() < 30)
                {
                    var gamesId = context.Games.Select(g => g.Id).ToList();

                    var fakeGameImage = new Faker<GameImageEntity>("uk")
                        .RuleFor(o => o.Name, f => $"game_image_{f.Random.Number(1, 100)}.jpg")
                        .RuleFor(o => o.Priority, f => (byte)f.Random.Number(1, 3));

                    foreach (var gameId in gamesId)
                    {
                        var images = fakeGameImage.Generate(3);
                        foreach (var image in images)
                        {
                            image.GameId = gameId;
                            context.GameImages.Add(image);
                        }
                    }
                    context.SaveChanges();
                }

                #endregion

                #region Додавання новин

                if (context.News.Count() < 10)
                {
                    var gamesId = context.Games.Select(g => g.Id).ToList();

                    var fakeNews = new Faker<NewsEntity>("uk")
                        .RuleFor(o => o.Title, f => f.Lorem.Sentence())
                        .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                        .RuleFor(o => o.DateOfRelease, f => f.Date.PastOffset(1).UtcDateTime)
                        .RuleFor(o => o.Image, f => $"news_image_{f.Random.Number(1, 100)}.jpg")
                        .RuleFor(o => o.VideoURL, f => $"https://videos.com/{f.Random.Guid()}.mp4");

                    var newsList = fakeNews.Generate(10);
                    var random = new Random();

                    foreach (var news in newsList)
                    {
                        news.GameId = gamesId[random.Next(0, gamesId.Count - 1)];
                        context.News.Add(news);
                    }
                    context.SaveChanges();
                }

                #endregion

                #region Додавання користувачів та їхніх ігор

                if (context.UserGames.Count() < 10)
                {
                    var users = context.Users.ToList();
                    var games = context.Games.ToList();

                    foreach (var someUser in users)
                    {
                        var userGames = new Faker<UserGameEntity>("uk")
                            .RuleFor(o => o.UserId, someUser.Id)
                            .RuleFor(o => o.GameId, f => games[f.Random.Number(0, games.Count - 1)].Id)
                            .Generate(2);

                        context.UserGames.AddRange(userGames);
                    }
                    context.SaveChanges();
                }

                #endregion
            }




        }
    }
}
