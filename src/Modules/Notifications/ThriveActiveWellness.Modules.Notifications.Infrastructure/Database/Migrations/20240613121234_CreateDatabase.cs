using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "notifications");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "notifications",
            columns: table => new
            {
                inbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_message_consumers", x => new { x.inbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "inbox_messages",
            schema: "notifications",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "outbox_message_consumers",
            schema: "notifications",
            columns: table => new
            {
                outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "notifications",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "users",
            schema: "notifications",
            columns: table => new
            {
                table_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("ak_users_table_id", x => x.table_id);
                table.UniqueConstraint("pk_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "notification_preferences",
            schema: "notifications",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_table_id = table.Column<int>(type: "integer", nullable: false),
                type = table.Column<int>(type: "integer", nullable: false),
                enabled = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_notification_preferences", x => x.id);
                table.ForeignKey(
                    name: "fk_notification_preferences_users_user_table_id",
                    column: x => x.user_table_id,
                    principalSchema: "notifications",
                    principalTable: "users",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "notifications",
            schema: "notifications",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<int>(type: "integer", nullable: false),
                template_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_notifications", x => x.id);
                table.ForeignKey(
                    name: "fk_notifications_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "notifications",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_notification_preferences_user_table_id_type",
            schema: "notifications",
            table: "notification_preferences",
            columns: new[] { "user_table_id", "type" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_notifications_user_id",
            schema: "notifications",
            table: "notifications",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_id",
            schema: "notifications",
            table: "users",
            column: "id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "notification_preferences",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "notifications",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "notifications");

        migrationBuilder.DropTable(
            name: "users",
            schema: "notifications");
    }
}
