CREATE TABLE [dbo].[PCProductoCuota]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Descripcion] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCProductoCuota] ADD CONSTRAINT [PK_PCProductoCuota] PRIMARY KEY CLUSTERED  ([Id])
GO
