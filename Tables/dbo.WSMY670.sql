CREATE TABLE [dbo].[WSMY670]
(
[Recnum] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[IdSubCategoria] [int] NOT NULL,
[PorcComision] [numeric] (18, 3) NOT NULL,
[PorcCoberturaDesde] [numeric] (18, 3) NOT NULL,
[PorcCoberturaHasta] [numeric] (18, 3) NOT NULL,
[UsuarioCrea] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NOT NULL,
[FechaRegistro] [datetime] NOT NULL CONSTRAINT [DF_WSMY670_FechaRegistro] DEFAULT (getdate()),
[UsuarioModifica] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NULL,
[FechaModifica] [datetime] NULL
)
GO
ALTER TABLE [dbo].[WSMY670] ADD CONSTRAINT [PK_WSMY670] PRIMARY KEY CLUSTERED  ([Recnum])
GO
CREATE NONCLUSTERED INDEX [IX_WSMY670] ON [dbo].[WSMY670] ([IdSubCategoria], [PorcCoberturaDesde], [PorcCoberturaHasta])
GO
ALTER TABLE [dbo].[WSMY670] ADD CONSTRAINT [IX_WSMY670_1] UNIQUE NONCLUSTERED  ([IdSubCategoria], [PorcComision], [PorcCoberturaDesde], [PorcCoberturaHasta])
GO
ALTER TABLE [dbo].[WSMY670] ADD CONSTRAINT [FK_WSMY670_WSMY437] FOREIGN KEY ([IdSubCategoria]) REFERENCES [dbo].[WSMY437] ([IdSubCategoria])
GO
