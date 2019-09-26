CREATE TABLE [dbo].[PCFlatComisionGerente]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[IdFlatComision] [int] NULL,
[Gerente] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[DescripcionCategoria] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NULL,
[Porcentaje] [numeric] (18, 2) NULL,
[NombreGerente] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCFlatComisionGerente] ADD CONSTRAINT [PK_FlatComisionGerente] PRIMARY KEY CLUSTERED  ([Id])
GO
