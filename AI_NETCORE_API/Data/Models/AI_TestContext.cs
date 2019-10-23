﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class AI_TestContext : DbContext
    {
        public AI_TestContext()
        {
        }

        public AI_TestContext(DbContextOptions<AI_TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuyOffers> BuyOffers { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Configurations> Configurations { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<SellOffers> SellOffers { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AI_Test;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOffers>(entity =>
            {
                entity.ToTable("Buy_Offers");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .IsRowVersion();

                entity.Property(e => e.IsValid).HasColumnName("is_valid");

                entity.Property(e => e.MaxPrice)
                    .HasColumnName("max_price")
                    .HasColumnType("numeric(10, 4)");

                entity.Property(e => e.ResourceId).HasColumnName("resource_id");

                entity.Property(e => e.StartAmount).HasColumnName("start_amount");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.BuyOffers)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Buy_Offer__resou__31EC6D26");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configurations>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Resources>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CompId).HasColumnName("comp_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Resources__comp___300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Resources__user___2F10007B");
            });

            modelBuilder.Entity<SellOffers>(entity =>
            {
                entity.ToTable("Sell_Offers");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .IsRowVersion();

                entity.Property(e => e.IsValid).HasColumnName("is_valid");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10, 4)");

                entity.Property(e => e.ResourceId).HasColumnName("resource_id");

                entity.Property(e => e.StartAmount).HasColumnName("start_amount");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.SellOffers)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sell_Offe__resou__30F848ED");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BuyOfferId).HasColumnName("buy_offer_id");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .IsRowVersion();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10, 4)");

                entity.Property(e => e.SellOfferId).HasColumnName("sell_offer_id");

                entity.HasOne(d => d.BuyOffer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BuyOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__buy_o__33D4B598");

                entity.HasOne(d => d.SellOffer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SellOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__sell___32E0915F");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cash)
                    .HasColumnName("cash")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });
        }
    }
}
