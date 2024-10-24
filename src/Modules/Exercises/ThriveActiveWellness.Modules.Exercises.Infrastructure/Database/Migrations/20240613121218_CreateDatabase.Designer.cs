﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

#nullable disable

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ExercisesDbContext))]
    [Migration("20240613121218_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("exercises")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ThriveActiveWellness.Common.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Common.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("inbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Common.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Common.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("outbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Clients.Client", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("table_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TableId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("TableId")
                        .HasName("pk_clients");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_clients_id");

                    b.ToTable("clients", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Equipment.Equipment", b =>
                {
                    b.Property<long>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("table_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TableId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("TableId")
                        .HasName("pk_equipment");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_equipment_id");

                    b.ToTable("equipment", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exercise", b =>
                {
                    b.Property<long>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("table_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TableId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("difficulty");

                    b.Property<long>("EquipmentTableId")
                        .HasColumnType("bigint")
                        .HasColumnName("equipment_table_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("TableId")
                        .HasName("pk_exercises");

                    b.HasIndex("EquipmentTableId")
                        .HasDatabaseName("ix_exercises_equipment_table_id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_exercises_id");

                    b.ToTable("exercises", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.ExerciseMuscleGroup", b =>
                {
                    b.Property<long>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("table_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TableId"));

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint")
                        .HasColumnName("exercise_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long>("MuscleGroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("muscle_group_id");

                    b.Property<int>("MuscleGroupType")
                        .HasColumnType("integer")
                        .HasColumnName("muscle_group_type");

                    b.HasKey("TableId")
                        .HasName("pk_exercise_muscle_groups");

                    b.HasIndex("ExerciseId")
                        .HasDatabaseName("ix_exercise_muscle_groups_exercise_id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_exercise_muscle_groups_id");

                    b.HasIndex("MuscleGroupId")
                        .HasDatabaseName("ix_exercise_muscle_groups_muscle_group_id");

                    b.ToTable("exercise_muscle_groups", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.MuscleGroup", b =>
                {
                    b.Property<long>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("table_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TableId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("TableId")
                        .HasName("pk_muscle_groups");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_muscle_groups_id");

                    b.ToTable("muscle_groups", "exercises");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exercise", b =>
                {
                    b.HasOne("ThriveActiveWellness.Modules.Exercises.Domain.Equipment.Equipment", null)
                        .WithMany()
                        .HasForeignKey("EquipmentTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercises_equipment_equipment_table_id");

                    b.OwnsMany("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Media", "Media", b1 =>
                        {
                            b1.Property<long>("ExerciseTableId")
                                .HasColumnType("bigint")
                                .HasColumnName("exercise_table_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)")
                                .HasColumnName("description");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("type");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)")
                                .HasColumnName("url");

                            b1.HasKey("ExerciseTableId", "Id")
                                .HasName("pk_exercise_media");

                            b1.ToTable("exercise_media", "exercises");

                            b1.WithOwner()
                                .HasForeignKey("ExerciseTableId")
                                .HasConstraintName("fk_exercise_media_exercises_exercise_table_id");
                        });

                    b.Navigation("Media");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.ExerciseMuscleGroup", b =>
                {
                    b.HasOne("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exercise", null)
                        .WithMany("ExerciseMuscleGroups")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercise_muscle_groups_exercises_exercise_id");

                    b.HasOne("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.MuscleGroup", null)
                        .WithMany()
                        .HasForeignKey("MuscleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercise_muscle_groups_muscle_groups_muscle_group_id");
                });

            modelBuilder.Entity("ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exercise", b =>
                {
                    b.Navigation("ExerciseMuscleGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
