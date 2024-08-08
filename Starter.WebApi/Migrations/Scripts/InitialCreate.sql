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
GO

CREATE TABLE [UserCredentials] (
    [Id] bigint NOT NULL IDENTITY,
    [EmailAddress] nvarchar(255) NOT NULL,
    [HashedPassword] nvarchar(255) NULL,
    [UserRole] nvarchar(100) NULL,
    CONSTRAINT [UserCredentials_pkey] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserProfile] (
    [Id] bigint NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NULL,
    [LastName] nvarchar(100) NULL,
    [Birthday] date NULL,
    [Gender] nvarchar(20) NULL,
    [Position] nvarchar(255) NULL,
    [PersonalPhone] nvarchar(100) NULL,
    [ProfessionalPhone] nvarchar(100) NULL,
    [PostalAddress] nvarchar(255) NULL,
    [AddressSupplement] nvarchar(255) NULL,
    [City] nvarchar(100) NULL,
    [ZipCode] nvarchar(20) NULL,
    [StateProvince] nvarchar(100) NULL,
    [Country] nvarchar(100) NULL,
    [UserCredentialsId] bigint NOT NULL,
    CONSTRAINT [UserProfile_pkey] PRIMARY KEY ([Id]),
    CONSTRAINT [UserProfile_UserCredentialsId_fkey] FOREIGN KEY ([UserCredentialsId]) REFERENCES [UserCredentials] ([Id])
);
GO

CREATE INDEX [IX_UserProfile_UserCredentialsId] ON [UserProfile] ([UserCredentialsId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240807205632_InitialCreate', N'8.0.7');
GO

COMMIT;
GO

