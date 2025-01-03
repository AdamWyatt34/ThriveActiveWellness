﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUser,
            Permission.ModifyAllUsers,
            Permission.ModifyIfCurrentUser,
            Permission.CreateEquipment,
            Permission.DeleteEquipment,
            Permission.GetEquipment,
            Permission.ModifyEquipment,
            Permission.SearchEquipment,
            Permission.CreateExercise,
            Permission.GetExercise,
            Permission.ModifyExercise,
            Permission.SearchExercise);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");

                joinBuilder.HasData(
                    // Member permissions
                    CreateRolePermission(Role.Client, Permission.GetUser),
                    CreateRolePermission(Role.Client, Permission.ModifyIfCurrentUser),
                    CreateRolePermission(Role.Client, Permission.SearchEquipment),
                    CreateRolePermission(Role.Client, Permission.GetEquipment),
                    CreateRolePermission(Role.Client, Permission.GetExercise),
                    CreateRolePermission(Role.Client, Permission.SearchExercise),
                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyIfCurrentUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyAllUsers),
                    CreateRolePermission(Role.Administrator, Permission.CreateEquipment),
                    CreateRolePermission(Role.Administrator, Permission.DeleteEquipment),
                    CreateRolePermission(Role.Administrator, Permission.GetEquipment),
                    CreateRolePermission(Role.Administrator, Permission.ModifyEquipment),
                    CreateRolePermission(Role.Administrator, Permission.CreateExercise),
                    CreateRolePermission(Role.Administrator, Permission.GetExercise),
                    CreateRolePermission(Role.Administrator, Permission.ModifyExercise),
                    CreateRolePermission(Role.Administrator, Permission.SearchExercise));
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
