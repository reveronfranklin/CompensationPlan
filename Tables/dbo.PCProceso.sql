CREATE TABLE [dbo].[PCProceso]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PeriodoDesde] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[PeriodoHasta] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[TotalReg] [int] NULL CONSTRAINT [DF_PCProceso_TotalReg] DEFAULT ((0)),
[RegistrosProcesados] [int] NULL,
[Iniciado] [bit] NULL CONSTRAINT [DF_PCProceso_Iniciado] DEFAULT ((0)),
[Culminado] [bit] NULL CONSTRAINT [DF_PCProceso_Culminado] DEFAULT ((0)),
[FechaRegistro] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[UsuarioRegistro] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[Desde] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[Hasta] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[Accion] [int] NULL CONSTRAINT [DF_PCProceso_Accion] DEFAULT ((0)),
[RegistrosCerrados] [int] NULL,
[PeriodoCerrado] [bit] NULL,
[IdPeriodo] [int] NULL
)
GO
