CREATE TABLE [dbo].[PCTipoPago]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TipoPago] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[Descripcion] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[FlagCalcular] [bit] NOT NULL CONSTRAINT [DF_PCTipoPago_FlagCalcular] DEFAULT ((0)),
[AplicaGerente] [bit] NULL,
[ConceptoNominaPago] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[ConceptoNominaDescuento] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PCTipoPago] ADD CONSTRAINT [PK_PCTipoPago] PRIMARY KEY CLUSTERED  ([Id])
GO
