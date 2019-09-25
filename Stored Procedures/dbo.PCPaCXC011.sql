SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

Create PROCEDURE [dbo].[PCPaCXC011] 
	@Desde nvarchar(12), @Hasta nvarchar(12)
AS
BEGIN
		
	--/*BUSCAMOS LOS RECIBOS DE COBRO SOBRE ANTICIPOS*/
	SELECT FECHA_INGRESO AS FechaIngreso, CLIENTE AS IdCliente, TRANSACCION AS Transaccion,
	DOCUMENTO AS Documento, LINEA, TIPO_CANC AS TranCancela, DOC_CANC AS DocCancela, LIN_CANC AS LineaCancela,
	MONTO AS Monto, 'ANTICIPOS' AS Tipo
	FROM CXC.dbo.WARY074
	WHERE
	EXISTS (SELECT * FROM dbo.WSMY477 WHERE (Anticipo = CXC.dbo.WARY074.DOCUMENTO) AND (Linea = CXC.dbo.WARY074.Linea))
	AND (FechaComision >= CONVERT(DATETIME,@Desde, 102))
	AND (FechaComision <= CONVERT(DATETIME, @Hasta, 102))
	AND (TRANSACCION = 'AN')
	AND ((SELECT TOP 1 DOCUMENTO FROM dbo.WSMY675H WHERE Documento = cxc.dbo.wary074.documento AND Linea = cxc.dbo.wary074.linea AND Transaccion = cxc.dbo.wary074.TRANSACCION) IS NULL)
	
	UNION ALL
	
	--/*BUSCAMOS LOS RECIBOS DE COBRO SOBRE RECIBOS DE COBROS Y RETENCIONES*/
	SELECT FECHA_INGRESO AS FechaIngreso, CLIENTE AS IdCliente, TRANSACCION AS Transaccion,
	DOCUMENTO AS Documento, LINEA, TIPO_CANC AS TranCancela, DOC_CANC AS DocCancela, LIN_CANC AS LineaCancela,
	MONTO AS Monto, 'RECIBOS-FA' AS Tipo
	FROM CXC.dbo.WARY074
	WHERE
	(FechaComision >= CONVERT(DATETIME,@Desde, 102))
	AND (FechaComision <= CONVERT(DATETIME, @Hasta, 102))
	AND (TRANSACCION = 'RC' OR TRANSACCION = 'RI')
	AND (TIPO_CANC = 'FA')
	AND ((SELECT TOP 1 DOCUMENTO FROM dbo.WSMY675H WHERE Documento = cxc.dbo.wary074.documento AND Linea = cxc.dbo.wary074.linea AND Transaccion = cxc.dbo.wary074.TRANSACCION) IS NULL)
	
	UNION ALL

	
	--/*BUSCAMOS LOS RECIBOS DE COBRO SOBRE CHEQUES DEVUELTOS*/
	SELECT FechaComision AS FechaIngreso, CLIENTE AS IdCliente, TRANSACCION AS Transaccion,
	DOCUMENTO AS Documento, LINEA, TIPO_CANC AS TranCancela, DOC_CANC AS DocCancela, LIN_CANC AS LineaCancela,
	MONTO AS Monto, 'RECIBOS-CD' AS Tipo
	FROM CXC.dbo.WARY074
	WHERE
	(FechaComision >= CONVERT(DATETIME,@Desde, 102))
	AND (FechaComision <= CONVERT(DATETIME, @Hasta, 102))
	AND (TRANSACCION = 'RC' OR TRANSACCION = 'RI')
	AND (TIPO_CANC = 'CD')
	AND ((SELECT TOP 1 DOCUMENTO FROM dbo.WSMY675H WHERE Documento = cxc.dbo.wary074.documento AND Linea = cxc.dbo.wary074.linea AND Transaccion = cxc.dbo.wary074.TRANSACCION) IS NULL) 
	
	ORDER BY FechaIngreso
	
END
GO
