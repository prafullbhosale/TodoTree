using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TodoTree.Data;

namespace TodoTree.Migrations
{
    [DbContext(typeof(TodoTreeContext))]
    partial class TodoTreeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoModels.TodoNode", b =>
                {
                    b.Property<int>("TodoNodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Description");

                    b.Property<bool>("IsRoot");

                    b.Property<DateTime>("LastModifiedTime");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("TodoNodeId");

                    b.ToTable("TodoNode");
                });

            modelBuilder.Entity("TodoModels.TodoNodeParentMap", b =>
                {
                    b.Property<int>("TodoNodeParentMapId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParentNodeId");

                    b.Property<int>("TodoNodeId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(-1);

                    b.HasKey("TodoNodeParentMapId");

                    b.HasIndex("ParentNodeId");

                    b.HasIndex("TodoNodeId");

                    b.ToTable("TodoNodeParentMap");
                });

            modelBuilder.Entity("TodoModels.TodoNodeParentMap", b =>
                {
                    b.HasOne("TodoModels.TodoNode", "ParentNode")
                        .WithMany()
                        .HasForeignKey("ParentNodeId");

                    b.HasOne("TodoModels.TodoNode", "TodoNode")
                        .WithMany("Children")
                        .HasForeignKey("TodoNodeId");
                });
        }
    }
}
