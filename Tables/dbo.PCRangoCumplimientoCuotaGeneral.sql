CREATE TABLE [dbo].[PCRangoCumplimientoCuotaGeneral]
(
[Id] [int] NOT NULL,
[Desde] [numeric] (18, 2) NULL,
[Hasta] [numeric] (18, 2) NULL,
[Porcentaje] [numeric] (18, 2) NULL,
[PorcentajeGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCRangoCumplimientoCuotaGeneral_PorcentajeGerente] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[PCRangoCumplimientoCuotaGeneral] ADD CONSTRAINT [PK_RangoCumplimientoCuotaGeneral] PRIMARY KEY CLUSTERED  ([Id])
GO
