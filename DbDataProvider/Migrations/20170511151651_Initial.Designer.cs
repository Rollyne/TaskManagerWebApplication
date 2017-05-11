using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DbDataProvider;

namespace DbDataProvider.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    [Migration("20170511151651_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DbDataProvider.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<int>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DbDataProvider.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<int>("ExecutitiveId");

                    b.Property<string>("Header");

                    b.Property<bool>("IsCompleted");

                    b.Property<DateTime>("LastEditedOn");

                    b.Property<int>("RequiredHours");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ExecutitiveId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DbDataProvider.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DbDataProvider.Entities.Comment", b =>
                {
                    b.HasOne("DbDataProvider.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DbDataProvider.Entities.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DbDataProvider.Entities.Task", b =>
                {
                    b.HasOne("DbDataProvider.Entities.User", "Creator")
                        .WithMany("TasksCreated")
                        .HasForeignKey("CreatorId");

                    b.HasOne("DbDataProvider.Entities.User", "Executitive")
                        .WithMany("TasksToDo")
                        .HasForeignKey("ExecutitiveId");
                });
        }
    }
}
