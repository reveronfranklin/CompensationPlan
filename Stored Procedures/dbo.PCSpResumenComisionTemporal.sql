SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpResumenComisionTemporal]
	 @parametro nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE from PCResumenComisionTemporal
	DELETE FROM PCResumenOficinaTemporal
DECLARE @Periodo AS NVARCHAR(50)
SELECT TOP 1 @Periodo ='Periodo desde: ' + PeriodoDesde + ' Hasta: ' + PeriodoHasta FROM dbo.PCTemporal
DELETE FROM [dbo].[PCTemporal] WHERE Idvendedor=''
INSERT INTO PCResumenComisionTemporal(CodigoVendedor,Monto)
SELECT IdVendedor,SUM(BsComision) FROM [dbo].[PCTemporal]
GROUP BY IdVendedor
UPDATE PCResumenComisionTemporal SET Periodo=@Periodo
UPDATE PCResumenComisionTemporal SET CodigoOficina=(SELECT CodOficina FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor)
UPDATE PCResumenComisionTemporal SET NombreOficina=(SELECT nombreOficina FROM  [dbo].[PCOficina] WHERE CodOficina=CodigoOficina)
UPDATE PCResumenComisionTemporal SET NombreVendedor=(SELECT nombre FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor)
UPDATE PCResumenComisionTemporal SET Ficha=(SELECT Ficha FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor)
UPDATE PCResumenComisionTemporal SET montostring=	[dbo].[FN_FORMAT_NUMBER](monto,'2',',','.','','')
INSERT INTO PCResumenOficinaTemporal(CodigoOficina,NombreOficina,Monto)
SELECT CodigoOficina,NombreOficina,SUM(Monto) FROM PCResumenComisionTemporal GROUP BY CodigoOficina,NombreOficina
UPDATE PCResumenOficinaTemporal SET montostring=	[dbo].[FN_FORMAT_NUMBER](monto,'2',',','.','','')

END



GO
