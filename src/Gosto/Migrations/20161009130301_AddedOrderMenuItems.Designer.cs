using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Gosto.Data;

namespace Gosto.Migrations
{
    [DbContext(typeof(MenuContext))]
    [Migration("20161009130301_AddedOrderMenuItems")]
    partial class AddedOrderMenuItems
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

            modelBuilder.Entity("Gosto.Models.OrderMenuItems", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<int>("MenuItemID");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.HasIndex("MenuItemID");

                    b.ToTable("OrderMenuItems");
                });

            modelBuilder.Entity("Gosto.Models.MenuItem", b =>
                {
                    b.HasOne("Gosto.Models.MenuSection")
                        .WithMany()
                        .HasForeignKey("MenuSectionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gosto.Models.OrderMenuItems", b =>
                {
                    b.HasOne("Gosto.Models.MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
