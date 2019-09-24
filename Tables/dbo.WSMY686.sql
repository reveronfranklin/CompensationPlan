CREATE TABLE [dbo].[WSMY686]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Desde] [datetime] NOT NULL,
[Hasta] [datetime] NOT NULL,
[Activo] [bit] NOT NULL,
[DescPeriodo] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaActualiza] [datetime] NOT NULL
)
GO
