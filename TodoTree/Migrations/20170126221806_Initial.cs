using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TodoTree.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoNode",
                columns: table => new
                {
                    TodoNodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsRoot = table.Column<bool>(nullable: false),
                    LastModifiedTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoNode", x => x.TodoNodeId);
                });

            migrationBuilder.CreateTable(
                name: "TodoNodeParentMap",
                columns: table => new
                {
                    TodoNodeParentMapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentNodeId = table.Column<int>(nullable: false),
                    TodoNodeId = table.Column<int>(nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoNodeParentMap", x => x.TodoNodeParentMapId);
                    table.ForeignKey(
                        name: "FK_TodoNodeParentMap_TodoNode_ParentNodeId",
                        column: x => x.ParentNodeId,
                        principalTable: "TodoNode",
                        principalColumn: "TodoNodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TodoNodeParentMap_TodoNode_TodoNodeId",
                        column: x => x.TodoNodeId,
                        principalTable: "TodoNode",
                        principalColumn: "TodoNodeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoNodeParentMap_ParentNodeId",
                table: "TodoNodeParentMap",
                column: "ParentNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoNodeParentMap_TodoNodeId",
                table: "TodoNodeParentMap",
                column: "TodoNodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoNodeParentMap");

            migrationBuilder.DropTable(
                name: "TodoNode");
        }
    }
}
