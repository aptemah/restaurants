using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Data.Entity;
using Intouch.Core;
using Microsoft.AspNet.Identity;

namespace Intouch.Core
{
    public class CoreContext : DbContext
    {
        public CoreContext() : base("DefaultConnection")
        {
            
        }

        #region Model Creating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetSchemaForWiFi(modelBuilder);
            SetSchemaForSocial(modelBuilder);
            SetSchemaForRestaurant(modelBuilder);
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RestOrder>()
                .HasOptional(m => m.RestBonusHistory)
                .WithRequired(p => p.RestOrder);


            modelBuilder.Entity<ChatFriend>()
                .HasOptional<SocialUser>(s => s.RequestUser)
                .WithMany(s => s.RequestFriends);

            modelBuilder.Entity<ChatFriend>()
                .HasOptional<SocialUser>(s => s.ConfirmUser)
                .WithMany(s => s.ConfirmFriends);
            modelBuilder.Entity<RestAppUser>()
                .HasOptional(m => m.RestPayment)
                .WithRequired(p => p.RestAppUser);
            modelBuilder.Entity<SocialUser>()
                .HasOptional(m => m.RestUser)
                .WithRequired(p => p.SocialUser);
            modelBuilder.Entity<RestOrder>()
                .HasOptional<RestAppUser>(s => s.RestAppUser)
                .WithMany(s => s.RestOrders);
            modelBuilder.Entity<RestOrder>()
                .HasOptional<RestAppUser>(s => s.Officiant)
                .WithMany(s => s.RestOrdersOfficiant);

        }

        private static void SetSchema(DbModelBuilder modelBuilder, IEnumerable<Type> entities, string schema)
        {
            foreach (var entity in entities)
            {
                // Call with reflection the following code:
                // modelBuilder.Entity<typeof(entity)>().ToTable(entity.Name, schema);
                var e = typeof(DbModelBuilder).GetMethod("Entity").MakeGenericMethod(entity).Invoke(modelBuilder, null);
                var toTable =
                    e.GetType().GetMethods().First(mi => mi.Name == "ToTable" && mi.GetParameters().Count() == 2);
                toTable.Invoke(e, new object[] { entity.Name, schema });
            }
        }
        #endregion

        #region WiFi

        public DbSet<Network> Networks { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<WifiSession> WiFiSessions { get; set; }
        public DbSet<WiFiVipAccess> WiFiVipAccesses { get; set; }

        private static void SetSchemaForWiFi(DbModelBuilder modelBuilder)
        {
            SetSchema(modelBuilder, new[]
            {
                typeof (Network),
                typeof (Point),
                typeof (Visit),
                typeof (WifiSession),
                typeof (WiFiVipAccess),
            }, "wifi");
        }

        #endregion

        #region Social
        public DbSet<SocialUser> SocialUsers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatMsgRecipient> ChatMsgRecipients { get; set; }
        public DbSet<Connections> Connections { get; set; }
        public DbSet<ChatFriend> Friends { get; set; }


        private static void SetSchemaForSocial(DbModelBuilder modelBuilder)
        {
            SetSchema(modelBuilder, new[]
            {
                typeof (Conversation),
                typeof (ChatParticipant),
                typeof (ChatMessage),
                typeof (ChatMsgRecipient),
                typeof (SocialUser),
                typeof (Connections),
                typeof (ChatFriend)
                
            }, "social");
        }

        #endregion

        #region Restaurant
        public DbSet<RestAdvertisement> RestAdvertisements { get; set; }
        public DbSet<RestAppSession> RestAppSessions { get; set; }
        public DbSet<RestAppUser> RestAppUsers { get; set; }
        public DbSet<RestArticle> RestArticles { get; set; }
        public DbSet<RestBonusHistory> RestBonusHistorys { get; set; }
        public DbSet<RestCategory> RestCategorys { get; set; }
        public DbSet<RestFeedback> RestFeedbacks { get; set; }
        public DbSet<RestGallery> RestGallerys { get; set; }
        public DbSet<RestMainPhoto> RestMainPhotos { get; set; }
        //public DbSet<RestMenu> RestMenus { get; set; }
        public DbSet<RestNetwork> RestNetworks { get; set; }
        public DbSet<RestNews> RestNewses { get; set; }
        public DbSet<RestOrder> RestOrders { get; set; }
        public DbSet<RestOrderPart> RestOrderParts { get; set; }
        public DbSet<RestOrderProduct> RestOrderProducts { get; set; }
        public DbSet<RestPayment> RestPayments { get; set; }
        public DbSet<RestPoint> RestPoints { get; set; }
        public DbSet<RestProdReview> RestProdReviews { get; set; }
        public DbSet<RestProduct> RestProducts { get; set; }
        public DbSet<RestProductFavorite> RestProductFavorites { get; set; }
        public DbSet<RestReservation> RestReservations { get; set; }
        public DbSet<RestReview> RestReviews { get; set; }
        public DbSet<RestSms> RestSmses { get; set; }

        private static void SetSchemaForRestaurant(DbModelBuilder modelBuilder)
        {
            SetSchema(modelBuilder, new[]
            {
                typeof (RestAdvertisement),
                typeof (RestAppSession),
                typeof (RestAppUser),
                typeof (RestArticle),
                typeof (RestBonusHistory),
                typeof (RestCategory),
                typeof (RestFeedback),
                typeof (RestGallery),
                typeof (RestMainPhoto),
                //typeof (RestMenu),
                typeof (RestNetwork),
                typeof (RestNetwork),
                typeof (RestNews),
                typeof (RestOrder),
                typeof (RestOrderPart),
                typeof (RestOrderProduct),
                typeof (RestPayment),
                typeof (RestPoint),
                typeof (RestProdReview),
                typeof (RestProduct),
                typeof (RestProductFavorite),
                typeof (RestReservation),
                typeof (RestReview),
                typeof (RestSms)
            }, "restaurant");
        }
        #endregion

        //public static void Initialize()
        //{
        //    bool dbExists;
        //    using (var db = new CoreContext())
        //    {
        //        dbExists = db.Database.Exists();
        //        Database.SetInitializer(new CoreContextSeedInitializer());
        //        if (!dbExists)
        //        {
        //            // Force db seed.
        //            db.Users.Any();
        //        }
        //    }
        //    if (!dbExists)
        //    {
        //        using (var db = new CoreContext())
        //        {
        //            db.IdentifyUsers(db.WiFiAdminUsers, new UserManager<WiFiAdminUser>(new WiFiAdminUserStore()));
        //            db.IdentifyUsers(db.PromoAdminUsers, new UserManager<PromoAdminUser>(new PromoAdminUserStore()));
        //            //db.IdentifyUsers(db.KarAdminUsers, new UserManager<KarAdminUser>(new KarAdminUserStore()));
        //            db.SaveChanges();
        //        }
        //    }
        //}

        private void IdentifyUsers<TUser>(IQueryable<TUser> users, UserManager<TUser> userManager) where TUser : class, IUser
        {
            foreach (var user in users)
            {
                userManager.Create(user, "123456");
            }
        }
    }
}

