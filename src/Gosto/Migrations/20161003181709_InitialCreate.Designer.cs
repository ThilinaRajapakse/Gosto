using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Gosto.Data;

namespace Gosto.Migrations
{
    [DbContext(typeof(MenuContext))]
    [Migration("20161003181709_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gosto.Models.MenuItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MenuSectionID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Price");

                    b.HasKey("ID");

                    b.HasIndex("MenuSectionID");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Gosto.Models.MenuSection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("MenuSections");
                });

            modelBuilder.Entity("Gosto.Models.MenuItem", b =>
                {
                    b.HasOne("Gosto.Models.MenuSection")
                        .WithMany()
                        .HasForeignKey("MenuSectionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
