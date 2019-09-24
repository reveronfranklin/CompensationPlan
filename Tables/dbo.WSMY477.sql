CREATE TABLE [dbo].[WSMY477]
(
[Orden] [numeric] (18, 0) NOT NULL,
[Cotizacion] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Anticipo] [bigint] NOT NULL,
[Linea] [smallint] NOT NULL,
[FechaCreacion] [datetime] NOT NULL,
[Usuario] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
