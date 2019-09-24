CREATE TABLE [dbo].[PCOrdenesPignoradas]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Orden] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[Cotizacion] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[NombreVendedor] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[IdCliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[NombreCliente] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCOrdenesPignoradas] ADD CONSTRAINT [PK_PCOrdenesPignoradas] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCOrdenesPignoradas_1] ON [dbo].[PCOrdenesPignoradas] ([Cotizacion])
GO
CREATE NONCLUSTERED INDEX [IX_PCOrdenesPignoradas] ON [dbo].[PCOrdenesPignoradas] ([Orden])
GO
