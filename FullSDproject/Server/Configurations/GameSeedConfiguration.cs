using FullSDproject.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDproject.Server.Configurations.Entities
{
    public class GameSeedConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Game> builder)
        {
            builder.HasData(
                 new Game
                 {
                     Id = 1,
                     Title = "Cheese The Game",
                     Price = 2.00f,
                     Requirements = "Intel Core i5 or higher, NVidia GTX 1650, 8GB of RAM, 10GB of free disk space",
                     Rating = "PG",
                     Genre = "Puzzle",
                     Developer = "Cheesy Studios",
                     Publisher = "Milk Games",
                     Thumbnail = "chez.png",
                     ReleaseDate = new DateTime(2020, 6, 9),
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now,
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 },
                 new Game
                 {
                     Id = 2,
                     Title = "Cheese The Game 2",
                     Price = 3.00f,
                     Requirements = "Intel Core i7 or higher, NVidia RTX 2060, 8GB of RAM, 10GB of free disk space",
                     Rating = "PG",
                     Genre = "Puzzle",
                     Developer = "Cheesy Studios",
                     Publisher = "Milk Games",
                     Thumbnail = "chez.png",
                     ReleaseDate = new DateTime(2021, 4, 20),
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now,
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 }
                );
        }
    }
}

