SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS OFF
GO
/*CONSULTA CONSOLIDADA DE LOS PAGOS DE COMISIONES ACTUALES POR VENDEDOR*/
CREATE procedure [dbo].[PCPaCXC013]
@Desde Varchar(10),
@Hasta Varchar(10)
AS

/*************************PAGO DE COMISIONES ************************************************/
SELECT Maestros.dbo.CSMY005.FICHA AS Ficha, ConceptoNomina AS Clave, Maestros.dbo.CSMY005.CODIGO AS Codigo,
SUM(dbo.PCHistorico.BsComision) AS NetoPagado, dbo.PCHistorico.PeriodoDesde AS Desde, dbo.PCHistorico.PeriodoHasta AS Hasta
FROM dbo.PCHistorico INNER JOIN Maestros.dbo.CSMY005 ON dbo.PCHistorico.IdVendedor = Maestros.dbo.CSMY005.CODIGO
WHERE dbo.PCHistorico.PeriodoDesde =  @Desde
AND dbo.PCHistorico.PeriodoHasta = @Hasta
AND (dbo.PCHistorico.BsComision >= 0)
AND (Maestros.dbo.CSMY005.ACTIVO = 'X')
GROUP BY Maestros.dbo.CSMY005.FICHA, Maestros.dbo.CSMY005.CODIGO,  ConceptoNomina,dbo.PCHistorico.PeriodoDesde, dbo.PCHistorico.PeriodoHasta
ORDER BY Clave
GO
