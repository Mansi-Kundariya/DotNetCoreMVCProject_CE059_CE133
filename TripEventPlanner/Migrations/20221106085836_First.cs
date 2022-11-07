using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripEventPlanner.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    activityType_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.activityType_id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    country_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    image_url = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    email = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    location_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    country_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.location_id);
                    table.ForeignKey(
                        name: "FK_location_country",
                        column: x => x.country_id,
                        principalTable: "Country",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    trip_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    country_id = table.Column<short>(type: "smallint", nullable: true),
                    user_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.trip_id);
                    table.ForeignKey(
                        name: "FK_Trip_Country",
                        column: x => x.country_id,
                        principalTable: "Country",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trip_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    activity_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    address = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true),
                    price = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    activityType_id = table.Column<short>(type: "smallint", nullable: true),
                    image_url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    location_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.activity_id);
                    table.ForeignKey(
                        name: "FK_activity_activityType",
                        column: x => x.activityType_id,
                        principalTable: "ActivityType",
                        principalColumn: "activityType_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Location",
                        column: x => x.location_id,
                        principalTable: "Location",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripActivity",
                columns: table => new
                {
                    trip_id = table.Column<short>(type: "smallint", nullable: false),
                    activity_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TripActivity_Activity",
                        column: x => x.activity_id,
                        principalTable: "Activity",
                        principalColumn: "activity_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripActivity_Trip",
                        column: x => x.trip_id,
                        principalTable: "Trip",
                        principalColumn: "trip_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_activityType_id",
                table: "Activity",
                column: "activityType_id");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_location_id",
                table: "Activity",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_Location_country_id",
                table: "Location",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_country_id",
                table: "Trip",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_user_id",
                table: "Trip",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_TripActivity_activity_id",
                table: "TripActivity",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "IX_TripActivity_trip_id",
                table: "TripActivity",
                column: "trip_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripActivity");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
