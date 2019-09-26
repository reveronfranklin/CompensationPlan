CREATE TABLE [dbo].[PCTemporalBk]
(
[Recnum] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[IdCliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Transaccion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Documento] [decimal] (18, 0) NOT NULL,
[Linea] [smallint] NOT NULL,
[TranCancela] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NOT NULL,
[DocCancela] [decimal] (18, 0) NOT NULL,
[LineaCancela] [smallint] NOT NULL,
[FechaIngreso] [datetime] NOT NULL,
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Monto] [decimal] (18, 2) NOT NULL,
[PorFactura] [decimal] (18, 4) NOT NULL,
[Impuesto] [decimal] (18, 2) NOT NULL,
[Orden] [bigint] NOT NULL,
[Producto] [nvarchar] (12) COLLATE Modern_Spanish_CI_AS NOT NULL,
[MontoReal] [decimal] (18, 2) NOT NULL,
[PeriodoDesde] [datetime] NOT NULL,
[PeriodoHasta] [datetime] NOT NULL,
[FechaRegistro] [datetime] NOT NULL,
[UsuarioRegistro] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdSolicitud] [numeric] (18, 0) NOT NULL,
[BsComision] [numeric] (18, 2) NOT NULL,
[BsComisionGrte] [numeric] (18, 2) NOT NULL,
[FichaGteProducto] [bigint] NOT NULL,
[BsComisionGteProducto] [numeric] (18, 2) NOT NULL,
[FlagParcial] [bit] NOT NULL,
[RecNumPadre] [numeric] (18, 0) NOT NULL,
[PorcFlat] [numeric] (18, 2) NULL,
[ComisionRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL,
[PorcRangoCumplimientoCuotaGeneral] [numeric] (18, 2) NULL,
[PorcCantidadCuotasCumplidas] [numeric] (18, 2) NULL,
[ComisionCantidadCuotasCumplidas] [numeric] (18, 2) NULL,
[CantidadCuotasCumplidas] [int] NULL
)
GO
ALTER TABLE [dbo].[PCTemporalBk] ADD CONSTRAINT [PK_PCTemporal] PRIMARY KEY CLUSTERED  ([Recnum])
GO
