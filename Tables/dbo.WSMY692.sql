CREATE TABLE [dbo].[WSMY692]
(
[Recnum] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[IdSubCategoria] [int] NOT NULL,
[BsDesde] [numeric] (18, 2) NOT NULL,
[BsHasta] [numeric] (18, 2) NOT NULL,
[Porcentaje] [numeric] (18, 2) NOT NULL,
[FechaActualiza] [datetime] NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
ALTER TABLE [dbo].[WSMY692] ADD CONSTRAINT [PK_WSMY692] PRIMARY KEY CLUSTERED  ([Recnum])
GO
ALTER TABLE [dbo].[WSMY692] ADD CONSTRAINT [FK_WSMY692_WSMY437] FOREIGN KEY ([IdSubCategoria]) REFERENCES [dbo].[WSMY437] ([IdSubCategoria])
GO
