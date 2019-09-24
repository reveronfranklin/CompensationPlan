CREATE TABLE [dbo].[PCCliente]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Cliente] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Nombre] [nvarchar] (80) COLLATE Modern_Spanish_CI_AS NOT NULL,
[F_Apertura] [smalldatetime] NULL,
[F_Ultm_Compra] [smalldatetime] NULL,
[FechaReactivado] [smalldatetime] NULL
)
GO
ALTER TABLE [dbo].[PCCliente] ADD CONSTRAINT [PK_PCCliente] PRIMARY KEY CLUSTERED  ([Id])
GO
CREATE NONCLUSTERED INDEX [IX_PCCliente] ON [dbo].[PCCliente] ([Cliente])
GO
