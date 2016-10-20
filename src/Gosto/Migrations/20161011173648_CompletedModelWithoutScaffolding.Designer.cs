using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Gosto.Data;

namespace Gosto.Migrations
{
    [DbContext(typeof(MenuContext))]
    [Migration("20161011173648_CompletedModelWithoutScaffolding")]
    partial class CompletedModelWithoutScaffolding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gosto.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Phone");

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

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

            modelBuilder.Entity("Gosto.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerID");

                    b.Property<DateTime>("ReadyAt");

                    b.Property<int>("TakeAway");

                    b.Property<int>("TotalPrice");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Gosto.Models.OrderMenuItems", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<int>("MenuItemID");

                    b.Property<int?>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.HasIndex("MenuItemID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderMenuItems");
                });

            modelBuilder.Entity("Gosto.Models.Reservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<int?>("CustomerID");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("Gosto.Models.MenuItem", b =>
                {
                    b.HasOne("Gosto.Models.MenuSection")
                        .WithMany()
                        .HasForeignKey("MenuSectionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gosto.Models.Order", b =>
                {
                    b.HasOne("Gosto.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");
                });

            modelBuilder.Entity("Gosto.Models.OrderMenuItems", b =>
                {
                    b.HasOne("Gosto.Models.MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gosto.Models.Order")
                        .WithMany()
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("Gosto.Models.Reservation", b =>
                {
                    b.HasOne("Gosto.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");
                });
        }
    }
}
