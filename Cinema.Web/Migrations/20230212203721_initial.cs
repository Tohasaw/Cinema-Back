using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Countries = table.Column<string>(type: "text", nullable: false),
                    Genres = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    AgeLimit = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    VideoLink = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    HexColor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceListRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceListId = table.Column<int>(type: "integer", nullable: false),
                    PriceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListRelation_Price_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceListRelation_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    PriceListId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableEntries_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableEntries_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    PriceId = table.Column<int>(type: "integer", nullable: false),
                    PriceListId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatPrice_Price_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatPrice_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatPrice_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableEntryId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    AdvertAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<int>(type: "integer", nullable: false),
                    RefundKey = table.Column<string>(type: "text", nullable: false),
                    PriceTotal = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_TableEntries_TableEntryId",
                        column: x => x.TableEntryId,
                        principalTable: "TableEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseId = table.Column<int>(type: "integer", nullable: false),
                    Cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    Visited = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "Column", "IsAvailable", "Row" },
                values: new object[,]
                {
                    { 1, 1, true, 1 },
                    { 2, 2, true, 1 },
                    { 3, 3, true, 1 },
                    { 4, 4, true, 1 },
                    { 5, 5, true, 1 },
                    { 6, 6, true, 1 },
                    { 7, 7, true, 1 },
                    { 8, 8, true, 1 },
                    { 9, 9, true, 1 },
                    { 10, 10, true, 1 },
                    { 11, 11, true, 1 },
                    { 12, 12, true, 1 },
                    { 13, 13, true, 1 },
                    { 14, 14, true, 1 },
                    { 15, 15, true, 1 },
                    { 16, 16, true, 1 },
                    { 17, 17, true, 1 },
                    { 18, 18, true, 1 },
                    { 19, 19, true, 1 },
                    { 20, 20, true, 1 },
                    { 21, 21, true, 1 },
                    { 22, 22, true, 1 },
                    { 23, 23, true, 1 },
                    { 24, 24, true, 1 },
                    { 25, 25, true, 1 },
                    { 26, 26, true, 1 },
                    { 27, 27, true, 1 },
                    { 28, 28, true, 1 },
                    { 29, 29, true, 1 },
                    { 30, 30, true, 1 },
                    { 31, 31, true, 1 },
                    { 32, 1, true, 2 },
                    { 33, 2, true, 2 },
                    { 34, 3, true, 2 },
                    { 35, 4, true, 2 },
                    { 36, 5, true, 2 },
                    { 37, 6, true, 2 },
                    { 38, 7, true, 2 },
                    { 39, 8, true, 2 },
                    { 40, 9, true, 2 },
                    { 41, 10, true, 2 },
                    { 42, 11, true, 2 },
                    { 43, 12, true, 2 },
                    { 44, 13, true, 2 },
                    { 45, 14, true, 2 },
                    { 46, 15, true, 2 },
                    { 47, 16, true, 2 },
                    { 48, 17, true, 2 },
                    { 49, 18, true, 2 },
                    { 50, 19, true, 2 },
                    { 51, 20, true, 2 },
                    { 52, 21, true, 2 },
                    { 53, 22, true, 2 },
                    { 54, 23, true, 2 },
                    { 55, 24, true, 2 },
                    { 56, 25, true, 2 },
                    { 57, 26, true, 2 },
                    { 58, 27, true, 2 },
                    { 59, 28, true, 2 },
                    { 60, 29, true, 2 },
                    { 61, 30, true, 2 },
                    { 62, 31, true, 2 },
                    { 63, 1, true, 3 },
                    { 64, 2, true, 3 },
                    { 65, 3, true, 3 },
                    { 66, 4, true, 3 },
                    { 67, 5, true, 3 },
                    { 68, 6, true, 3 },
                    { 69, 7, true, 3 },
                    { 70, 8, true, 3 },
                    { 71, 9, true, 3 },
                    { 72, 10, true, 3 },
                    { 73, 11, true, 3 },
                    { 74, 12, true, 3 },
                    { 75, 13, true, 3 },
                    { 76, 14, true, 3 },
                    { 77, 15, true, 3 },
                    { 78, 16, true, 3 },
                    { 79, 17, true, 3 },
                    { 80, 18, true, 3 },
                    { 81, 19, true, 3 },
                    { 82, 20, true, 3 },
                    { 83, 21, true, 3 },
                    { 84, 22, true, 3 },
                    { 85, 23, true, 3 },
                    { 86, 24, true, 3 },
                    { 87, 25, true, 3 },
                    { 88, 26, true, 3 },
                    { 89, 27, true, 3 },
                    { 90, 28, true, 3 },
                    { 91, 29, true, 3 },
                    { 92, 30, true, 3 },
                    { 93, 31, true, 3 },
                    { 94, 1, true, 4 },
                    { 95, 2, true, 4 },
                    { 96, 3, true, 4 },
                    { 97, 4, true, 4 },
                    { 98, 5, true, 4 },
                    { 99, 6, true, 4 },
                    { 100, 7, true, 4 },
                    { 101, 8, true, 4 },
                    { 102, 9, true, 4 },
                    { 103, 10, true, 4 },
                    { 104, 11, true, 4 },
                    { 105, 12, true, 4 },
                    { 106, 13, true, 4 },
                    { 107, 14, true, 4 },
                    { 108, 15, true, 4 },
                    { 109, 16, true, 4 },
                    { 110, 17, true, 4 },
                    { 111, 18, true, 4 },
                    { 112, 19, true, 4 },
                    { 113, 20, true, 4 },
                    { 114, 21, true, 4 },
                    { 115, 22, true, 4 },
                    { 116, 23, true, 4 },
                    { 117, 24, true, 4 },
                    { 118, 25, true, 4 },
                    { 119, 26, true, 4 },
                    { 120, 27, true, 4 },
                    { 121, 28, true, 4 },
                    { 122, 29, true, 4 },
                    { 123, 30, true, 4 },
                    { 124, 31, true, 4 },
                    { 125, 1, true, 5 },
                    { 126, 2, true, 5 },
                    { 127, 3, true, 5 },
                    { 128, 4, true, 5 },
                    { 129, 5, true, 5 },
                    { 130, 6, true, 5 },
                    { 131, 7, true, 5 },
                    { 132, 8, true, 5 },
                    { 133, 9, true, 5 },
                    { 134, 10, true, 5 },
                    { 135, 11, true, 5 },
                    { 136, 12, true, 5 },
                    { 137, 13, true, 5 },
                    { 138, 14, true, 5 },
                    { 139, 15, true, 5 },
                    { 140, 16, true, 5 },
                    { 141, 17, true, 5 },
                    { 142, 18, true, 5 },
                    { 143, 19, true, 5 },
                    { 144, 20, true, 5 },
                    { 145, 21, true, 5 },
                    { 146, 22, true, 5 },
                    { 147, 23, true, 5 },
                    { 148, 24, true, 5 },
                    { 149, 25, true, 5 },
                    { 150, 26, true, 5 },
                    { 151, 27, true, 5 },
                    { 152, 28, true, 5 },
                    { 153, 29, true, 5 },
                    { 154, 30, true, 5 },
                    { 155, 31, true, 5 },
                    { 156, 1, true, 6 },
                    { 157, 2, true, 6 },
                    { 158, 3, true, 6 },
                    { 159, 4, true, 6 },
                    { 160, 5, true, 6 },
                    { 161, 6, true, 6 },
                    { 162, 7, true, 6 },
                    { 163, 8, true, 6 },
                    { 164, 9, true, 6 },
                    { 165, 10, true, 6 },
                    { 166, 11, true, 6 },
                    { 167, 12, true, 6 },
                    { 168, 13, true, 6 },
                    { 169, 14, true, 6 },
                    { 170, 15, true, 6 },
                    { 171, 16, true, 6 },
                    { 172, 17, true, 6 },
                    { 173, 18, true, 6 },
                    { 174, 19, true, 6 },
                    { 175, 20, true, 6 },
                    { 176, 21, true, 6 },
                    { 177, 22, true, 6 },
                    { 178, 23, true, 6 },
                    { 179, 24, true, 6 },
                    { 180, 25, true, 6 },
                    { 181, 26, true, 6 },
                    { 182, 27, true, 6 },
                    { 183, 28, true, 6 },
                    { 184, 29, true, 6 },
                    { 185, 30, true, 6 },
                    { 186, 31, true, 6 },
                    { 187, 1, true, 7 },
                    { 188, 2, true, 7 },
                    { 189, 3, true, 7 },
                    { 190, 4, true, 7 },
                    { 191, 5, true, 7 },
                    { 192, 6, true, 7 },
                    { 193, 7, true, 7 },
                    { 194, 8, true, 7 },
                    { 195, 9, true, 7 },
                    { 196, 10, true, 7 },
                    { 197, 11, true, 7 },
                    { 198, 12, true, 7 },
                    { 199, 13, true, 7 },
                    { 200, 14, true, 7 },
                    { 201, 15, true, 7 },
                    { 202, 16, true, 7 },
                    { 203, 17, true, 7 },
                    { 204, 18, true, 7 },
                    { 205, 19, true, 7 },
                    { 206, 20, true, 7 },
                    { 207, 21, true, 7 },
                    { 208, 22, true, 7 },
                    { 209, 23, true, 7 },
                    { 210, 24, true, 7 },
                    { 211, 25, true, 7 },
                    { 212, 26, true, 7 },
                    { 213, 27, true, 7 },
                    { 214, 28, true, 7 },
                    { 215, 29, true, 7 },
                    { 216, 30, true, 7 },
                    { 217, 31, true, 7 },
                    { 218, 1, true, 8 },
                    { 219, 2, true, 8 },
                    { 220, 3, true, 8 },
                    { 221, 4, true, 8 },
                    { 222, 5, true, 8 },
                    { 223, 6, true, 8 },
                    { 224, 7, true, 8 },
                    { 225, 8, true, 8 },
                    { 226, 9, true, 8 },
                    { 227, 10, true, 8 },
                    { 228, 11, true, 8 },
                    { 229, 12, true, 8 },
                    { 230, 13, true, 8 },
                    { 231, 14, true, 8 },
                    { 232, 15, true, 8 },
                    { 233, 16, true, 8 },
                    { 234, 17, true, 8 },
                    { 235, 18, true, 8 },
                    { 236, 19, true, 8 },
                    { 237, 20, true, 8 },
                    { 238, 21, true, 8 },
                    { 239, 22, true, 8 },
                    { 240, 23, true, 8 },
                    { 241, 24, true, 8 },
                    { 242, 25, true, 8 },
                    { 243, 26, true, 8 },
                    { 244, 27, true, 8 },
                    { 245, 28, true, 8 },
                    { 246, 29, true, 8 },
                    { 247, 30, true, 8 },
                    { 248, 31, true, 8 },
                    { 249, 1, true, 9 },
                    { 250, 2, true, 9 },
                    { 251, 3, true, 9 },
                    { 252, 4, true, 9 },
                    { 253, 5, true, 9 },
                    { 254, 6, true, 9 },
                    { 255, 7, true, 9 },
                    { 256, 8, true, 9 },
                    { 257, 9, true, 9 },
                    { 258, 10, true, 9 },
                    { 259, 11, true, 9 },
                    { 260, 12, true, 9 },
                    { 261, 13, true, 9 },
                    { 262, 14, true, 9 },
                    { 263, 15, true, 9 },
                    { 264, 16, true, 9 },
                    { 265, 17, true, 9 },
                    { 266, 18, true, 9 },
                    { 267, 19, true, 9 },
                    { 268, 20, true, 9 },
                    { 269, 21, true, 9 },
                    { 270, 22, true, 9 },
                    { 271, 23, true, 9 },
                    { 272, 1, true, 10 },
                    { 273, 2, true, 10 },
                    { 274, 3, true, 10 },
                    { 275, 4, true, 10 },
                    { 276, 5, true, 10 },
                    { 277, 6, true, 10 },
                    { 278, 7, true, 10 },
                    { 279, 8, true, 10 },
                    { 280, 9, true, 10 },
                    { 281, 10, true, 10 },
                    { 282, 11, true, 10 },
                    { 283, 12, true, 10 },
                    { 284, 13, true, 10 },
                    { 285, 14, true, 10 },
                    { 286, 15, true, 10 },
                    { 287, 16, true, 10 },
                    { 288, 17, true, 10 },
                    { 289, 18, true, 10 },
                    { 290, 19, true, 10 },
                    { 291, 20, true, 10 },
                    { 292, 21, true, 10 },
                    { 293, 22, true, 10 },
                    { 294, 23, true, 10 },
                    { 295, 1, true, 11 },
                    { 296, 2, true, 11 },
                    { 297, 3, true, 11 },
                    { 298, 4, true, 11 },
                    { 299, 5, true, 11 },
                    { 300, 6, true, 11 },
                    { 301, 7, true, 11 },
                    { 302, 8, true, 11 },
                    { 303, 9, true, 11 },
                    { 304, 10, true, 11 },
                    { 305, 11, true, 11 },
                    { 306, 12, true, 11 },
                    { 307, 13, true, 11 },
                    { 308, 14, true, 11 },
                    { 309, 15, true, 11 },
                    { 310, 16, true, 11 },
                    { 311, 17, true, 11 },
                    { 312, 18, true, 11 },
                    { 313, 19, true, 11 },
                    { 314, 20, true, 11 },
                    { 315, 21, true, 11 },
                    { 316, 22, true, 11 },
                    { 317, 23, true, 11 },
                    { 318, 1, true, 12 },
                    { 319, 2, true, 12 },
                    { 320, 3, true, 12 },
                    { 321, 4, true, 12 },
                    { 322, 5, true, 12 },
                    { 323, 6, true, 12 },
                    { 324, 7, true, 12 },
                    { 325, 8, true, 12 },
                    { 326, 9, true, 12 },
                    { 327, 10, true, 12 },
                    { 328, 11, true, 12 },
                    { 329, 12, true, 12 },
                    { 330, 13, true, 12 },
                    { 331, 14, true, 12 },
                    { 332, 15, true, 12 },
                    { 333, 16, true, 12 },
                    { 334, 17, true, 12 },
                    { 335, 18, true, 12 },
                    { 336, 19, true, 12 },
                    { 337, 20, true, 12 },
                    { 338, 21, true, 12 },
                    { 339, 22, true, 12 },
                    { 340, 23, true, 12 },
                    { 341, 1, true, 13 },
                    { 342, 2, true, 13 },
                    { 343, 3, true, 13 },
                    { 344, 4, true, 13 },
                    { 345, 5, true, 13 },
                    { 346, 6, true, 13 },
                    { 347, 7, true, 13 },
                    { 348, 8, true, 13 },
                    { 349, 9, true, 13 },
                    { 350, 10, true, 13 },
                    { 351, 11, true, 13 },
                    { 352, 12, true, 13 },
                    { 353, 13, true, 13 },
                    { 354, 14, true, 13 },
                    { 355, 15, true, 13 },
                    { 356, 16, true, 13 },
                    { 357, 17, true, 13 },
                    { 358, 18, true, 13 },
                    { 359, 19, true, 13 },
                    { 360, 20, true, 13 },
                    { 361, 21, true, 13 },
                    { 362, 22, true, 13 },
                    { 363, 23, true, 13 },
                    { 364, 1, true, 14 },
                    { 365, 2, true, 14 },
                    { 366, 3, true, 14 },
                    { 367, 4, true, 14 },
                    { 368, 5, true, 14 },
                    { 369, 6, true, 14 },
                    { 370, 7, true, 14 },
                    { 371, 8, true, 14 },
                    { 372, 9, true, 14 },
                    { 373, 10, true, 14 },
                    { 374, 11, true, 14 },
                    { 375, 12, true, 14 },
                    { 376, 13, true, 14 },
                    { 377, 14, true, 14 },
                    { 378, 15, true, 14 },
                    { 379, 16, true, 14 },
                    { 380, 17, true, 14 },
                    { 381, 18, true, 14 },
                    { 382, 19, true, 14 },
                    { 383, 20, true, 14 },
                    { 384, 21, true, 14 },
                    { 385, 22, true, 14 },
                    { 386, 23, true, 14 },
                    { 387, 1, true, 15 },
                    { 388, 2, true, 15 },
                    { 389, 3, true, 15 },
                    { 390, 4, true, 15 },
                    { 391, 5, true, 15 },
                    { 392, 6, true, 15 },
                    { 393, 7, true, 15 },
                    { 394, 8, true, 15 },
                    { 395, 9, true, 15 },
                    { 396, 10, true, 15 },
                    { 397, 11, true, 15 },
                    { 398, 12, true, 15 },
                    { 399, 13, true, 15 },
                    { 400, 14, true, 15 },
                    { 401, 15, true, 15 },
                    { 402, 16, true, 15 },
                    { 403, 17, true, 15 },
                    { 404, 18, true, 15 },
                    { 405, 19, true, 15 },
                    { 406, 20, true, 15 },
                    { 407, 21, true, 15 },
                    { 408, 22, true, 15 },
                    { 409, 23, true, 15 },
                    { 410, 1, true, 16 },
                    { 411, 2, true, 16 },
                    { 412, 3, true, 16 },
                    { 413, 4, true, 16 },
                    { 414, 5, true, 16 },
                    { 415, 6, true, 16 },
                    { 416, 7, true, 16 },
                    { 417, 8, true, 16 },
                    { 418, 9, true, 16 },
                    { 419, 10, true, 16 },
                    { 420, 11, true, 16 },
                    { 421, 12, true, 16 },
                    { 422, 13, true, 16 },
                    { 423, 14, true, 16 },
                    { 424, 15, true, 16 },
                    { 425, 16, true, 16 },
                    { 426, 17, true, 16 },
                    { 427, 18, true, 16 },
                    { 428, 19, true, 16 },
                    { 429, 20, true, 16 },
                    { 430, 21, true, 16 },
                    { 431, 22, true, 16 },
                    { 432, 23, true, 16 },
                    { 433, 1, true, 17 },
                    { 434, 2, true, 17 },
                    { 435, 3, true, 17 },
                    { 436, 4, true, 17 },
                    { 437, 5, true, 17 },
                    { 438, 6, true, 17 },
                    { 439, 7, true, 17 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "RoleId" },
                values: new object[] { 1, "admin", "admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListRelation_PriceId",
                table: "PriceListRelation",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListRelation_PriceListId",
                table: "PriceListRelation",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_TableEntryId",
                table: "Purchase",
                column: "TableEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPrice_PriceId",
                table: "SeatPrice",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPrice_PriceListId",
                table: "SeatPrice",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPrice_SeatId",
                table: "SeatPrice",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_TableEntries_MovieId",
                table: "TableEntries",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_TableEntries_PriceListId",
                table: "TableEntries",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PurchaseId",
                table: "Ticket",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListRelation");

            migrationBuilder.DropTable(
                name: "SeatPrice");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "TableEntries");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "PriceList");
        }
    }
}
