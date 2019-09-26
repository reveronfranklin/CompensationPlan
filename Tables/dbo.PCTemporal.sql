CREATE TABLE [dbo].[PCTemporal]
(
[Id] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdCliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Transaccion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Documento] [numeric] (18, 0) NOT NULL,
[Linea] [smallint] NOT NULL,
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[Orden] [bigint] NULL,
[Producto] [nvarchar] (12) COLLATE Modern_Spanish_CI_AS NULL,
[MontoReal] [numeric] (18, 2) NULL,
[BsComision] [numeric] (18, 2) NULL,
[PorcFlat] [numeric] (18, 2) NULL,
[ComisionRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL,
[PorcRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL,
[PorcCantidadCuotasCumplidas] [numeric] (18, 2) NULL,
[ComisionCantidadCuotasCumplidas] [numeric] (18, 2) NULL,
[CantidadCuotasCumplidas] [int] NULL,
[TotalVentasMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCTemporal_TotalVentasMes] DEFAULT ((0)),
[TotalCuotaMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCTemporal_TotalCuotaMes] DEFAULT ((0)),
[PeriodoDesde] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCTemporal_PeriodoDesde] DEFAULT (''),
[PeriodoHasta] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCTemporal_PeriodoHasta] DEFAULT (''),
[IdTipoPago] [int] NULL,
[FechaRegistro] [datetime] NULL,
[DescripcionTipoPago] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCTemporal_DescipcionTipoPago] DEFAULT (''),
[MontoString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[OrdenString] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[DocumentoString] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[MontoRealString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[IdPeriodo] [int] NULL
)
GO
ALTER TABLE [dbo].[PCTemporal] ADD CONSTRAINT [PK_PCTemporal_1] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCTemporal] ON [dbo].[PCTemporal] ([IdVendedor])
GO
ALTER TABLE [dbo].[PCTemporal] ADD CONSTRAINT [FK_PCTemporal_PCTipoPago] FOREIGN KEY ([IdTipoPago]) REFERENCES [dbo].[PCTipoPago] ([Id])
GO
