SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCAñoOrden] 
	-- Add the parameters for the stored procedure here
	 @Orden as NVARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	DELETE FROM PCAñoMesOrden WHERE orden=@Orden
	INSERT INTO PCAñoMesOrden(Orden,Año,Mes,FechaOrden,cotizacion,IdVendedor,NombreVendedor,IdCliente,NombreCliente,producto)
	SELECT Orden,ALO_CONTABLE,MES_CONTABLE,FECHA_INGRESO,COTIZACION,VENDEDOR,(SELECT NOMBRE FROM Maestros.dbo.CSMY005 WHERE CODIGO=VENDEDOR),CLIENTE,NOMBRE,PRODUCTO FROM estadisticas.dbo.VENTAS WHERE orden=@Orden AND CORRELATIVO=0
END
GO
