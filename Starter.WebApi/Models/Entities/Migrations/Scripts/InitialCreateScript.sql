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

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [EmailAddress] nvarchar(450) NOT NULL,
    [HashedPassword] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Birthday] date NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    CONSTRAINT [User_pkey] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserAddresses] (
    [UserId] bigint NOT NULL,
    [AddressLine] nvarchar(max) NOT NULL,
    [AddressSupplement] nvarchar(max) NULL,
    [City] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    [StateProvince] nvarchar(max) NULL,
    [Country] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserAddresses] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_UserAddresses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Users_EmailAddress] ON [Users] ([EmailAddress]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240922215832_InitialCreate', N'8.0.8');
GO

COMMIT;
GO

