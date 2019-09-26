CREATE TABLE [dbo].[PCCuotaVentas]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Año] [int] NULL,
[Mes] [int] NULL,
[Vendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[IdProductoCuota] [int] NULL,
[Cuota] [numeric] (18, 2) NULL,
[Venta] [numeric] (18, 2) NULL,
[PorcCumplimiento] [numeric] (18, 2) NULL,
[CuotaUsd] [numeric] (18, 2) NULL,
[VentaUsd] [numeric] (18, 2) NULL,
[TasaUsd] [numeric] (18, 2) NULL,
[NombreVendedor] [nvarchar] (30) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCCuotaVentas_NombreVendedor] DEFAULT (''),
[DescripcionCuota] [nchar] (50) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCCuotaVentas_DescripcionCuota] DEFAULT (''),
[CuotaString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[VentaString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[CuotaUsdString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[VentaUsdString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[TasaUsdString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[Supervisor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCCuotaVentas] ADD CONSTRAINT [PK_CuotaVentas] PRIMARY KEY CLUSTERED  ([Id])
GO
