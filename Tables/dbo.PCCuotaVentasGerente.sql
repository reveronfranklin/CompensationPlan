CREATE TABLE [dbo].[PCCuotaVentasGerente]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Año] [int] NULL,
[Mes] [int] NULL,
[Gerente] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[IdProductoCuota] [int] NULL,
[Cuota] [numeric] (18, 2) NULL,
[Venta] [numeric] (18, 2) NULL,
[PorcCumplimiento] [numeric] (18, 2) NULL,
[DescripcionCuota] [nchar] (50) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCCuotaVentasGerente_DescripcionCuota] DEFAULT (''),
[CuotaString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[VentaString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCCuotaVentasGerente] ADD CONSTRAINT [PK_PCCuotaVentasGerente] PRIMARY KEY CLUSTERED  ([Id])
GO
