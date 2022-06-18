﻿using GenshinCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace GenshinCalculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<CharacterRegion> CharacterRegions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CharacterRegion>()
                .HasKey(x => new { x.CharacterId, x.RegionId });
        }
    }
}
