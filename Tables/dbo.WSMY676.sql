CREATE TABLE [dbo].[WSMY676]
(
[RecNum] [numeric] (18, 0) NOT NULL,
[PeriodoDesde] [datetime] NOT NULL,
[PeriodoHasta] [datetime] NOT NULL,
[TotalRegPaCXC011] [int] NOT NULL,
[TotalRegPaCXC012] [int] NOT NULL,
[RegistrosProcesados] [int] NOT NULL,
[FlagCorriendo] [bit] NOT NULL,
[FlagHistorico] [bit] NOT NULL,
[FechaHistorico] [datetime] NULL,
[FechaRegistro] [datetime] NOT NULL,
[UsuarioRegistro] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
