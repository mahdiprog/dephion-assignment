using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationService.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkingStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkingEndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceTypes",
                columns: table => new
                {
                    ResourceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTypes", x => x.ResourceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResourceTypeId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceTypes_ResourceTypeId",
                        column: x => x.ResourceTypeId,
                        principalTable: "ResourceTypes",
                        principalColumn: "ResourceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "OfficeId", "Location", "WorkingEndTime", "WorkingStartTime" },
                values: new object[,]
                {
                    { 1, "Amsterdam", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 30, 0, 0) },
                    { 2, "Berlin", new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 30, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "ResourceTypes",
                columns: new[] { "ResourceTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Presentation Device" },
                    { 2, "Audio Device" },
                    { 3, "Board" }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "Discriminator", "Name", "OfficeId", "ResourceTypeId" },
                values: new object[,]
                {
                    { 8, "MovableResource", "TV", 1, 1 },
                    { 9, "MovableResource", "Beamer", 1, 1 },
                    { 10, "MovableResource", "TV 1", 2, 1 },
                    { 11, "MovableResource", "TV 2", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Capacity", "Name", "OfficeId" },
                values: new object[,]
                {
                    { 1, 10, "Meeting Room 1", 1 },
                    { 2, 15, "Meeting Room 2", 1 },
                    { 3, 18, "Meeting Room 1", 2 },
                    { 4, 20, "Meeting Room 2", 2 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "Discriminator", "Name", "ResourceTypeId", "RoomId" },
                values: new object[,]
                {
                    { 1, "SteadyResource", "White board", 3, 1 },
                    { 2, "SteadyResource", "White board", 3, 2 },
                    { 3, "SteadyResource", "Beamer", 1, 2 },
                    { 4, "SteadyResource", "White board", 3, 3 },
                    { 5, "SteadyResource", "White board", 3, 4 },
                    { 6, "SteadyResource", "TV", 1, 4 },
                    { 7, "SteadyResource", "Speaker", 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_OfficeId",
                table: "Resources",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_RoomId",
                table: "Resources",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OfficeId",
                table: "Rooms",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "ResourceTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
