using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileSessionsAndProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[ProgressEntries]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [ProgressEntries] (
                        [Id] int NOT NULL IDENTITY,
                        [UserId] nvarchar(450) NOT NULL,
                        [EntryDate] datetime2 NOT NULL,
                        [WeightKg] decimal(5,2) NOT NULL,
                        [ChestCm] decimal(5,2) NULL,
                        [WaistCm] decimal(5,2) NULL,
                        [HipCm] decimal(5,2) NULL,
                        [Notes] nvarchar(500) NULL,
                        CONSTRAINT [PK_ProgressEntries] PRIMARY KEY ([Id]),
                        CONSTRAINT [FK_ProgressEntries_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
                    );
                END

                IF OBJECT_ID(N'[UserProfiles]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [UserProfiles] (
                        [Id] int NOT NULL IDENTITY,
                        [UserId] nvarchar(450) NOT NULL,
                        [FirstName] nvarchar(80) NOT NULL,
                        [LastName] nvarchar(80) NOT NULL,
                        [Age] int NOT NULL,
                        [HeightCm] decimal(5,2) NOT NULL,
                        [WeightKg] decimal(5,2) NOT NULL,
                        [Goal] nvarchar(300) NULL,
                        [CreatedAt] datetime2 NOT NULL,
                        CONSTRAINT [PK_UserProfiles] PRIMARY KEY ([Id]),
                        CONSTRAINT [FK_UserProfiles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
                    );
                END

                IF OBJECT_ID(N'[UserTrainingSessions]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [UserTrainingSessions] (
                        [Id] int NOT NULL IDENTITY,
                        [UserId] nvarchar(450) NOT NULL,
                        [Name] nvarchar(120) NOT NULL,
                        [TrainingType] nvarchar(80) NULL,
                        [SessionDate] datetime2 NOT NULL,
                        [DurationMinutes] int NOT NULL,
                        [CaloriesBurned] int NOT NULL,
                        [Notes] nvarchar(500) NULL,
                        CONSTRAINT [PK_UserTrainingSessions] PRIMARY KEY ([Id]),
                        CONSTRAINT [FK_UserTrainingSessions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
                    );
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = N'IX_ProgressEntries_UserId_EntryDate'
                    AND object_id = OBJECT_ID(N'[ProgressEntries]')
                )
                BEGIN
                    CREATE INDEX [IX_ProgressEntries_UserId_EntryDate] ON [ProgressEntries] ([UserId], [EntryDate]);
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = N'IX_UserProfiles_UserId'
                    AND object_id = OBJECT_ID(N'[UserProfiles]')
                )
                BEGIN
                    CREATE UNIQUE INDEX [IX_UserProfiles_UserId] ON [UserProfiles] ([UserId]);
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = N'IX_UserTrainingSessions_UserId_SessionDate'
                    AND object_id = OBJECT_ID(N'[UserTrainingSessions]')
                )
                BEGIN
                    CREATE INDEX [IX_UserTrainingSessions_UserId_SessionDate] ON [UserTrainingSessions] ([UserId], [SessionDate]);
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[ProgressEntries]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [ProgressEntries];
                END

                IF OBJECT_ID(N'[UserProfiles]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [UserProfiles];
                END

                IF OBJECT_ID(N'[UserTrainingSessions]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [UserTrainingSessions];
                END
                """);
        }
    }
}
