CREATE TABLE [dbo].[PCTasaAñoMes]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Año] [int] NULL,
[Mes] [int] NULL,
[Tasa] [decimal] (18, 2) NULL
)
GO
ALTER TABLE [dbo].[PCTasaAñoMes] ADD CONSTRAINT [PK_TasaAñoMes] PRIMARY KEY CLUSTERED  ([Id])
GO
