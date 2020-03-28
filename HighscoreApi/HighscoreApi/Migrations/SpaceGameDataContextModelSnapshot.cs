﻿// <auto-generated />
using HighscoreApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HighscoreApi.Migrations
{
    [DbContext(typeof(SpaceGameDataContext))]
    partial class SpaceGameDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HighscoreLogic.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            PName = "BRU",
                            Score = 17383
                        },
                        new
                        {
                            PlayerId = 2,
                            PName = "BRO",
                            Score = 1738
                        },
                        new
                        {
                            PlayerId = 3,
                            PName = "BRA",
                            Score = 1783
                        },
                        new
                        {
                            PlayerId = 4,
                            PName = "BRE",
                            Score = 1383
                        },
                        new
                        {
                            PlayerId = 5,
                            PName = "BRI",
                            Score = 383
                        },
                        new
                        {
                            PlayerId = 6,
                            PName = "COK",
                            Score = 7383
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
