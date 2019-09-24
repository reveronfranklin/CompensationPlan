CREATE TABLE [dbo].[auxiliarcuota]
(
[codigo_oficina] [int] NOT NULL,
[idcategoria] [int] NOT NULL,
[idsubcategoria] [int] NOT NULL,
[porcentaje] [numeric] (18, 2) NULL,
[idproductocuota] [int] NULL
)
GO
ALTER TABLE [dbo].[auxiliarcuota] ADD CONSTRAINT [PK_auxiliarcuota] PRIMARY KEY CLUSTERED  ([codigo_oficina], [idcategoria], [idsubcategoria])
GO
ALTER TABLE [dbo].[auxiliarcuota] ADD CONSTRAINT [FK_auxiliarcuota_WSMY437] FOREIGN KEY ([idsubcategoria]) REFERENCES [dbo].[WSMY437] ([IdSubCategoria])
GO
