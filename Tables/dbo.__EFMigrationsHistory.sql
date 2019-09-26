CREATE TABLE [dbo].[__EFMigrationsHistory]
(
[MigrationId] [nvarchar] (150) COLLATE Modern_Spanish_CI_AS NOT NULL,
[ProductVersion] [nvarchar] (32) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED  ([MigrationId])
GO
