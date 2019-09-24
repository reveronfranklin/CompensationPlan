CREATE TABLE [dbo].[WSMY696]
(
[Recnum] [numeric] (18, 0) NOT NULL IDENTITY(1, 1),
[Ficha] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NOT NULL,
[IdSubCategoria] [int] NOT NULL,
[PorcCoberturaDesde] [numeric] (18, 3) NOT NULL,
[PorcCoberturaHasta] [numeric] (18, 3) NOT NULL,
[PorcComision] [numeric] (18, 3) NOT NULL,
[UsuarioActualiza] [nvarchar] (40) COLLATE Modern_Spanish_CI_AS NULL,
[FechaActualiza] [datetime] NULL
)
GO
ALTER TABLE [dbo].[WSMY696] ADD CONSTRAINT [PK_WSMY696] PRIMARY KEY CLUSTERED  ([Recnum])
GO
ALTER TABLE [dbo].[WSMY696] ADD CONSTRAINT [IX_WSMY696_1] UNIQUE NONCLUSTERED  ([Ficha], [IdSubCategoria], [PorcComision], [PorcCoberturaDesde], [PorcCoberturaHasta])
GO
ALTER TABLE [dbo].[WSMY696] ADD CONSTRAINT [FK_WSMY696_WSMY437] FOREIGN KEY ([IdSubCategoria]) REFERENCES [dbo].[WSMY437] ([IdSubCategoria])
GO
