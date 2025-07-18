using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Latest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_systems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    short_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_systems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "server_blocks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    app_system_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_server_blocks", x => x.id);
                    table.ForeignKey(
                        name: "fk_server_blocks_app_systems_app_system_id",
                        column: x => x.app_system_id,
                        principalTable: "app_systems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_app_system",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    app_system_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_app_system", x => new { x.user_id, x.app_system_id });
                    table.ForeignKey(
                        name: "fk_user_app_system_app_systems_app_system_id",
                        column: x => x.app_system_id,
                        principalTable: "app_systems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_app_system_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ip_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    host_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<byte[]>(type: "bytea", nullable: false),
                    server_block_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servers", x => x.id);
                    table.ForeignKey(
                        name: "fk_servers_server_blocks_server_block_id",
                        column: x => x.server_block_id,
                        principalTable: "server_blocks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_app_systems_name",
                table: "app_systems",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_server_blocks_app_system_id",
                table: "server_blocks",
                column: "app_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_server_blocks_name_app_system_id",
                table: "server_blocks",
                columns: ["name", "app_system_id"],
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_servers_ip_address_server_block_id",
                table: "servers",
                columns: ["ip_address", "server_block_id"]);

            migrationBuilder.CreateIndex(
                name: "ix_servers_server_block_id",
                table: "servers",
                column: "server_block_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_app_system_app_system_id",
                table: "user_app_system",
                column: "app_system_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servers");

            migrationBuilder.DropTable(
                name: "user_app_system");

            migrationBuilder.DropTable(
                name: "server_blocks");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "app_systems");
        }
    }
}
