CREATE TABLE [dbo].[WSMY678]
(
[IdSysFile] [smallint] NOT NULL,
[ValorExcesoCobertura] [numeric] (18, 2) NOT NULL,
[ValorExcesoCoberturaGte] [numeric] (18, 2) NOT NULL,
[ValorMaxExcesoCobertura] [numeric] (18, 2) NOT NULL,
[PorcExcesoCuota] [numeric] (18, 2) NOT NULL,
[PorcExcesoCuotaGte] [numeric] (18, 2) NOT NULL,
[DiasClienteNuevo] [int] NOT NULL,
[DiasClienteReactivado] [int] NOT NULL,
[FactorClienteNuevo] [numeric] (18, 3) NOT NULL,
[FactorClienteNuevoGte] [numeric] (18, 3) NOT NULL,
[CuotaCliente] [smallint] NOT NULL,
[CuotaClienteGte] [smallint] NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaActualiza] [datetime] NOT NULL
)
GO
