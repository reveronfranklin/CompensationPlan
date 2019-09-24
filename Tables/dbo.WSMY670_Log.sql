CREATE TABLE [dbo].[WSMY670_Log]
(
[Recnum] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[IdSubCategoria] [int] NOT NULL,
[PorcComision] [numeric] (18, 3) NOT NULL,
[PorcCoberturaDesde] [numeric] (18, 3) NOT NULL,
[PorcCoberturaHasta] [numeric] (18, 3) NOT NULL,
[UsuarioCrea] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaRegistro] [datetime] NOT NULL CONSTRAINT [DF_WSMY670_Log_FechaRegistro] DEFAULT (getdate()),
[UsuarioModifica] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NULL,
[FechaModifica] [datetime] NULL,
[Accion] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL
)
GO
ALTER TABLE [dbo].[WSMY670_Log] ADD CONSTRAINT [PK_WSMY670_Log] PRIMARY KEY CLUSTERED  ([Recnum])
GO
CREATE NONCLUSTERED INDEX [IX_WSMY670_Log_1] ON [dbo].[WSMY670_Log] ([IdSubCategoria], [PorcComision], [PorcCoberturaDesde], [PorcCoberturaHasta])
GO
ALTER TABLE [dbo].[WSMY670_Log] ADD CONSTRAINT [FK_WSMY670_Log_WSMY437] FOREIGN KEY ([IdSubCategoria]) REFERENCES [dbo].[WSMY437] ([IdSubCategoria])
GO
