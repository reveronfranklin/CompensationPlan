CREATE TABLE [dbo].[PCPorcCantidadCategoriasCubiertas]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Cantidad] [int] NULL,
[Porcentaje] [numeric] (18, 2) NULL,
[PorcentajeGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCPorcCantidadCategoriasCubiertas_PorcentajeGerente] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[PCPorcCantidadCategoriasCubiertas] ADD CONSTRAINT [PK_PorcCantidadCategoriasCubiertas] PRIMARY KEY CLUSTERED  ([Id])
GO
