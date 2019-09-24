CREATE TABLE [dbo].[PCAñoMesOrden]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Orden] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[Año] [int] NULL CONSTRAINT [DF_PCAñoMesOrden_Año] DEFAULT ((0)),
[Mes] [int] NULL CONSTRAINT [DF_PCAñoMesOrden_Mes] DEFAULT ((0)),
[FechaOrden] [smalldatetime] NULL,
[Cotizacion] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[NombreVendedor] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[IdCliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[NombreCliente] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCAñoMesOrden] ADD CONSTRAINT [PK_PCAñoMesOrden] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCAñoMesOrden] ON [dbo].[PCAñoMesOrden] ([Orden])
GO
