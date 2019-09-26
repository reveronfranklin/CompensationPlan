SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpActualizaCuotaVentas]
	@Año int,@Mes int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	    UPDATE  PCCuotaVentas SET TasaUsd=(SELECT tasa FROM dbo.PCTasaAñoMes WHERE PCTasaAñoMes.Año=PCCuotaVentas.Año AND PCTasaAñoMes.Mes=PCCuotaVentas.Mes)  WHERE Año=@Año AND mes=@mes
		UPDATE  PCCuotaVentas SET cuota= CuotaUsd*TasaUsd  WHERE Año=@Año AND mes=@mes
		UPDATE  PCCuotaVentas SET ventausd= venta/TasaUsd  WHERE Año=@Año AND mes=@mes
		UPDATE  pcCuotaVentas set PorcCumplimiento=0  WHERE Año=@Año AND mes=@mes
    	UPDATE  PCCuotaVentas set venta=isnull((SELECT  SUM(VALOR_VENTA) FROM   estadisticas.dbo.VENTAS WHERE        (ALO_CONTABLE = Año) AND (MES_CONTABLE = Mes) AND (estadisticas.dbo.VENTAS.VENDEDOR = pccuotaventas.Vendedor) AND (IdCuotaComision = IdProductoCuota)),0) WHERE Año=@Año AND mes=@mes
	    UPDATE  pcCuotaVentas set PorcCumplimiento=(venta*100)/cuota WHERE Año=@Año AND mes=@mes AND cuota<>0
		UPDATE  pcCuotaVentas SET NombreVendedor=(SELECT Nombre FROM maestros.dbo.csmy005 WHERE codigo=Vendedor) WHERE Año=@Año AND mes=@mes
        UPDATE  pcCuotaVentas SET DescripcionCuota=(SELECT Descripcion FROM PCProductoCuota WHERE PCProductoCuota .Id=pcCuotaVentas.IdProductoCuota) WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET CuotaString=	[dbo].[FN_FORMAT_NUMBER](Cuota,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET VentaString=	[dbo].[FN_FORMAT_NUMBER](Venta,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET CuotaUsdString=	[dbo].[FN_FORMAT_NUMBER](CuotaUsd,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET VentaUsdString=	[dbo].[FN_FORMAT_NUMBER](VentaUsd,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET TasaUsdString=	[dbo].[FN_FORMAT_NUMBER](TasaUsd,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		UPDATE pcCuotaVentas SET Supervisor=(SELECT supervisor FROM maestros.dbo.CSMY005 WHERE CODIGO=Vendedor) WHERE Año=@Año AND mes=@mes
		DELETE FROM [PCCuotaVentasGerente] WHERE  (Año = @Año) AND (Mes = @mes)
		INSERT INTO [PCCuotaVentasGerente](Año,Mes,Gerente,IdProductoCuota,Cuota,Venta)

		SELECT        Año, Mes, Supervisor, IdProductoCuota, SUM(Cuota) AS Cuota, SUM(Venta) AS Venta
		FROM            PCCuotaVentas
		GROUP BY Año, Mes, Supervisor, IdProductoCuota
		HAVING        (Año = @Año) AND (Mes = @mes)

		UPDATE  [PCCuotaVentasGerente] set PorcCumplimiento=(venta*100)/cuota WHERE Año=@Año AND mes=@mes and cuota <>0

		UPDATE  [PCCuotaVentasGerente] set  DescripcionCuota=(SELECT Descripcion FROM PCProductoCuota WHERE PCProductoCuota .Id=[PCCuotaVentasGerente].IdProductoCuota) WHERE Año=@Año AND mes=@mes 

		UPDATE [PCCuotaVentasGerente] SET CuotaString=	[dbo].[FN_FORMAT_NUMBER](Cuota,'2',',','.','','') WHERE Año=@Año AND mes=@mes
		
		UPDATE [PCCuotaVentasGerente] SET VentaString=	[dbo].[FN_FORMAT_NUMBER](Venta,'2',',','.','','') WHERE Año=@Año AND mes=@mes
END

GO
