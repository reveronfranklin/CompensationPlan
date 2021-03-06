CREATE TABLE [dbo].[PCHistorico]
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
[TotalVentasMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCHistorico_TotalVentasMes] DEFAULT ((0)),
[TotalCuotaMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCHistorico_TotalCuotaMes] DEFAULT ((0)),
[PeriodoDesde] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCHistorico_PeriodoDesde] DEFAULT (''),
[PeriodoHasta] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCHistorico_PeriodoHasta] DEFAULT (''),
[IdTipoPago] [int] NULL,
[FechaRegistro] [datetime] NULL,
[DescripcionTipoPago] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCHistorico_DescipcionTipoPago] DEFAULT (''),
[MontoString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[OrdenString] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[DocumentoString] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[MontoRealString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[IdPeriodo] [int] NULL,
[ConceptoNomina] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCHistorico] ADD CONSTRAINT [PK_PCHistorico_1] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCHistorico_2] ON [dbo].[PCHistorico] ([DocumentoString])
GO
CREATE NONCLUSTERED INDEX [IX_PCHistorico] ON [dbo].[PCHistorico] ([IdPeriodo])
GO
CREATE NONCLUSTERED INDEX [IX_PCHistorico_1] ON [dbo].[PCHistorico] ([OrdenString])
GO
ALTER TABLE [dbo].[PCHistorico] ADD CONSTRAINT [FK_PCHistorico_PCTipoPago] FOREIGN KEY ([IdTipoPago]) REFERENCES [dbo].[PCTipoPago] ([Id])
GO
