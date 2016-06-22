using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Intouch.Core
{
    public class CoreContextSeedInitializer : DropCreateDatabaseIfModelChanges<CoreContext>
    {
        protected override void Seed(CoreContext db)
        {
            base.Seed(db);

            SeedWiFi(db);
            SeedRestaurant(db);

        }

        private void SeedWiFi(CoreContext db)
        {
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "INTOUCH media",
                PersonalizationEnable = true,
                Points = new[]
                {
                    new Point {
                        Name = "Мiсто",
                        Ip = "192.168.88.1",
                        Address = "г.Харьков, Клочковская 192А", City = City.Moscow, Latitude = 50.029908, Longitude = 36.207557
                    },
                    new Point
                    {
                        Name = "Горбушкин двор",
                        Ip = "192.168.88.1",
                        Address = "Багратионовский пр-д 7, к. 1", City = City.Moscow, Latitude = 55.7419778, Longitude = 37.5011286
                    }
                }
            });
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "WorldClass",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 3,
                        Name = "Шереметьевская",
                        Ip = "195.128.55.163",
                        Address = "12-й проезд Марьиной Рощи, вл.9, стр.2", City = City.Moscow, Latitude = 55.804424, Longitude = 37.618329
                    },
                    new Point
                    {
                        Id = 4,
                        Name = "Олимпийский",
                        Ip = "195.128.52.235",
                        Address = "Олимпийский проспект, д.16, стр.2", City = City.Moscow, Latitude = 55.783519, Longitude = 37.6265069
                    },
                    new Point
                    {
                        Id = 5,
                        Name = "Кунцево",
                        Ip = "195.128.55.235",
                        Address = "ул. Ивана Франко, д.16", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 6,
                        Name = "Тульская",
                        Ip = "195.128.55.235",
                        Address = "Варшавское шоссе, д.12 А", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 7,
                        Name = "Севастопольский",
                        Ip = "195.128.55.235",
                        Address = "Севастопольский пр-т, д. 28Г", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 8,
                        Name = "Павлово",
                        Ip = "195.128.55.235",
                        Address = "Истринский р-н, с/п. Павло-Слободское, д. Новинки, д.115, стр.9", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 9,
                        Name = "Наметкина",
                        Ip = "195.128.55.235",
                        Address = "ул. Намёткина, д.6, корп.1", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 10,
                        Name = "Lady's",
                        Ip = "195.128.55.235",
                        Address = "ул.Профсоюзная, д.31, стр.5", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 11,
                        Name = "Воробьевы горы",
                        Ip = "195.128.55.235",
                        Address = "ул. Мосфильмовская, 70", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 12,
                        Name = "Триумф Палас",
                        Ip = "195.128.55.235",
                        Address = "Чапаевский пер., вл.3", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 13,
                        Name = "Митино",
                        Ip = "195.128.55.235",
                        Address = "Пятницкое ш., 29, кор. 5", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 14,
                        Name = "Варшавка",
                        Ip = "195.128.55.235",
                        Address = "Варшавское ш., 122а", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 15,
                        Name = "Тверская",
                        Ip = "195.128.55.235",
                        Address = "ул. Большая Грузинская, вл.69-71", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 16,
                        Name = "Лодочная",
                        Ip = "195.128.55.235",
                        Address = "ул. Лодочная, д. 43", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 17,
                        Name = "Земляной вал",
                        Ip = "195.128.55.235",
                        Address = "ул. Земляной вал, д. 9", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 18,
                        Name = "Плежаевская",
                        Ip = "195.128.55.235",
                        Address = "ул.Демьяна Бедного, д.4к2", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 19,
                        Name = "Власова",
                        Ip = "195.128.55.235",
                        Address = "ул.Архитектора Власова, д.22", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 20,
                        Name = "Житная",
                        Ip = "195.128.55.235",
                        Address = "ул. Житная, д.14, стр.2", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 21,
                        Name = "Романовъ",
                        Ip = "195.128.55.235",
                        Address = "Романов пер., д.4, стр. 2", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 22,
                        Name = "Вернадского",
                        Ip = "195.128.55.235",
                        Address = "Вернадского, д.6", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 23,
                        Name = "Смоленский Пасаж",
                        Ip = "195.128.55.235",
                        Address = "Смоленская пл. д.3", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    },
                    new Point
                    {
                        Id = 24,
                        Name = "Климашкина",
                        Ip = "195.128.55.235",
                        Address = "ул. Климашкина, д. 17, стр. 2", City = City.Moscow, Latitude = 55.7283, Longitude = 37.438564
                    }
                }
            });
            db.SaveChanges();
            for (int i = 25; i <= 50; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();

            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "Dr. Loder",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 51,
                        Name = "Белорусская",
                        Ip = "1.2.3.4",
                        Address = "3-я улица Ямского Поля, 2к4", City = City.Moscow, Latitude = 55.78364, Longitude = 37.560017
                    },
                    new Point
                    {
                        Id = 52,
                        Name = "Академическая",
                        Ip = "1.2.3.4",
                        Address = "Дм.Ульянова, вл.31", City = City.Moscow, Latitude = 55.730399, Longitude = 37.573962
                    },
                    new Point
                    {
                        Id = 53,
                        Name = "Юбилейный",
                        Ip = "1.2.3.4",
                        Address = "Московская область, Юбилейный, улица Маяковского, 2", City = City.Moscow, Latitude = 55.7377349, Longitude = 37.5061069
                    },
                    new Point
                    {
                        Id = 54,
                        Name = "Сколково",
                        Ip = "1.2.3.4",
                        Address = "Тихая улица, 13 поселок Заречье, Одинцовский район, Московская область", City = City.Moscow, Latitude = 55.722184, Longitude = 37.4139849
                    },
                    new Point
                    {
                        Id = 55,
                        Name = "Остоженка",
                        Ip = "1.2.3.4",
                        Address = "ул. Остоженка, д.25", City = City.Moscow, Latitude = 55.747509, Longitude = 37.621749
                    }
                }
            });
            db.SaveChanges();
            for (int i = 56; i <= 70; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "Fitness One",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 71,
                        Name = "Мелоди",
                        Ip = "1.2.3.4",
                        Address = "МО, Истринский район, деревня Крючково, ул. Радости, коттеджный поселок «Мелоди»", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    },
                    new Point
                    {
                        Id = 72,
                        Name = "Истра центр",
                        Ip = "1.2.3.4",
                        Address = "МО, г. Истра, пл. Революции, д. 6", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    },
                    new Point
                    {
                        Id = 73,
                        Name = "Променад, Киевское шоссе",
                        Ip = "1.2.3.4",
                        Address = "МО, к/п Променад (Киевское ш.)", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    },
                    new Point
                    {
                        Id = 74,
                        Name = "Минское шоссе",
                        Ip = "1.2.3.4",
                        Address = "Одинцовский р-н, пос.Лесной Городок, ул.Грибовская, д.8", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            for (int i = 75; i <= 80; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "FitnessON",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 81,
                        Name = "Звенигород",
                        Ip = "1.2.3.4",
                        Address = "г. Звенигород, ул. Московская, дом 12", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    },
                    new Point
                    {
                        Id = 82,
                        Name = "Краснознаменск",
                        Ip = "1.2.3.4",
                        Address = "Краснознаменск, ул. Краснознаменная 23", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    },
                    new Point
                    {
                        Id = 83,
                        Name = "Большие Вяземы",
                        Ip = "1.2.3.4",
                        Address = "Большие Вяземы, пос. Школьный, дом 12 А", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            for (int i = 84; i <= 90; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "HardCandy",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 91,
                        Name = "Hard Candy Fitness (Клуб Мадонны)",
                        Ip = "1.2.3.4",
                        Address = "г. Москва, пер. Большой Кисловский, д. 9", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "De-VISION",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 92,
                        Name = "ТЦ Родео Драйв",
                        Ip = "1.2.3.4",
                        Address = "проспект Культуры, 1", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            for (int i = 93; i <= 95; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "Biosphere",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Id = 96,
                        Name = "Биосфера Калужская",
                        Ip = "1.2.3.4",
                        Address = "ул. Малая Калужская, д. 15, стр. 4", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            for (int i = 97; i <= 99; i++)
            {
                db.Points.Add(new Point
                {
                    Name = "Test",
                    Ip = "1.2.3.4",
                    Address = "test",
                    Latitude = 55.6848915,
                    Longitude = 37.5861277
                });
            }
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Fitness,
                Name = "Все точки",
                PersonalizationEnable = false,
                Points = new[]
                {
                    new Point
                    {
                        Name = "Вся Москва",
                        Ip = "1.2.3.4",
                        Address = "Москва", City = City.Moscow, Latitude = 55.6848915, Longitude = 37.5861277
                    }
                }
            });
            db.SaveChanges();
            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Karaoke,
                PersonalizationEnable = false,
                Name = "WHO IS WHO",
                Points = new[]
                {
                    new Point
                    {
                        Name = "WHO IS WHO",
                        Ip = "192.168.88.1",
                        Latitude = 37.5850654,
                        Longitude = 55.7519339,
                        Radius = 20.0,
                    }
                }
            });
            db.SaveChanges();

            db.Networks.Add(new Network
            {
                Branch = NetworkBranch.Restaurant,
                PersonalizationEnable = false,
                Name = "Coffee Life",
                Points = new[]
                {
                    new Point
                    {
                        Name = "На Гайдара",
                        Ip = "192.168.88.1",
                        Latitude = 55.7519339,
                        Longitude = 37.5850654,
                        Radius = 20.0,
                        Address = "ул. Новый Арбат, д.1"
                    },
                    new Point
                    {
                        Name = "Возле вокзала",
                        Ip = "192.168.88.2",
                        Latitude = 55.7519339,
                        Longitude = 37.5850654,
                        Radius = 20.0,
                        Address = "ул. Новый Арбат, д.2"
                    }
                }
            });
            db.SaveChanges();
        }

        private static Guid GetHashString(string password)
        {
            return new Guid(new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(password)));
        }

        private static void SeedRestaurant(CoreContext db)
        {
            var Point1 = db.Points.Find(102);
            var Point2 = db.Points.Find(103);

            var Network1 = db.Networks.Find(11);

            db.RestNetworks.Add(new RestNetwork {
                Id = 1,
                Name = Network1.Name,
                Description = "описание тестовой сети ресторанов",
                Network = Network1
            });

            var RestPoint1 = new RestPoint
            {
                Name = "На Гайдара",
                Description = "В нашей кофейне вас всегда ждут свежая пресса, бесплатный Wi-Fi, приятная обстановка, расслабляющая музыка и вежливый персонал. И конечно, гостей порадует широкий ассортимент свежеприготовленных кофейных напитков, большой выбор десертов и закусок собственного производства.",
                AveragePrice = "Наш средний уровень цен от .. и до ...",
                ConfirmationMessage = Core.ConfirmationMessage.Used,
                WorkTime = "от заката до рассвета ресторан 1",
                Skype = "Skype 1",
                Logo = "logo.png",
                Kitchen = "кухня ресторана 1",
                EmailToFeedback = "vodianitsky@gmail.com",
                EmailToReservation = "vodianitsky@gmail.com",
                Phone = "0661234567",
                BonusPercent = 15,
                Email = "vodianitsky@gmail.com",
                Point = Point1
            };
            var RestPoint2 = new RestPoint
            {
                Name = "Возле вокзала",
                Description = "описание тестового ресторана номер 2",
                AveragePrice = "Наш средний уровень цен от .. и до ...",
                ConfirmationMessage = Core.ConfirmationMessage.Used,
                WorkTime = "от заката до рассвета ресторан 2",
                Skype = "Skype 2",
                Logo = "logo.png",
                Kitchen = "кухня ресторана 2",
                EmailToFeedback = "vodianitsky@gmail.com",
                EmailToReservation = "vodianitsky@gmail.com",
                Phone = "0661234567",
                BonusPercent = 5,
                Email = "vodianitsky@gmail.com",
                Point = Point2
            };
            db.RestPoints.Add(RestPoint1);
            db.RestPoints.Add(RestPoint2);

            var RestNetwork1 = new RestNetwork
            {
                Name = "сеть ресторанов",
                Description = "описание тестового ресторана",
                Network = db.Networks.Find(11),
            };
            db.RestNetworks.Add(RestNetwork1);

            var RestAdvertisement1 = new RestAdvertisement { Name = "Реклама ресторана 1", Description = "описание рекламы ресторана 1", Photo = "http://st.kp.yandex.net/images/film_big/277328.jpg", RestPoint = RestPoint1 };
            var RestAdvertisement2 = new RestAdvertisement { Name = "Реклама ресторана 2", Description = "описание рекламы ресторана 2", Photo = "http://yaokino.ru/wp-content/uploads/2014/01/998214150.jpg", RestPoint = RestPoint1 };
            db.RestAdvertisements.Add(RestAdvertisement1);
            db.RestAdvertisements.Add(RestAdvertisement2);

            var RestNews1 = new RestNews { Name = "Новость ресторана 1", Description = "описание новости ресторана 1", SmallDescription = "мааааааленькое описание новости ресторана 1", DateCreate = DateTime.Now, Image = "photo1.jpg", RestPoint = RestPoint1 };
            var RestNews2 = new RestNews { Name = "Новость ресторана 2", Description = "описание новости ресторана 2", SmallDescription = "мааааааленькое описание новости ресторана 1", DateCreate = DateTime.Now, Image = "photo2.jpg", RestPoint = RestPoint1 };
            db.RestNewses.Add(RestNews1);
            db.RestNewses.Add(RestNews2);

            var user1 = new RestAppUser { Name = "Имя1", Password = "202cb962ac59075b964b07152d234b70", Phone = "066", Email = "email1@gmail.com", RegistrationDate = DateTime.UtcNow, Role = RestRole.User, CheckUser = CheckUser.Сonfirmed };
            var user2 = new RestAppUser { Name = "Имя2", Password = "202cb962ac59075b964b07152d234b70", Phone = "077", Email = "email2@gmail.com", RegistrationDate = DateTime.UtcNow, Role = RestRole.Admin, CheckUser = CheckUser.Сonfirmed, Point = Point1 };
            var user3 = new RestAppUser { Name = "Имя3", Password = "202cb962ac59075b964b07152d234b70", Phone = "088", Email = "email3@gmail.com", RegistrationDate = DateTime.UtcNow, Role = RestRole.Officiant, CheckUser = CheckUser.Сonfirmed, Point = Point1 };
            db.RestAppUsers.Add(user1);
            db.RestAppUsers.Add(user2);
            db.RestAppUsers.Add(user3);

            var socialUser1 = new SocialUser { RestUser = user1, LastActivityDate = DateTimeOffset.UtcNow };
            var socialUser2 = new SocialUser { RestUser = user2, LastActivityDate = DateTimeOffset.UtcNow };
            var socialUser3 = new SocialUser { RestUser = user3, LastActivityDate = DateTimeOffset.UtcNow };
            db.SocialUsers.Add(socialUser1);
            db.SocialUsers.Add(socialUser2);
            db.SocialUsers.Add(socialUser3);

            var sms1 = new RestSms { Date = DateTime.UtcNow, Code = 1111, RestAppUser = user1, UsedSms = UsedSms.Used };
            var sms2 = new RestSms { Date = DateTime.UtcNow, Code = 1111, RestAppUser = user2, UsedSms = UsedSms.Used };
            var sms3 = new RestSms { Date = DateTime.UtcNow, Code = 1111, RestAppUser = user3, UsedSms = UsedSms.Used };
            db.RestSmses.Add(sms1);
            db.RestSmses.Add(sms2);
            db.RestSmses.Add(sms3);

            var mainPhoto1 = new RestMainPhoto { DateCreate = DateTimeOffset.UtcNow, Photo = "photo1.jpg", RestPoint = RestPoint1 };
            var mainPhoto2 = new RestMainPhoto { DateCreate = DateTimeOffset.UtcNow, Photo = "photo2.jpg", RestPoint = RestPoint1 };
            var mainPhoto3 = new RestMainPhoto { DateCreate = DateTimeOffset.UtcNow, Photo = "photo3.jpg", RestPoint = RestPoint1 };
            db.RestMainPhotos.Add(mainPhoto1);
            db.RestMainPhotos.Add(mainPhoto2);
            db.RestMainPhotos.Add(mainPhoto3);


            var article = new RestArticle { Name = "Сладкий сюрприз к нашим любимым зимним праздникам", DateCreate = DateTimeOffset.UtcNow, DateStart = DateTimeOffset.UtcNow.AddDays(7), Description = "Сладкий сюрприз к нашим любимым зимним праздникам – горячий какао с маршмэллоу и рождественский пирог в выгодном предложении за 149 рублей. Зимние праздники ярче и вкуснее с Coffee Life! Они вернулись в меню! Хиты прошлых зим, какао с маршмэллоу и рождественский пирог снова радуют нашим гостей своими бесподобными вкусами и приятной ценой в нашем особенном зимнем предложении. Нежное, шоколадно-молочное какао с воздушными кусочками маршмэллоу создано специально для праздничного настроения. Горячий напиток для веселого отдыха!  Рождественский пирог уже стал любимым зимним десертом наших посетителей. Шоколадное сабле и сочная вишня с пряным ароматом корицы – лучшее угощение во время зимних праздников!", RestPoint = RestPoint1, Image = "article1.jpg"};
            var article2 = new RestArticle { Name = "Сладкий сюрприз к нашим любимым зимним праздникам 2", DateCreate = DateTimeOffset.UtcNow.AddDays(7), DateStart = DateTimeOffset.UtcNow.AddDays(15), Description = "Сладкий сюрприз к нашим любимым зимним праздникам – горячий какао с маршмэллоу и рождественский пирог в выгодном предложении за 149 рублей. Зимние праздники ярче и вкуснее с Coffee Life! Они вернулись в меню! Хиты прошлых зим, какао с маршмэллоу и рождественский пирог снова радуют нашим гостей своими бесподобными вкусами и приятной ценой в нашем особенном зимнем предложении. Нежное, шоколадно-молочное какао с воздушными кусочками маршмэллоу создано специально для праздничного настроения. Горячий напиток для веселого отдыха!  Рождественский пирог уже стал любимым зимним десертом наших посетителей. Шоколадное сабле и сочная вишня с пряным ароматом корицы – лучшее угощение во время зимних праздников!", RestPoint = RestPoint1, Image = "article2.jpg" };
            db.RestArticles.Add(article);
            db.RestArticles.Add(article2);

            var restAppSession1 = new RestAppSession { Id = Guid.NewGuid(), RestAppUser = user1, StartDate = DateTimeOffset.UtcNow, LastNewsView = DateTimeOffset.UtcNow, LastOccasionView = DateTimeOffset.UtcNow };
            var restAppSession2 = new RestAppSession { Id = Guid.NewGuid(), RestAppUser = user2, StartDate = DateTimeOffset.UtcNow, LastNewsView = DateTimeOffset.UtcNow, LastOccasionView = DateTimeOffset.UtcNow };
            var restAppSession3 = new RestAppSession { Id = Guid.NewGuid(), RestAppUser = user3, StartDate = DateTimeOffset.UtcNow, LastNewsView = DateTimeOffset.UtcNow, LastOccasionView = DateTimeOffset.UtcNow };
            db.RestAppSessions.Add(restAppSession1);
            db.RestAppSessions.Add(restAppSession2);
            db.RestAppSessions.Add(restAppSession3);

            var restPayment1 = new RestPayment { Bonus = 100, RestAppUser = user1 };
            var restPayment2 = new RestPayment { Bonus = 0, RestAppUser = user2 };
            var restPayment3 = new RestPayment { Bonus = 0, RestAppUser = user3 };
            db.RestPayments.Add(restPayment1);
            db.RestPayments.Add(restPayment2);
            db.RestPayments.Add(restPayment3);

            var gallery = new List<RestGallery> 
            { 
                new RestGallery { Image = "photo(1).jpg", RestPoint = RestPoint1, Description = "бла бла бла" },
                new RestGallery { Image = "photo(2).jpg", RestPoint = RestPoint1, Description = "бла бла бла" },
                new RestGallery { Image = "photo(3).jpg", RestPoint = RestPoint1, Description = "бла бла бла" },
                new RestGallery { Image = "image(1).jpg", RestPoint = RestPoint2, Description = "бла бла бла" },
                new RestGallery { Image = "image(2).jpg", RestPoint = RestPoint2, Description = "бла бла бла" },
                new RestGallery { Image = "image(3).jpg", RestPoint = RestPoint2, Description = "бла бла бла" },

            };

            gallery.ForEach(b => db.RestGallerys.Add(b));
            
            db.SaveChanges();
        }
    }
}