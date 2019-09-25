CREATE TABLE [dbo].[PCComisionesTemporal]
(
[Id] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[IdCliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[Transaccion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[Documento] [decimal] (18, 0) NULL,
[Linea] [smallint] NULL,
[TranCancela] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[DocCancela] [decimal] (18, 0) NULL,
[LineaCancela] [smallint] NULL,
[FechaIngreso] [datetime] NULL,
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[Monto] [decimal] (18, 2) NULL,
[PorFactura] [decimal] (18, 4) NULL,
[Impuesto] [decimal] (18, 2) NULL,
[Orden] [bigint] NULL,
[Producto] [nvarchar] (12) COLLATE Modern_Spanish_CI_AS NULL,
[MontoReal] [decimal] (18, 2) NULL,
[PeriodoDesde] [datetime] NULL,
[PeriodoHasta] [datetime] NULL,
[FechaRegistro] [datetime] NULL,
[UsuarioRegistro] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NULL,
[IdSolicitud] [numeric] (18, 0) NULL CONSTRAINT [DF_PCComisionesTemporal_IdSolicitud] DEFAULT ((0)),
[BsComision] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_BsComision] DEFAULT ((0)),
[BsComisionGrte] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_BsComisionGrte] DEFAULT ((0)),
[FichaGteProducto] [bigint] NULL CONSTRAINT [DF_PCComisionesTemporal_FichaGteProducto] DEFAULT ((0)),
[BsComisionGteProducto] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_BsComisionGteProducto] DEFAULT ((0)),
[FlagParcial] [bit] NULL CONSTRAINT [DF_PCComisionesTemporal_FlagParcial] DEFAULT ((0)),
[RecNumPadre] [numeric] (18, 0) NULL CONSTRAINT [DF_PCComisionesTemporal_RecNumPadre] DEFAULT ((0)),
[PorcFlat] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcFlat] DEFAULT ((0)),
[ComisionRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_ComisionRangoCumplimientoCuotaGeneral] DEFAULT ((0)),
[PorcRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcRangoCumplimientoCuotaGeneral] DEFAULT ((0)),
[PorcCantidadCuotasCumplidas] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcCantidadCuotasCumplidas] DEFAULT ((0)),
[ComisionCantidadCuotasCumplidas] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_ComisionCantidadCuotasCumplidas] DEFAULT ((0)),
[CantidadCuotasCumplidas] [int] NULL CONSTRAINT [DF_PCComisionesTemporal_CantidadCuotasCumplidas] DEFAULT ((0)),
[TotalVentasMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_TotalVentasMes] DEFAULT ((0)),
[TotalCuotaMes] [decimal] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_TotalCuotaMes] DEFAULT ((0)),
[IdTipoPago] [int] NULL,
[Desde] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCComisionesTemporal_Desde] DEFAULT (''),
[Hasta] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_PCComisionesTemporal_Hasta] DEFAULT (''),
[PorcFlatGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcFlat1] DEFAULT ((0)),
[ComisionRangoCumplimientoCuotaGeneralGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_ComisionRangoCumplimientoCuotaGeneral1] DEFAULT ((0)),
[PorcRangoCumplimientoCuotaGeneralGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcRangoCumplimientoCuotaGeneral1] DEFAULT ((0)),
[PorcCantidadCuotasCumplidasGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_PorcCantidadCuotasCumplidas1] DEFAULT ((0)),
[ComisionCantidadCuotasCumplidasGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_ComisionCantidadCuotasCumplidas1] DEFAULT ((0)),
[CantidadCuotasCumplidasGerente] [int] NULL CONSTRAINT [DF_PCComisionesTemporal_CantidadCuotasCumplidas1] DEFAULT ((0)),
[TotalVentasMesGerente] [decimal] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_TotalVentasMes1] DEFAULT ((0)),
[TotalCuotaMesGerente] [decimal] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_TotalCuotaMes1] DEFAULT ((0)),
[IdGerente] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[BsComisionNuevoReactivado] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_BsComisionNuevoReactivado] DEFAULT ((0)),
[BsComisionNuevoReactivadoGerente] [numeric] (18, 2) NULL CONSTRAINT [DF_PCComisionesTemporal_BsComisionNuevoReactivadoGerente] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[PCComisionesTemporal] ADD CONSTRAINT [PK_PCComisionesTemporal] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[PCComisionesTemporal] ADD CONSTRAINT [FK_PCComisionesTemporal_PCTipoPago] FOREIGN KEY ([IdTipoPago]) REFERENCES [dbo].[PCTipoPago] ([Id])
GO