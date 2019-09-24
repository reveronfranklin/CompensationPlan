CREATE TABLE [dbo].[PCResumenComisionTemporal]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Periodo] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[CodigoOficina] [int] NULL,
[NombreOficina] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[CodigoVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[NombreVendedor] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[Ficha] [nvarchar] (6) COLLATE Modern_Spanish_CI_AS NULL,
[Monto] [numeric] (18, 2) NULL,
[MontoString] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCResumenComisionTemporal] ADD CONSTRAINT [PK_PCResumenComisionTemporal] PRIMARY KEY CLUSTERED  ([Id])
GO
