CREATE TABLE [dbo].[PCResumenOficinaTemporal]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CodigoOficina] [int] NULL,
[NombreOficina] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[Monto] [numeric] (18, 2) NULL,
[MontoString] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCResumenOficinaTemporal] ADD CONSTRAINT [PK_PCResumenOficinaTemporal] PRIMARY KEY CLUSTERED  ([Id])
GO
