CREATE TABLE [dbo].[WSMY683]
(
[Recnum] [numeric] (18, 0) NOT NULL,
[Transaccion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Documento] [numeric] (10, 0) NOT NULL,
[Linea] [smallint] NOT NULL,
[IdCliente] [int] NOT NULL,
[NroRC] [bigint] NOT NULL,
[FlagReversado] [bit] NOT NULL,
[FechaReversado] [datetime] NULL,
[FechaActualiza] [datetime] NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
