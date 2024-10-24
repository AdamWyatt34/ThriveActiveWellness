using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThriveActiveWellness.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "users",
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
            schema: "users",
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
            schema: "users",
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
            schema: "users",
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
            name: "parq_questions",
            schema: "users",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                question = table.Column<string>(type: "text", nullable: false),
                parent_question_id = table.Column<int>(type: "integer", nullable: true),
                condition_type = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_parq_questions", x => x.id);
                table.ForeignKey(
                    name: "fk_parq_questions_parq_questions_parent_question_id",
                    column: x => x.parent_question_id,
                    principalSchema: "users",
                    principalTable: "parq_questions",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "permissions",
            schema: "users",
            columns: table => new
            {
                code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_permissions", x => x.code);
            });

        migrationBuilder.CreateTable(
            name: "roles",
            schema: "users",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "users",
            schema: "users",
            columns: table => new
            {
                table_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                identity_id = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.table_id);
                table.UniqueConstraint("ak_users_id", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "parq_responses",
            schema: "users",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                question_id = table.Column<int>(type: "integer", nullable: false),
                response = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                response_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_parq_responses", x => x.id);
                table.ForeignKey(
                    name: "fk_parq_responses_parq_questions_question_id",
                    column: x => x.question_id,
                    principalSchema: "users",
                    principalTable: "parq_questions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "role_permissions",
            schema: "users",
            columns: table => new
            {
                permission_code = table.Column<string>(type: "character varying(100)", nullable: false),
                role_name = table.Column<string>(type: "character varying(50)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_permissions", x => new { x.permission_code, x.role_name });
                table.ForeignKey(
                    name: "fk_role_permissions_permissions_permission_code",
                    column: x => x.permission_code,
                    principalSchema: "users",
                    principalTable: "permissions",
                    principalColumn: "code",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_permissions_roles_role_name",
                    column: x => x.role_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "parq_completions",
            schema: "users",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                completion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                pdf_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_parq_completions", x => x.id);
                table.ForeignKey(
                    name: "fk_parq_completions_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "user_fitness_profiles",
            schema: "users",
            columns: table => new
            {
                table_id = table.Column<int>(type: "integer", nullable: false),
                fitness_goals = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                fitness_level = table.Column<int>(type: "integer", nullable: false),
                health_information = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                dietary_preferences = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_fitness_profiles", x => x.table_id);
                table.ForeignKey(
                    name: "fk_user_fitness_profiles_users_table_id",
                    column: x => x.table_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "user_parQs",
            schema: "users",
            columns: table => new
            {
                table_id = table.Column<int>(type: "integer", nullable: false),
                responses = table.Column<string>(type: "text", nullable: false),
                date_completed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp at time zone 'utc'")
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_par_qs", x => x.table_id);
                table.ForeignKey(
                    name: "fk_user_par_qs_users_table_id",
                    column: x => x.table_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "user_roles",
            schema: "users",
            columns: table => new
            {
                role_name = table.Column<string>(type: "character varying(50)", nullable: false),
                user_table_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_roles", x => new { x.role_name, x.user_table_id });
                table.ForeignKey(
                    name: "fk_user_roles_roles_roles_name",
                    column: x => x.role_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_user_roles_users_user_table_id",
                    column: x => x.user_table_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "table_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "parq_questions",
            columns: new[] { "id", "condition_type", "parent_question_id", "question" },
            values: new object[,]
            {
                { 1, null, null, "Has your doctor ever said that you have a heart condition OR high blood pressure?" },
                { 2, null, null, "Do you feel pain in your chest at rest, during your daily activities of living, OR when you do physical activity?" },
                { 3, null, null, "Do you lose balance because of dizziness OR have you lost consciousness in the last 12 months?" },
                { 4, null, null, "Have you ever been diagnosed with another chronic medical condition (other than heart disease or high blood pressure)?" },
                { 5, null, null, "Are you currently taking prescribed medications for a chronic medical condition?" },
                { 6, null, null, "Do you currently have (or have had within the past 12 months) a bone, joint, or soft tissue problem that could be made worse by becoming more physically active?" },
                { 7, null, null, "Has your doctor ever said that you should only do medically supervised physical activity?" }
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "permissions",
            column: "code",
            values: new object[]
            {
                "equipment:create",
                "equipment:delete",
                "equipment:read",
                "equipment:search",
                "equipment:update",
                "users:modify",
                "users:modify:current",
                "users:read"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "roles",
            column: "name",
            values: new object[]
            {
                "Administrator",
                "Client",
                "MealService",
                "Trainer"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "parq_questions",
            columns: new[] { "id", "condition_type", "parent_question_id", "question" },
            values: new object[,]
            {
                { 8, 1, 4, "Do you have Arthritis, Osteoporosis, or Back Problems?" },
                { 12, 1, 4, "Do you currently have Cancer of any kind?" },
                { 15, 1, 4, "Do you have a Heart or Cardiovascular Condition?" },
                { 20, 1, 4, "Do you have High Blood Pressure?" },
                { 23, 1, 4, "Do you have any Metabolic Conditions?" },
                { 29, 1, 4, "Do you have any Mental Health Problems or Learning Difficulties?" },
                { 32, 1, 4, "Do you have a Respiratory Disease?" },
                { 37, 1, 4, "Do you have a Spinal Cord Injury?" },
                { 41, 1, 4, "Have you had a Stroke?" },
                { 45, 1, 4, "Do you have any other medical condition not listed above?" }
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: new[] { "permission_code", "role_name" },
            values: new object[,]
            {
                { "equipment:create", "Administrator" },
                { "equipment:delete", "Administrator" },
                { "equipment:read", "Administrator" },
                { "equipment:read", "Client" },
                { "equipment:search", "Client" },
                { "equipment:update", "Administrator" },
                { "users:modify", "Administrator" },
                { "users:modify:current", "Administrator" },
                { "users:modify:current", "Client" },
                { "users:read", "Administrator" },
                { "users:read", "Client" }
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "parq_questions",
            columns: new[] { "id", "condition_type", "parent_question_id", "question" },
            values: new object[,]
            {
                { 9, 1, 8, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 10, 1, 8, "Do you have joint problems causing pain, a recent fracture, or fracture caused by osteoporosis or cancer?" },
                { 11, 1, 8, "Have you had steroid injections or taken steroid tablets regularly for more than 3 months?" },
                { 13, 1, 12, "Does your cancer diagnosis include any of the following types: lung/bronchogenic, multiple myeloma, head, and/or neck?" },
                { 14, 1, 12, "Are you currently receiving cancer therapy (such as chemotherapy or radiotherapy)?" },
                { 16, 1, 15, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 17, 1, 15, "Do you have an irregular heartbeat that requires medical management?" },
                { 18, 1, 15, "Do you have chronic heart failure?" },
                { 19, 1, 15, "Do you have diagnosed coronary artery disease and have not participated in regular physical activity in the last 2 months?" },
                { 21, 1, 20, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 22, 1, 20, "Do you have a resting blood pressure equal to or greater than 160/90 mmHg with or without medication?" },
                { 24, 1, 23, "Do you often have difficulty controlling your blood sugar levels with foods, medications, or other physician-prescribed therapies?" },
                { 25, 1, 23, "Do you often suffer from signs and symptoms of low blood sugar following exercise and/or during activities of daily living?" },
                { 26, 1, 23, "Do you have any signs or symptoms of diabetes complications such as heart or vascular disease?" },
                { 27, 1, 23, "Do you have other metabolic conditions such as pregnancy-related diabetes, chronic kidney disease, or liver problems?" },
                { 28, 1, 23, "Are you planning to engage in unusually high intensity exercise in the near future?" },
                { 30, 1, 29, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 31, 1, 29, "Do you have Down Syndrome AND back problems affecting nerves or muscles?" },
                { 33, 1, 32, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 34, 1, 32, "Has your doctor ever said your blood oxygen level is low at rest or during exercise and/or that you require supplemental oxygen therapy?" },
                { 35, 1, 32, "If asthmatic, do you currently have symptoms of chest tightness, wheezing, laboured breathing, or consistent cough?" },
                { 36, 1, 32, "Has your doctor ever said you have high blood pressure in the blood vessels of your lungs?" },
                { 38, 1, 37, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 39, 1, 37, "Do you commonly exhibit low resting blood pressure significant enough to cause dizziness, light-headedness, and/or fainting?" },
                { 40, 1, 37, "Has your physician indicated that you exhibit sudden bouts of high blood pressure (known as Autonomic Dysreflexia)?" },
                { 42, 1, 41, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?" },
                { 43, 1, 41, "Do you have any impairment in walking or mobility?" },
                { 44, 1, 41, "Have you experienced a stroke or impairment in nerves or muscles in the past 6 months?" },
                { 46, 1, 45, "Have you experienced a blackout, fainted, or lost consciousness as a result of a head injury within the last 12 months?" },
                { 47, 1, 45, "Do you have a medical condition that is not listed (such as epilepsy, neurological conditions, kidney problems)?" },
                { 48, 1, 45, "Do you currently live with two or more medical conditions?" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_parq_completions_user_id",
            schema: "users",
            table: "parq_completions",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_parq_questions_parent_question_id",
            schema: "users",
            table: "parq_questions",
            column: "parent_question_id");

        migrationBuilder.CreateIndex(
            name: "ix_parq_responses_question_id",
            schema: "users",
            table: "parq_responses",
            column: "question_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_permissions_role_name",
            schema: "users",
            table: "role_permissions",
            column: "role_name");

        migrationBuilder.CreateIndex(
            name: "ix_user_roles_user_table_id",
            schema: "users",
            table: "user_roles",
            column: "user_table_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "users",
            table: "users",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_id",
            schema: "users",
            table: "users",
            column: "id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_identity_id",
            schema: "users",
            table: "users",
            column: "identity_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "users");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "users");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "users");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "users");

        migrationBuilder.DropTable(
            name: "parq_completions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "parq_responses",
            schema: "users");

        migrationBuilder.DropTable(
            name: "role_permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "user_fitness_profiles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "user_parQs",
            schema: "users");

        migrationBuilder.DropTable(
            name: "user_roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "parq_questions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "users",
            schema: "users");
    }
}
