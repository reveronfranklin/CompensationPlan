CREATE TABLE [dbo].[PCVendedor]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[IdVendedor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[Nombre] [nvarchar] (30) COLLATE Modern_Spanish_CI_AS NULL,
[CodOficina] [int] NULL,
[Activo] [bit] NULL,
[Ficha] [nvarchar] (6) COLLATE Modern_Spanish_CI_AS NULL,
[Supervisor] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[Gerente] [bit] NULL,
[NoPagarComision] [bit] NULL
)
GO
