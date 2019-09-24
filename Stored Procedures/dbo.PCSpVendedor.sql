SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpVendedor]
	 @parametro nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	INSERT INTO PCVendedor(IdVendedor, Nombre,CodOficina,Activo,supervisor,NoPagarComision)
	SELECT codigo,nombre,oficina,CASE WHEN FLAG_RETIRADO= 'X' THEN 0 ELSE 1 END,SUPERVISOR ,NoPagarComision FROM maestros.dbo.csmy005
	WHERE Codigo NOT IN (SELECT IdVendedor FROM [PCVendedor])

	UPDATE PCVendedor SET nombre=(SELECT nombre FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
	UPDATE PCVendedor SET CodOficina=(SELECT oficina FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
	UPDATE PCVendedor SET Supervisor=(SELECT SUPERVISOR FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
	UPDATE PCVendedor SET NoPagarComision=(SELECT NoPagarComision FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
	UPDATE PCVendedor SET Activo=(SELECT CASE WHEN FLAG_RETIRADO= 'X' THEN 0 ELSE 1 END FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
	UPDATE PCVendedor SET Gerente=(SELECT CASE WHEN FLAG_DE_GERENTE= 'X' THEN 1 ELSE 0 END FROM maestros.dbo.csmy005 WHERE maestros.dbo.csmy005.codigo=PCVendedor.IdVendedor)
END
GO
