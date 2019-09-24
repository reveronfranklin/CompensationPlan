SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpActualizaCliente]
	@Cliente NVARCHAR(10)
AS
BEGIN

DELETE FROM PCCliente WHERE cliente=@Cliente
INSERT INTO PCCliente (Cliente, Nombre, F_Apertura, F_Ultm_Compra, FechaReactivado)
SELECT codigo,nombre,F_Apertura, F_Ultm_Compra, FechaReactivado FROM clientes.dbo.csmy003 WHERE LTRIM(RTRIM(codigo))=@Cliente

END
GO
