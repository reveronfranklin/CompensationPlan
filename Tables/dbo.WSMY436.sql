CREATE TABLE [dbo].[WSMY436]
(
[IdCategoria] [int] NOT NULL,
[Descripcion] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[IdPrint] [int] NULL CONSTRAINT [DF_WSMY436_IdPrint] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[WSMY436] ADD CONSTRAINT [PK_WSMY436] PRIMARY KEY CLUSTERED  ([IdCategoria])
GO
