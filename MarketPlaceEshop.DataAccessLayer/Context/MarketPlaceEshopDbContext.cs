using System;
using System.Collections.Generic;
using System.Text;
using MarketPlaceEshop.DataAccessLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using MarketPlaceEshop.DataAccessLayer.Entities.Contact;

namespace MarketPlaceEshop.DataAccessLayer.Context
{
    public class MarketPlaceEshopDbContext : DbContext
    {
        public MarketPlaceEshopDbContext(DbContextOptions<MarketPlaceEshopDbContext> options) : base(options)
        {

        }

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region All DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SiteBanner> SiteBanners { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        #endregion
    }
}
