CREATE TABLE [dbo].[WSMY685]
(
[IdPago] [bigint] NOT NULL IDENTITY(1, 1),
[Transaccion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Orden] [bigint] NOT NULL,
[Producto] [nvarchar] (12) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Monto] [numeric] (18, 2) NOT NULL,
[MontoGte] [numeric] (18, 2) NOT NULL,
[MontoGteProducto] [numeric] (18, 2) NOT NULL,
[FichaGteProducto] [bigint] NOT NULL,
[Observaciones] [nvarchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaActualiza] [datetime] NOT NULL,
[FlagPagado] [bit] NOT NULL,
[FechaPagado] [datetime] NULL,
[RMonto] [numeric] (18, 2) NULL,
[RMontoGte] [numeric] (18, 2) NULL,
[RMontoGteProducto] [numeric] (18, 2) NULL,
[OrdenString] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL
)
GO
