using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "exercises");

        migrationBuilder.CreateTable(
            name: "clients",
            schema: "exercises",
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
                table.PrimaryKey("pk_clients", x => x.table_id);
            });

        migrationBuilder.CreateTable(
            name: "equipment",
            schema: "exercises",
            columns: table => new
            {
                table_id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_equipment", x => x.table_id);
            });

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "exercises",
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
            schema: "exercises",
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
            name: "muscle_groups",
            schema: "exercises",
            columns: table => new
            {
                table_id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_muscle_groups", x => x.table_id);
            });

        migrationBuilder.CreateTable(
            name: "outbox_message_consumers",
            schema: "exercises",
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
            schema: "exercises",
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
            name: "exercises",
            schema: "exercises",
            columns: table => new
            {
                table_id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                difficulty = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                equipment_table_id = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_exercises", x => x.table_id);
                table.ForeignKey(
                    name: "fk_exercises_equipment_equipment_table_id",
                    column: x => x.equipment_table_id,
                    principalSchema: "exercises",
                    principalTable: "equipment",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "exercise_media",
            schema: "exercises",
            columns: table => new
            {
                exercise_table_id = table.Column<long>(type: "bigint", nullable: false),
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_exercise_media", x => new { x.exercise_table_id, x.id });
                table.ForeignKey(
                    name: "fk_exercise_media_exercises_exercise_table_id",
                    column: x => x.exercise_table_id,
                    principalSchema: "exercises",
                    principalTable: "exercises",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "exercise_muscle_groups",
            schema: "exercises",
            columns: table => new
            {
                table_id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                exercise_id = table.Column<long>(type: "bigint", nullable: false),
                muscle_group_id = table.Column<long>(type: "bigint", nullable: false),
                muscle_group_type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_exercise_muscle_groups", x => x.table_id);
                table.ForeignKey(
                    name: "fk_exercise_muscle_groups_exercises_exercise_id",
                    column: x => x.exercise_id,
                    principalSchema: "exercises",
                    principalTable: "exercises",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_exercise_muscle_groups_muscle_groups_muscle_group_id",
                    column: x => x.muscle_group_id,
                    principalSchema: "exercises",
                    principalTable: "muscle_groups",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_clients_id",
            schema: "exercises",
            table: "clients",
            column: "id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_equipment_id",
            schema: "exercises",
            table: "equipment",
            column: "id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_exercise_muscle_groups_exercise_id",
            schema: "exercises",
            table: "exercise_muscle_groups",
            column: "exercise_id");

        migrationBuilder.CreateIndex(
            name: "ix_exercise_muscle_groups_id",
            schema: "exercises",
            table: "exercise_muscle_groups",
            column: "id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_exercise_muscle_groups_muscle_group_id",
            schema: "exercises",
            table: "exercise_muscle_groups",
            column: "muscle_group_id");

        migrationBuilder.CreateIndex(
            name: "ix_exercises_equipment_table_id",
            schema: "exercises",
            table: "exercises",
            column: "equipment_table_id");

        migrationBuilder.CreateIndex(
            name: "ix_exercises_id",
            schema: "exercises",
            table: "exercises",
            column: "id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_muscle_groups_id",
            schema: "exercises",
            table: "muscle_groups",
            column: "id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "clients",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "exercise_media",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "exercise_muscle_groups",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "exercises",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "muscle_groups",
            schema: "exercises");

        migrationBuilder.DropTable(
            name: "equipment",
            schema: "exercises");
    }
}
