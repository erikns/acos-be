using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ACOSbe.Migrations
{
    public partial class AddTaskType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.Id);
                    table.UniqueConstraint("AK_TaskType_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_TypeId",
                table: "Task",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskType_TypeId",
                table: "Task",
                column: "TypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskType_TypeId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "TaskType");

            migrationBuilder.DropIndex(
                name: "IX_Task_TypeId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Task");
        }
    }
}
