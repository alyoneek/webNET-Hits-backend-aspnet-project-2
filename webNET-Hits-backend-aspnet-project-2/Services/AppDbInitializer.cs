using System.Reflection.Metadata;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataBaseContext>();

                context.Database.EnsureCreated();

                if (!context.DishCategories.Any())
                {
                    context.DishCategories.AddRange(new List<DishCategory>()
                    {   new DishCategory()
                        {
                            Id = 1,
                            Name = DishCategoryType.Wok.ToString()
                        },
                        new DishCategory()
                        {
                            Id = 2,
                            Name = DishCategoryType.Pizza.ToString()
                        },
                        new DishCategory()
                        {
                            Id = 3,
                            Name = DishCategoryType.Soup.ToString()
                        },
                        new DishCategory()
                        {
                            Id = 4,
                            Name = DishCategoryType.Dessert.ToString()
                        },
                        new DishCategory()
                        {
                            Id = 5,
                            Name = DishCategoryType.Drink.ToString()
                        },
                    });
                    context.SaveChanges();
                }

                Dish milkshakeClassic = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Коктейль классический",
                    Description = "Классический молочный коктейль",
                    Price = 140,
                    Image = "https://mistertako.ru/uploads/products/120b46bc-5f32-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = true,
                    DishCategoryId = 5
                };
                Dish morsSmorodina = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Морс cмородиновый",
                    Description = "Смородиновый морс",
                    Price = 90,
                    Image = "https://mistertako.ru/uploads/products/120b46c1-5f32-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = true,
                    DishCategoryId = 5
                };
                Dish tomYamKai = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Том ям кай",
                    Description = "Знаменитый тайский острый суп со сливками, куриным филе, шампиньонами, красным луком, помидором, перчиком Чили и кинзой. Подается с рисом. БЖУ на 100 г. Белки, г — 5,75 Жиры, г — 3,72 Углеводы, г — 14,76",
                    Price = 300,
                    Image = "https://mistertako.ru/uploads/products/ccd8e2de-5f36-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = false,
                    DishCategoryId = 3
                };
                Dish rollBanana = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Сладкий ролл с бананом и киви",
                    Description = "Сырная лепешка, банан, киви, сливочный сыр, топинг клубничный",
                    Price = 220,
                    Image = "https://mistertako.ru/uploads/products/9e7c8581-7a6f-11eb-850a-0050569dbef0.jpeg",
                    Vegetarian = true,
                    DishCategoryId = 4
                };
                Dish milkshakeChocolate = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Коктейль шоколадный",
                    Description = "Классический молочный коктейль с добавлением шоколадного топпинга",
                    Price = 170,
                    Image = "https://mistertako.ru/uploads/products/120b46be-5f32-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = true,
                    DishCategoryId = 5
                };
                Dish redBarhat = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Красный бархат",
                    Description = "Шоколадный торт тёмно-красного, ярко-красного или красно-коричневого цвета. Традиционно готовится как слоёный пирог с глазурью из сливочного сыра.",
                    Price = 180,
                    Image = "https://hochu-gotovit.ru/wp-content/uploads/2019/01/krasnyj-barhat-13.jpg",
                    Vegetarian = false,
                    DishCategoryId = 4
                };
                Dish tomYamVegetables = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Wok том ям с овощами",
                    Description = "Лапша пшеничная, шампиньоны, лук красный, заправка Том Ям (паста Том Ям, паста Том Кха, сахар, соевый соус), сливки, соевый соус, помидор, перец чили. БЖУ на 100 г. Белки, г - 5,32 Жиры, г - 14,89 Углеводы, г - 22,46",
                    Price = 250,
                    Image = "https://mistertako.ru/uploads/products/cd661716-54ed-11ed-8575-0050569dbef0.jpg",
                    Vegetarian = true,
                    DishCategoryId = 1
                };
                Dish fourCheeses = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "4 сыра",
                    Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
                    Price = 360,
                    Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
                    Vegetarian = true,
                    DishCategoryId = 2
                };
                Dish tomYamChicken = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Wok том ям с курицей",
                    Description = "Лапша пшеничная, куриное филе, шампиньоны, лук красный, заправка Том Ям (паста Том Ям, паста Том Кха, сахар, соевый соус), сливки, соевый соус, помидор, перец чили. БЖУ на 100 г. Белки, г - 7,05 Жиры, г - 12,92 Углеводы, г - 18,65",
                    Price = 280,
                    Image = "https://mistertako.ru/uploads/products/a41bd9fd-54ed-11ed-8575-0050569dbef0.jpg",
                    Vegetarian = false,
                    DishCategoryId = 1
                };
                Dish rollPeanut = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Сладкий ролл с арахисом и бананом",
                    Description = "Сырная лепешка, банан, арахис, сливочный сыр, шоколадная крошка, топинг карамельный",
                    Price = 210,
                    Image = "https://mistertako.ru/uploads/products/a4772f7a-7a6f-11eb-850a-0050569dbef0.jpeg",
                    Vegetarian = true,
                    DishCategoryId = 4
                };
                Dish rollOrange = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Сладкий ролл с апельсином и бананом",
                    Description = "Апельсин, банан, шоколадная крошка, сыр творожный, сырная лепешка. БЖУ на 100 г. Белки, г - 5,86 Жиры, г - 13,12 Углеводы, г - 44,05",
                    Price = 250,
                    Image = "https://mistertako.ru/uploads/products/05391255-54ee-11ed-8575-0050569dbef0.jpg",
                    Vegetarian = true,
                    DishCategoryId = 4
                };
                Dish partyBBQ = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Party BBQ",
                    Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
                    Price = 480,
                    Rating = 4,
                    Image = "https://mistertako.ru/uploads/products/839d0250-8327-11ec-8575-0050569dbef0.",
                    Vegetarian = false,
                    DishCategoryId = 2
                };
                Dish cheaseCakeNewYork = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Чизкейк Нью-Йорк",
                    Description = "Чизкейк Нью-Йорк - настоящая классика. Его основа - сочетание вкусов нежнейшего сливочного сыра и тонкой песочной корочки.",
                    Price = 210,
                    Image = "https://mistertako.ru/uploads/products/120b46b1-5f32-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = true,
                    DishCategoryId = 4
                };
                Dish wokBoloneze = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Wok болоньезе",
                    Description = "Пшеничная лапша обжаренная на воке с фаршем (Говядина/свинина) и овощами (шампиньоны, перец сладкий, лук красный) в томатном соусе с добавлением чесночно–имбирной заправки и петрушки. БЖУ на 100 г. Белки, г — 8,07 Жиры, г — 15,38 Углеводы, г — 23,22",
                    Price = 290,
                    Image = "https://mistertako.ru/uploads/products/663ab866-85ec-11ea-a9ab-86b1f8341741.jpg",
                    Vegetarian = false,
                    DishCategoryId = 1
                };
                Dish belissimo = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Белиссимо",
                    Description = "Копченая куриная грудка, свежие шампиньоны, маринованные опята, сыр «Моцарелла», сыр «Гауда», сливочно-чесночный соус, свежая зелень.",
                    Price = 400,
                    Image = "https://mistertako.ru/uploads/products/9ee8ed49-8327-11ec-8575-0050569dbef0.jpg",
                    Vegetarian = false,
                    DishCategoryId = 2
                };
                Dish ramenChicken = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Сливочный рамен с курицей и шампиньонами",
                    Description = "Бульон рамен со сливками (куриный бульон, чесночно-имбирная заправка, соевый соус) с пшеничной лапшой, отварной курицей, омлетом Томаго и шампиньонами. БЖУ на 100 г. Белки, г — 8,13 Жиры, г — 6,18 Углеводы, г — 8,08",
                    Price = 260,
                    Image = "https://mistertako.ru/uploads/products/ccd8e2de-5f36-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = false,
                    DishCategoryId = 3
                };
                Dish wokDiablo = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Wok а-ля Диаблo",
                    Description = "Пшеничная лапша обжаренная на воке с колбасками пепперони, маслинами, сладким перцем и перцем халапеньо в томатном соусе с добавлением петрушки. БЖУ на 100 г. Белки, г — 8,18 Жиры, г — 16,33 Углеводы, г — 20,62",
                    Price = 330,
                    Image = "https://mistertako.ru/uploads/products/663ab868-85ec-11ea-a9ab-86b1f8341741.jpg",
                    Vegetarian = false,
                    DishCategoryId = 1
                };
                Dish ramenCheese = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Рамен сырный",
                    Description = "Сырный бульон с пшеничной лапшой, отварным куриным филе,помидором и сырными шариками. БЖУ на 100 г. Белки, г — 11,8 Жиры, г — 9,82 Углеводы, г — 22,69",
                    Price = 300,
                    Rating = 10,
                    Image = "https://mistertako.ru/uploads/products/ccd8e2de-5f36-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = false,
                    DishCategoryId = 3
                };
                Dish morsOblepiha = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Морс облепиховый",
                    Description = "Облепиха, имбирь, сахар",
                    Price = 90,
                    Image = "https://mistertako.ru/uploads/products/5a7d58a5-879d-11eb-850a-0050569dbef0.jpg",
                    Vegetarian = true,
                    DishCategoryId = 5
                };
                Dish milkshakeStrawberry = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Коктейль клубничный",
                    Description = "Классический молочный коктейль с клубничным топпингом",
                    Price = 170,
                    Image = "https://mistertako.ru/uploads/products/120b46bd-5f32-11e8-8f7d-00155dd9fd01.jpg",
                    Vegetarian = true,
                    DishCategoryId = 5
                };
                Dish tomYamSea = new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Wok том ям с морепродуктам",
                    Description = "Лапша пшеничная, креветки, кальмар, шампиньоны, лук красный, заправка Том Ям (паста Том Ям, паста Том Кха, сахар, соевый соус), сливки, соевый соус, помидор, перец чили. БЖУ на 100 г. Белки, г - 8,57 Жиры, г - 12,8 Углеводы, г - 18,8",
                    Price = 340,
                    Image = "https://mistertako.ru/uploads/products/bacd88ca-54ed-11ed-8575-0050569dbef0.jpg",
                    Vegetarian = false,
                    DishCategoryId = 1
                };

                User user1 = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Том Хиддлстон",
                    Email = "tom-hiddlston11@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password1"),
                    Address = "США, Калифорния",
                    Gender = GenderType.Male
                };

                User user2 = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Элизабет Олсен",
                    Email = "scarlet_witch0@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("scarlet2"),
                    Address = "США, Калифорния",
                    Gender = GenderType.Female,
                    BirthDate = DateTime.SpecifyKind(DateTime.Parse("02.02.1989"), DateTimeKind.Utc)
                };

                Order order1 = new Order()
                {
                    Id = Guid.NewGuid(),
                    UserId = user1.Id,
                    DeliveryTime = DateTime.SpecifyKind(DateTime.UtcNow.AddMinutes(30), DateTimeKind.Utc),
                    Status = OrderStatus.InProcess,
                    Price = 1180,
                    Address = "США, Калифорния"
                };

                Order order2 = new Order()
                {
                    Id = Guid.NewGuid(),
                    UserId = user1.Id,
                    DeliveryTime = DateTime.SpecifyKind(DateTime.Parse("12.05.2022"), DateTimeKind.Utc),
                    OrderTime = DateTime.SpecifyKind(DateTime.Parse("11.05.2022"), DateTimeKind.Utc),
                    Status = OrderStatus.Delivered,
                    Price = 900,
                    Address = "США, Калифорния"
                };

                Order order3 = new Order()
                {
                    Id = Guid.NewGuid(),
                    UserId = user2.Id,
                    DeliveryTime = DateTime.SpecifyKind(DateTime.Parse("13.05.2022"), DateTimeKind.Utc),
                    OrderTime = DateTime.SpecifyKind(DateTime.Parse("10.05.2022"), DateTimeKind.Utc),
                    Status = OrderStatus.Delivered,
                    Price = 480,
                    Address = "США, Калифорния"
                };

                if (!context.Dishes.Any())
                {
                    context.Dishes.AddRange(rollBanana, milkshakeClassic, morsSmorodina, tomYamKai, milkshakeChocolate,
                        redBarhat, tomYamVegetables, fourCheeses, tomYamChicken, rollPeanut, rollOrange, partyBBQ,
                        cheaseCakeNewYork, wokBoloneze, belissimo, ramenChicken, wokDiablo, ramenCheese, morsOblepiha,
                        milkshakeStrawberry, tomYamSea);
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(user1, user2);
                    context.SaveChanges();
                }

                //if (!context.Orders.Any())
                //{
                //    context.Orders.AddRange(order1, order2, order3);
                //    context.SaveChanges();
                //}

                //if (!context.DishesInBasket.Any())
                //{
                //    context.DishesInBasket.AddRange(new List<DishInBasket>()
                //    {
                //        new DishInBasket()
                //        {
                //            CartId = user1.Id,
                //            DishId = tomYamKai.Id,
                //            OrderId = order1.Id,
                //            Amount = 3
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user1.Id,
                //            DishId = milkshakeClassic.Id,
                //            OrderId = order1.Id,
                //            Amount = 2
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user1.Id,
                //            DishId = ramenCheese.Id,
                //            OrderId = order2.Id,
                //            Amount = 3
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user2.Id,
                //            DishId = partyBBQ.Id,
                //            OrderId = order3.Id,
                //            Amount = 1
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user2.Id,
                //            DishId = milkshakeStrawberry.Id,
                //            Amount = 2
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user2.Id,
                //            DishId = wokBoloneze.Id,
                //            Amount = 1
                //        },
                //        new DishInBasket()
                //        {
                //            CartId = user2.Id,
                //            DishId = tomYamSea.Id,
                //            Amount = 2
                //        },
                //    });
                //    context.SaveChanges();
                //}

                //if (!context.Ratings.Any())
                //{
                //    context.Ratings.AddRange(new List<Rating>()
                //    {
                //        new Rating()
                //        {
                //            UserId = user1.Id,
                //            DishId = ramenCheese.Id,
                //            RatingScore = 10
                //        },
                //        new Rating()
                //        {
                //            UserId = user2.Id,
                //            DishId = partyBBQ.Id,
                //            RatingScore = 4
                //        },
                //    });
                //    context.SaveChanges();
                //}
            }
        }
    }
}
