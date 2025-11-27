IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] bigint NOT NULL IDENTITY,
        [IsProfessional] bit NOT NULL,
        [IsClient] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] bigint NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] bigint NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] bigint NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] bigint NOT NULL,
        [RoleId] bigint NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] bigint NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [ProfessionalProfiles] (
        [Id] bigint NOT NULL IDENTITY,
        [UserId] bigint NOT NULL,
        [ProfessionalName] nvarchar(100) NOT NULL,
        [Bio] nvarchar(1000) NOT NULL,
        [Expertise] nvarchar(200) NOT NULL,
        [IsPremium] bit NOT NULL,
        [SkillDolarBalance] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_ProfessionalProfiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProfessionalProfiles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [ServiceOffered] (
        [Id] bigint NOT NULL IDENTITY,
        [Title] nvarchar(50) NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [Price] decimal(18,2) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ProfessionalProfileId] bigint NOT NULL,
        [CategoryId] bigint NOT NULL,
        CONSTRAINT [PK_ServiceOffered] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceOffered_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ServiceOffered_ProfessionalProfiles_ProfessionalProfileId] FOREIGN KEY ([ProfessionalProfileId]) REFERENCES [ProfessionalProfiles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [Reviews] (
        [Id] bigint NOT NULL IDENTITY,
        [ServiceId] bigint NOT NULL,
        [ReviewerId] bigint NOT NULL,
        [Rating] int NOT NULL,
        [Comment] nvarchar(1000) NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ProfessionalProfileId] bigint NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reviews_AspNetUsers_ReviewerId] FOREIGN KEY ([ReviewerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reviews_ProfessionalProfiles_ProfessionalProfileId] FOREIGN KEY ([ProfessionalProfileId]) REFERENCES [ProfessionalProfiles] ([Id]),
        CONSTRAINT [FK_Reviews_ServiceOffered_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [SkillSwapRequests] (
        [Id] bigint NOT NULL IDENTITY,
        [ProfessionalAId] bigint NOT NULL,
        [ProfessionalBId] bigint NOT NULL,
        [ServiceAId] bigint NULL,
        [ServiceBId] bigint NOT NULL,
        [SwapDate] datetimeoffset NOT NULL,
        [Status] int NOT NULL,
        [OfferedAmount] decimal(18,2) NULL,
        CONSTRAINT [PK_SkillSwapRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SkillSwapRequests_ProfessionalProfiles_ProfessionalAId] FOREIGN KEY ([ProfessionalAId]) REFERENCES [ProfessionalProfiles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SkillSwapRequests_ProfessionalProfiles_ProfessionalBId] FOREIGN KEY ([ProfessionalBId]) REFERENCES [ProfessionalProfiles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SkillSwapRequests_ServiceOffered_ServiceAId] FOREIGN KEY ([ServiceAId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SkillSwapRequests_ServiceOffered_ServiceBId] FOREIGN KEY ([ServiceBId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [ServiceRequests] (
        [Id] bigint NOT NULL IDENTITY,
        [ClientId] bigint NOT NULL,
        [ServiceId] bigint NOT NULL,
        [ServiceTitle] nvarchar(50) NOT NULL,
        [RequestDate] datetimeoffset NOT NULL,
        [Observations] nvarchar(500) NULL,
        [Status] nvarchar(max) NOT NULL,
        [ReviewId] bigint NULL,
        [ApplicationUserId] bigint NULL,
        CONSTRAINT [PK_ServiceRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceRequests_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_ServiceRequests_AspNetUsers_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ServiceRequests_Reviews_ReviewId] FOREIGN KEY ([ReviewId]) REFERENCES [Reviews] ([Id]),
        CONSTRAINT [FK_ServiceRequests_ServiceOffered_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [Messages] (
        [Id] bigint NOT NULL IDENTITY,
        [SenderId] bigint NOT NULL,
        [ReceiverId] bigint NOT NULL,
        [Content] nvarchar(1000) NOT NULL,
        [SentAt] datetimeoffset NOT NULL,
        [IsRead] bit NOT NULL,
        [SkillSwapRequestId] bigint NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_SkillSwapRequests_SkillSwapRequestId] FOREIGN KEY ([SkillSwapRequestId]) REFERENCES [SkillSwapRequests] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE TABLE [Transaction] (
        [Id] bigint NOT NULL IDENTITY,
        [SenderProfileId] bigint NOT NULL,
        [ReceiverProfileId] bigint NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [TransactionDate] datetimeoffset NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [SkillSwapRequestId] bigint NULL,
        CONSTRAINT [PK_Transaction] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transaction_ProfessionalProfiles_ReceiverProfileId] FOREIGN KEY ([ReceiverProfileId]) REFERENCES [ProfessionalProfiles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Transaction_ProfessionalProfiles_SenderProfileId] FOREIGN KEY ([SenderProfileId]) REFERENCES [ProfessionalProfiles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Transaction_SkillSwapRequests_SkillSwapRequestId] FOREIGN KEY ([SkillSwapRequestId]) REFERENCES [SkillSwapRequests] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Messages_ReceiverId] ON [Messages] ([ReceiverId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Messages_SkillSwapRequestId] ON [Messages] ([SkillSwapRequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ProfessionalProfiles_UserId] ON [ProfessionalProfiles] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Reviews_ProfessionalProfileId] ON [Reviews] ([ProfessionalProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Reviews_ReviewerId] ON [Reviews] ([ReviewerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Reviews_ServiceId] ON [Reviews] ([ServiceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceOffered_CategoryId] ON [ServiceOffered] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceOffered_ProfessionalProfileId] ON [ServiceOffered] ([ProfessionalProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceRequests_ApplicationUserId] ON [ServiceRequests] ([ApplicationUserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceRequests_ClientId] ON [ServiceRequests] ([ClientId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceRequests_ReviewId] ON [ServiceRequests] ([ReviewId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ServiceRequests_ServiceId] ON [ServiceRequests] ([ServiceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_SkillSwapRequests_ProfessionalAId] ON [SkillSwapRequests] ([ProfessionalAId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_SkillSwapRequests_ProfessionalBId] ON [SkillSwapRequests] ([ProfessionalBId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_SkillSwapRequests_ServiceAId] ON [SkillSwapRequests] ([ServiceAId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_SkillSwapRequests_ServiceBId] ON [SkillSwapRequests] ([ServiceBId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Transaction_ReceiverProfileId] ON [Transaction] ([ReceiverProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Transaction_SenderProfileId] ON [Transaction] ([SenderProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Transaction_SkillSwapRequestId] ON [Transaction] ([SkillSwapRequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251015151003_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251015151003_InitialMigration', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_AspNetUsers_SenderId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_SkillSwapRequests_SkillSwapRequestId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ProfessionalProfiles] DROP CONSTRAINT [FK_ProfessionalProfiles_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_AspNetUsers_ReviewerId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_ProfessionalProfiles_ProfessionalProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_ServiceOffered_ServiceId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] DROP CONSTRAINT [FK_ServiceOffered_Categories_CategoryId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] DROP CONSTRAINT [FK_ServiceOffered_ProfessionalProfiles_ProfessionalProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_AspNetUsers_ApplicationUserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_AspNetUsers_ClientId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_Reviews_ReviewId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_ServiceOffered_ServiceId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequests] DROP CONSTRAINT [FK_SkillSwapRequests_ProfessionalProfiles_ProfessionalAId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequests] DROP CONSTRAINT [FK_SkillSwapRequests_ProfessionalProfiles_ProfessionalBId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequests] DROP CONSTRAINT [FK_SkillSwapRequests_ServiceOffered_ServiceAId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequests] DROP CONSTRAINT [FK_SkillSwapRequests_ServiceOffered_ServiceBId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] DROP CONSTRAINT [FK_Transaction_ProfessionalProfiles_ReceiverProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] DROP CONSTRAINT [FK_Transaction_ProfessionalProfiles_SenderProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] DROP CONSTRAINT [FK_Transaction_SkillSwapRequests_SkillSwapRequestId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequests] DROP CONSTRAINT [PK_SkillSwapRequests];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [PK_ServiceRequests];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [PK_Reviews];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ProfessionalProfiles] DROP CONSTRAINT [PK_ProfessionalProfiles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_ProfessionalProfiles_UserId] ON [ProfessionalProfiles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [PK_Messages];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Categories] DROP CONSTRAINT [PK_Categories];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[SkillSwapRequests]', N'SkillSwapRequest', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ServiceRequests]', N'ServiceRequest', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Reviews]', N'Review', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ProfessionalProfiles]', N'ProfessionalProfile', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Messages]', N'Message', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Categories]', N'Category', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[SkillSwapRequest].[IX_SkillSwapRequests_ServiceBId]', N'IX_SkillSwapRequest_ServiceBId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[SkillSwapRequest].[IX_SkillSwapRequests_ServiceAId]', N'IX_SkillSwapRequest_ServiceAId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[SkillSwapRequest].[IX_SkillSwapRequests_ProfessionalBId]', N'IX_SkillSwapRequest_ProfessionalBId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[SkillSwapRequest].[IX_SkillSwapRequests_ProfessionalAId]', N'IX_SkillSwapRequest_ProfessionalAId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ServiceRequest].[IX_ServiceRequests_ServiceId]', N'IX_ServiceRequest_ServiceId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ServiceRequest].[IX_ServiceRequests_ReviewId]', N'IX_ServiceRequest_ReviewId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ServiceRequest].[IX_ServiceRequests_ClientId]', N'IX_ServiceRequest_ClientId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[ServiceRequest].[IX_ServiceRequests_ApplicationUserId]', N'IX_ServiceRequest_ApplicationUserId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Review].[IX_Reviews_ServiceId]', N'IX_Review_ServiceId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Review].[IX_Reviews_ReviewerId]', N'IX_Review_ReviewerId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Review].[IX_Reviews_ProfessionalProfileId]', N'IX_Review_ProfessionalProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Message].[IX_Messages_SkillSwapRequestId]', N'IX_Message_SkillSwapRequestId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Message].[IX_Messages_SenderId]', N'IX_Message_SenderId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    EXEC sp_rename N'[Message].[IX_Messages_ReceiverId]', N'IX_Message_ReceiverId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'TransactionDate');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [TransactionDate] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Transaction_SkillSwapRequestId] ON [Transaction];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'SkillSwapRequestId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [SkillSwapRequestId] BIGINT NULL;
    CREATE INDEX [IX_Transaction_SkillSwapRequestId] ON [Transaction] ([SkillSwapRequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Transaction_SenderProfileId] ON [Transaction];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'SenderProfileId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [SenderProfileId] BIGINT NOT NULL;
    CREATE INDEX [IX_Transaction_SenderProfileId] ON [Transaction] ([SenderProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Transaction_ReceiverProfileId] ON [Transaction];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'ReceiverProfileId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [ReceiverProfileId] BIGINT NOT NULL;
    CREATE INDEX [IX_Transaction_ReceiverProfileId] ON [Transaction] ([ReceiverProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'Description');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [Description] NVARCHAR(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'Amount');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Transaction] ALTER COLUMN [Amount] DECIMAL(18,2) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'Title');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [Title] NVARCHAR(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_ServiceOffered_ProfessionalProfileId] ON [ServiceOffered];
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'ProfessionalProfileId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [ProfessionalProfileId] BIGINT NOT NULL;
    CREATE INDEX [IX_ServiceOffered_ProfessionalProfileId] ON [ServiceOffered] ([ProfessionalProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'Price');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [Price] DECIMAL(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'IsActive');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [IsActive] BIT NOT NULL;
    ALTER TABLE [ServiceOffered] ADD DEFAULT CAST(1 AS BIT) FOR [IsActive];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'Description');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [Description] NVARCHAR(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'CreatedAt');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [CreatedAt] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_ServiceOffered_CategoryId] ON [ServiceOffered];
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceOffered]') AND [c].[name] = N'CategoryId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ServiceOffered] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ServiceOffered] ALTER COLUMN [CategoryId] BIGINT NOT NULL;
    CREATE INDEX [IX_ServiceOffered_CategoryId] ON [ServiceOffered] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] ADD [CategoryId1] bigint NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ProfessionalProfileId] bigint NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'SwapDate');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [SwapDate] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'Status');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [Status] INT NOT NULL;
    ALTER TABLE [SkillSwapRequest] ADD DEFAULT 0 FOR [Status];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_SkillSwapRequest_ServiceBId] ON [SkillSwapRequest];
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'ServiceBId');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [ServiceBId] BIGINT NOT NULL;
    CREATE INDEX [IX_SkillSwapRequest_ServiceBId] ON [SkillSwapRequest] ([ServiceBId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_SkillSwapRequest_ServiceAId] ON [SkillSwapRequest];
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'ServiceAId');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [ServiceAId] BIGINT NULL;
    CREATE INDEX [IX_SkillSwapRequest_ServiceAId] ON [SkillSwapRequest] ([ServiceAId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_SkillSwapRequest_ProfessionalBId] ON [SkillSwapRequest];
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'ProfessionalBId');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [ProfessionalBId] BIGINT NOT NULL;
    CREATE INDEX [IX_SkillSwapRequest_ProfessionalBId] ON [SkillSwapRequest] ([ProfessionalBId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_SkillSwapRequest_ProfessionalAId] ON [SkillSwapRequest];
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'ProfessionalAId');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [ProfessionalAId] BIGINT NOT NULL;
    CREATE INDEX [IX_SkillSwapRequest_ProfessionalAId] ON [SkillSwapRequest] ([ProfessionalAId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SkillSwapRequest]') AND [c].[name] = N'OfferedAmount');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [SkillSwapRequest] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [SkillSwapRequest] ALTER COLUMN [OfferedAmount] DECIMAL(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'Status');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [Status] NVARCHAR(20) NOT NULL;
    ALTER TABLE [ServiceRequest] ADD DEFAULT N'Pendente' FOR [Status];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'ServiceTitle');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [ServiceTitle] NVARCHAR(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_ServiceRequest_ServiceId] ON [ServiceRequest];
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'ServiceId');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [ServiceId] BIGINT NOT NULL;
    CREATE INDEX [IX_ServiceRequest_ServiceId] ON [ServiceRequest] ([ServiceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'RequestDate');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [RequestDate] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'Observations');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [Observations] NVARCHAR(500) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_ServiceRequest_ClientId] ON [ServiceRequest];
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequest]') AND [c].[name] = N'ClientId');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequest] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [ServiceRequest] ALTER COLUMN [ClientId] BIGINT NOT NULL;
    CREATE INDEX [IX_ServiceRequest_ClientId] ON [ServiceRequest] ([ClientId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Review_ServiceId] ON [Review];
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'ServiceId');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [Review] ALTER COLUMN [ServiceId] BIGINT NOT NULL;
    CREATE INDEX [IX_Review_ServiceId] ON [Review] ([ServiceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Review_ReviewerId] ON [Review];
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'ReviewerId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [Review] ALTER COLUMN [ReviewerId] BIGINT NOT NULL;
    CREATE INDEX [IX_Review_ReviewerId] ON [Review] ([ReviewerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'Rating');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [Review] ALTER COLUMN [Rating] INT NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'CreatedAt');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [Review] ALTER COLUMN [CreatedAt] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'Comment');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [Review] ALTER COLUMN [Comment] NVARCHAR(1000) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'UserId');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [UserId] BIGINT NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'SkillDolarBalance');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [SkillDolarBalance] DECIMAL(18,2) NOT NULL;
    ALTER TABLE [ProfessionalProfile] ADD DEFAULT 0.0 FOR [SkillDolarBalance];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'ProfessionalName');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [ProfessionalName] NVARCHAR(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'IsPremium');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [IsPremium] BIT NOT NULL;
    ALTER TABLE [ProfessionalProfile] ADD DEFAULT CAST(0 AS BIT) FOR [IsPremium];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'Expertise');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [Expertise] NVARCHAR(200) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProfessionalProfile]') AND [c].[name] = N'Bio');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [ProfessionalProfile] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [ProfessionalProfile] ALTER COLUMN [Bio] NVARCHAR(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Message_SkillSwapRequestId] ON [Message];
    DECLARE @var37 sysname;
    SELECT @var37 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'SkillSwapRequestId');
    IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var37 + '];');
    ALTER TABLE [Message] ALTER COLUMN [SkillSwapRequestId] BIGINT NULL;
    CREATE INDEX [IX_Message_SkillSwapRequestId] ON [Message] ([SkillSwapRequestId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var38 sysname;
    SELECT @var38 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'SentAt');
    IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var38 + '];');
    ALTER TABLE [Message] ALTER COLUMN [SentAt] DATETIMEOFFSET NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Message_SenderId] ON [Message];
    DECLARE @var39 sysname;
    SELECT @var39 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'SenderId');
    IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var39 + '];');
    ALTER TABLE [Message] ALTER COLUMN [SenderId] BIGINT NOT NULL;
    CREATE INDEX [IX_Message_SenderId] ON [Message] ([SenderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DROP INDEX [IX_Message_ReceiverId] ON [Message];
    DECLARE @var40 sysname;
    SELECT @var40 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'ReceiverId');
    IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var40 + '];');
    ALTER TABLE [Message] ALTER COLUMN [ReceiverId] BIGINT NOT NULL;
    CREATE INDEX [IX_Message_ReceiverId] ON [Message] ([ReceiverId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var41 sysname;
    SELECT @var41 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'IsRead');
    IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var41 + '];');
    ALTER TABLE [Message] ALTER COLUMN [IsRead] BIT NOT NULL;
    ALTER TABLE [Message] ADD DEFAULT CAST(0 AS BIT) FOR [IsRead];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var42 sysname;
    SELECT @var42 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Message]') AND [c].[name] = N'Content');
    IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [Message] DROP CONSTRAINT [' + @var42 + '];');
    ALTER TABLE [Message] ALTER COLUMN [Content] NVARCHAR(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var43 sysname;
    SELECT @var43 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'Name');
    IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var43 + '];');
    ALTER TABLE [Category] ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    DECLARE @var44 sysname;
    SELECT @var44 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'Description');
    IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var44 + '];');
    ALTER TABLE [Category] ALTER COLUMN [Description] NVARCHAR(200) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequest] ADD CONSTRAINT [PK_SkillSwapRequest] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequest] ADD CONSTRAINT [PK_ServiceRequest] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [PK_Review] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ProfessionalProfile] ADD CONSTRAINT [PK_ProfessionalProfile] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Message] ADD CONSTRAINT [PK_Message] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Category] ADD CONSTRAINT [PK_Category] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    CREATE INDEX [IX_ServiceOffered_CategoryId1] ON [ServiceOffered] ([CategoryId1]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    CREATE INDEX [IX_AspNetUsers_ProfessionalProfileId] ON [AspNetUsers] ([ProfessionalProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    CREATE INDEX [IX_ProfessionalProfile_UserId] ON [ProfessionalProfile] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_ProfessionalProfile_ProfessionalProfileId] FOREIGN KEY ([ProfessionalProfileId]) REFERENCES [ProfessionalProfile] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_SkillSwapRequest_SkillSwapRequestId] FOREIGN KEY ([SkillSwapRequestId]) REFERENCES [SkillSwapRequest] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ProfessionalProfile] ADD CONSTRAINT [FK_ProfessionalProfile_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_AspNetUsers_ReviewerId] FOREIGN KEY ([ReviewerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_ProfessionalProfile_ProfessionalProfileId] FOREIGN KEY ([ProfessionalProfileId]) REFERENCES [ProfessionalProfile] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_ServiceOffered_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] ADD CONSTRAINT [FK_ServiceOffered_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] ADD CONSTRAINT [FK_ServiceOffered_Category_CategoryId1] FOREIGN KEY ([CategoryId1]) REFERENCES [Category] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceOffered] ADD CONSTRAINT [FK_ServiceOffered_ProfessionalProfile_ProfessionalProfileId] FOREIGN KEY ([ProfessionalProfileId]) REFERENCES [ProfessionalProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequest] ADD CONSTRAINT [FK_ServiceRequest_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequest] ADD CONSTRAINT [FK_ServiceRequest_AspNetUsers_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequest] ADD CONSTRAINT [FK_ServiceRequest_Review_ReviewId] FOREIGN KEY ([ReviewId]) REFERENCES [Review] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [ServiceRequest] ADD CONSTRAINT [FK_ServiceRequest_ServiceOffered_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequest] ADD CONSTRAINT [FK_SkillSwapRequest_ProfessionalProfile_ProfessionalAId] FOREIGN KEY ([ProfessionalAId]) REFERENCES [ProfessionalProfile] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequest] ADD CONSTRAINT [FK_SkillSwapRequest_ProfessionalProfile_ProfessionalBId] FOREIGN KEY ([ProfessionalBId]) REFERENCES [ProfessionalProfile] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequest] ADD CONSTRAINT [FK_SkillSwapRequest_ServiceOffered_ServiceAId] FOREIGN KEY ([ServiceAId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [SkillSwapRequest] ADD CONSTRAINT [FK_SkillSwapRequest_ServiceOffered_ServiceBId] FOREIGN KEY ([ServiceBId]) REFERENCES [ServiceOffered] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_ProfessionalProfile_ReceiverProfileId] FOREIGN KEY ([ReceiverProfileId]) REFERENCES [ProfessionalProfile] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_ProfessionalProfile_SenderProfileId] FOREIGN KEY ([SenderProfileId]) REFERENCES [ProfessionalProfile] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_SkillSwapRequest_SkillSwapRequestId] FOREIGN KEY ([SkillSwapRequestId]) REFERENCES [SkillSwapRequest] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251109143835_v2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251109143835_v2', N'9.0.10');
END;

COMMIT;
GO

