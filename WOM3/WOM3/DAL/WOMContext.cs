using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WOM3.Models;

namespace WOM3.DAL
{
    public class WOMContext : DbContext
    {
        public WOMContext():base("DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Items> Items { get; set; }

        public DbSet<Heroes> Heroes { get; set; }

        public DbSet<EmailReg> EmailRegs { get; set; }

        public DbSet<HeroSpells> HeroSpells { get; set; }
        public DbSet<Spells> Spells { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<UserHeroes> UserHeroes { get; set; }

        public DbSet<UserItems> UserItems { get; set; }

        public DbSet<UserStats> UserStats { get; set; }

        public DbSet<ItemSpells> ItemSpells { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public DbSet<Messages> Messages { get; set; }

        public DbSet<FriendRequests> FriendRequests { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Friends>().HasKey(f => f.ID);
            modelBuilder.Entity<Friends>()
              .HasRequired(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UsernameId);
            modelBuilder.Entity<Friends>()
                .HasRequired(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Messages>().HasKey(x => x.id);
            modelBuilder.Entity<Messages>()
              .HasRequired(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UsernameId);
            modelBuilder.Entity<Messages>()
                .HasRequired(f => f.Receiver)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FriendRequests>().HasKey(f => f.ID);
            modelBuilder.Entity<FriendRequests>()
              .HasRequired(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UsernameId);
            modelBuilder.Entity<FriendRequests>()
                .HasRequired(f => f.FriendR)
                .WithMany()
                .HasForeignKey(f => f.FriendRId)
                .WillCascadeOnDelete(false);
        }

    }
}