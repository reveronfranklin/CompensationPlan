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
	
	DECLARE @Año AS INT, @Mes AS INT,@ordenBusqueda AS BIGINT,@FechaOrden SMALLDATETIME,@Cotizacion  NVARCHAR(20),@IdVendedor AS NVARCHAR(4),@NombreVededor NVARCHAR(50),@IdCliente NVARCHAR(10),@NombreCliente AS NVARCHAR(50)
	SET @ordenBusqueda=CAST(@Orden AS BIGINT)
	SET @Año = isnull((SELECT YEAR(FEC_REG_VENTAS) FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden),(select year(fecha_pedido) from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @Mes = isnull((SELECT Month(FEC_REG_VENTAS) FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden) ,(select month(fecha_pedido) from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @FechaOrden = isnull((SELECT FEC_REG_VENTAS FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden) ,(select fecha_pedido from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @Cotizacion= isnull((SELECT COTIZACION FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden) ,(select orden from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @IdVendedor=isnull((SELECT VENDEDOR FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden) ,(select VENDEDOR from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @NombreVededor=(SELECT NOMBRE FROM Maestros.dbo.CSMY005 WHERE CODIGO=@IdVendedor)
	SET @IdCliente=isnull((SELECT CLIENTE FROM  PLANTA.dbo.CPRY012 where ORDEN = @Orden) ,(select CLIENTE from pedidos.dbo.ctmy045 where orden=@Orden))
	SET @NombreCliente=(SELECT Nombre FROM clientes.dbo.csmy003 WHERE Codigo=@IdCliente)
	DELETE FROM PCAñoMesOrden WHERE orden=@Orden
	INSERT INTO PCAñoMesOrden(Orden,Año,Mes,FechaOrden,cotizacion,IdVendedor,NombreVendedor,IdCliente,NombreCliente)
	SELECT @Orden,@Año,@Mes,@FechaOrden,@Cotizacion,@IdVendedor,@NombreVededor,@IdCliente,@NombreCliente
	

END
GO
