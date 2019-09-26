CREATE TABLE [dbo].[PCSysfile]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ToleranciaDesde] [numeric] (18, 2) NULL,
[ToleranciaHasta] [numeric] (18, 2) NULL,
[PorcCunplimiento] [numeric] (18, 2) NULL,
[DiasClienteNuevo] [int] NULL,
[DiasClienteReactivado] [int] NULL,
[UmbralOrdenesPignoradas] [numeric] (18, 2) NULL,
[DiasPagoDoble] [int] NULL
)
GO
