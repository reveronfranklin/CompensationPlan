CREATE TABLE [dbo].[PCFlatComision]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Descripcion] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Porcentaje] [decimal] (18, 2) NOT NULL
)
GO
ALTER TABLE [dbo].[PCFlatComision] ADD CONSTRAINT [PK_FlatComision] PRIMARY KEY CLUSTERED  ([Id])
GO
