CREATE TABLE [dbo].[PCProducto]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Producto] [nvarchar] (12) COLLATE Modern_Spanish_CI_AS NULL,
[IdSubcategoria] [int] NULL
)
GO
ALTER TABLE [dbo].[PCProducto] ADD CONSTRAINT [PK_PCProducto] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCProducto] ON [dbo].[PCProducto] ([Producto])
GO
