SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[PCPaNpcETL_CXC_Comisiones]
    @Desde AS nvarchar(10),
    @Hasta AS nvarchar(10),
    @Usuario as nvarchar(40)
    
AS

	--VARIABLES DEL PROCEDIMIENTO--------------------------------------------------------------------------
	-------------------------------------------------------------------------------------------------------
	Declare @Id676 numeric(18,0), @MontoReal decimal(18,2), @DocBusqueda numeric(10,0)

	--DECLARAMOS TABLAS TEMPORALES--------------------------------------------------------------------------
	Declare @RegCob as Int, @ContCob as int, @FechaIngreso datetime, @IdCliente int, @Transaccion nvarchar(2),
	@Documento numeric(10,0), @Linea smallint, @TranCancela as nvarchar(2),@DocCancela as numeric(10,0),
	@LineaCancela smallint, @Monto decimal(18,2), @Tipo nvarchar(50)
	
	Declare @Cobranza Table (ID int Identity(1,1), FechaIngreso datetime, IdCliente int, Transaccion nvarchar(2),
	Documento numeric(10,0), Linea smallint, TranCancela nvarchar(2), DocCancela numeric(10,0), LineaCancela smallint,
	Monto decimal(18,2), Tipo nvarchar(50))
		
	Declare @RegFact Int, @ContFact as int, @Orden Bigint, @IdVendedor nvarchar(4), @PorFactura decimal(18,4),
	@IdProducto nvarchar(12), @Iva decimal(18,2), @IdSubCategoria as Smallint, @Mensaje nvarchar(MAX)

	DECLARE @Fecharegistro DATETIME,@DesdeProceso AS NVARCHAR(10),@Hastaproceso AS NVARCHAR(10)

BEGIN

    SET NOCOUNT ON;

		SET @Fecharegistro=GETDATE()
		
		/****************BLANQUEAMOS LA TABLA TEMPORAL DE COMISIONES***********************************/
		TRUNCATE TABLE PCComisionesTemporal
		TRUNCATE TABLE PCTemporal

		/**********************************************************************************************/
		
		/****************************PASAMOS LOS PAGOS MANUALES****************************************/
		INSERT INTO
		PCComisionesTemporal
		(IdCliente, Transaccion, Documento, Linea, TranCancela, DocCancela, LineaCancela,
		FechaIngreso, IdVendedor, Monto, PorFactura, Impuesto, Orden, 
		Producto, MontoReal, PeriodoDesde, PeriodoHasta, FechaRegistro, UsuarioRegistro,
		IdSolicitud, BsComision, BsComisionGrte, FichaGteProducto, BsComisionGteProducto,
		FlagParcial, RecNumPadre,IdTipoPago)
		SELECT
		(SELECT CLIENTE FROM Estadisticas.dbo.VENTAS WHERE (CORRELATIVO = 0) AND (ORDEN = Wsmy685.Orden)
		AND (PRODUCTO = Wsmy685.Producto)),
		'PM',IdPago,1,'',0,0,FechaActualiza,
		(SELECT VENDEDOR FROM Estadisticas.dbo.VENTAS WHERE (CORRELATIVO = 0) AND (ORDEN = Wsmy685.Orden)
		AND (PRODUCTO = Wsmy685.Producto)),
		Monto,1,0,Orden,Producto,Monto,CONVERT(DATETIME,@Desde,102),CONVERT(DATETIME,@Hasta,102),getdate(),
		@Usuario,0,Monto,MontoGte, FichaGteProducto, MontoGteProducto, 0,0,1
		From WSMY685 WHERE FlagPagado = 0
		
		/***********************************************************************************************/
    
		/****************PASAMOS LOS CHEQUES QUE AUN NO HAN SIDO REVERSADOS****************************/
		INSERT INTO PCComisionesTemporal (IdCliente, Transaccion,
		Documento, Linea, IdVendedor,Orden, Producto, MontoReal, PeriodoDesde, PeriodoHasta,
		FechaRegistro, BsComision,IdTipoPago)
		SELECT
		PCHistorico.IdCliente, WSMY683.Transaccion, WSMY683.Documento,
		WSMY683.Linea,   PCHistorico.IdVendedor,
		PCHistorico.Orden, PCHistorico.Producto, PCHistorico.MontoReal*-1,
		CONVERT(DATETIME,@Desde,102),CONVERT(DATETIME,@Hasta,102),getdate(),
		PCHistorico.BsComision * - 1 AS BsComision,2
		FROM WSMY683 INNER JOIN PCHistorico ON WSMY683.NroRC = PCHistorico.Documento AND WSMY683.Linea = PCHistorico.Linea
		WHERE (PCHistorico.Transaccion IN (N'RC', N'AN'))
		AND (WSMY683.FlagReversado = 0)
		/**********************************************************************************************/
    
		
		/****************LLENAMOS LA TABLA TEMPORAL DE COBRANZA****************************************/
		INSERT INTO @Cobranza(FechaIngreso,IdCliente,Transaccion,Documento,Linea,TranCancela,DocCancela,
		LineaCancela,Monto,Tipo) EXEC dbo.PCPaCXC011 @Desde, @Hasta
		/**********************************************************************************************/
		
		/****************CREAMOS LOS REGISTROS DE COMISIONES*******************************************/
		SET @RegCob = (select count(*) from @Cobranza)
		SET @ContCob = 1
		WHILE @ContCob <= @RegCob BEGIN	
			SELECT @FechaIngreso = t.FechaIngreso, @IdCliente = t.IdCliente, @Transaccion = t.Transaccion,
			@Documento = t.Documento, @Linea = t.Linea, @TranCancela = t.TranCancela,
			@DocCancela = t.DocCancela, @LineaCancela = LineaCancela, @Monto = t.Monto, @Tipo = t.Tipo
			FROM @Cobranza t WHERE t.ID = @ContCob
		
			IF @Tipo = 'ANTICIPOS' BEGIN	
				Set @DocBusqueda = @Documento
			END
			ELSE BEGIN		
				Set @DocBusqueda = @DocCancela		
			END
		
			/*ELIMINAMOS LA TABLA TEMPORAL EN CASO QUE EXISTA*/
			IF OBJECT_ID('tempdb.dbo.#Factura', 'U') IS NOT NULL BEGIN
				DROP TABLE #Factura; 			
			END
		
			/*CREAMOS LA TABLA TEMPORAL PARA RECCORER LOS DOCUMENTOS DE UN RECIBO DE COBRO*/
			CREATE TABLE #Factura (ID int Identity(1,1),Orden Bigint,IdVendedor nvarchar(4),
			PorFactura decimal(18,4),IdProducto nvarchar(12),Iva decimal(18,2),IdSubCategoria Smallint)
			INSERT INTO #Factura(Orden,IdVendedor,PorFactura,IdProducto,Iva,IdSubCategoria)
			EXEC dbo.PCPaCXC012 @DocBusqueda, @Linea
		
			SET @RegFact = (select count(*) from #Factura)
			SET @ContFact = 1
		
			WHILE @ContFact <= @RegFact BEGIN
				
				SELECT @Orden = f.Orden, @IdVendedor = f.IdVendedor, @PorFactura = f.PorFactura,
				@IdProducto = f.IdProducto, @Iva = f.Iva, @IdSubCategoria = f.IdSubCategoria
				FROM #Factura f WHERE f.ID = @ContFact			
				
				--CALCULAMOS LAS VARIABLES DE PROCESO				
				--MONTO REAL: ES EL MONTO SIN EL IMPUESTO Y PRORRATEADO SEGUN LA PARTICIPACION DEL PRODUCTO
				Set @MontoReal = (@Monto/(1+(@Iva/100)))* @PorFactura
			
				--LLENAMOS LA TABLA DE COMISIONES NUEVO PLAN DE COMPENSACION-----------------------------------
				INSERT INTO	PCComisionesTemporal
				(IdCliente,Transaccion,Documento,Linea,TranCancela,DocCancela,LineaCancela,FechaIngreso,
				IdVendedor,Monto, Impuesto,Orden,Producto,MontoReal,PorFactura,PeriodoDesde,PeriodoHasta,
				UsuarioRegistro,FechaRegistro,IdTipoPago)
				VALUES
				(@IdCliente,@Transaccion,@Documento,@Linea,@TranCancela,@DocCancela,@LineaCancela,@FechaIngreso,
				@IdVendedor,@Monto, @Iva,@Orden,@IdProducto,@MontoReal,@PorFactura,CONVERT(DATETIME,@Desde,102),
				CONVERT(DATETIME,@Hasta,102),@Usuario,@Fecharegistro,6)
				-----------------------------------------------------------------------------------------------		
				
				SET @ContFact = @ContFact + 1
									
			END			
					
			DROP TABLE #Factura			
		
			SET @ContCob = @ContCob + 1
		
		END
		/**********************************************************************************************/		

     
        
        --Exec dbo.PaNpcETL_Comisiones_Calculo @Desde, @Hasta
        
        /****PASAMOS LOS CHEQUES QUE AUN NO HAN SIDO REVERSADOS QUE VENGAN EN ESTE MISMO LOTE**********/
		INSERT INTO PCComisionesTemporal (IdCliente, Transaccion,
		Documento, Linea, TranCancela, DocCancela, LineaCancela,
		FechaIngreso, IdVendedor, Monto, PorFactura, Impuesto,
		Orden, Producto, MontoReal, PeriodoDesde, PeriodoHasta,
		FechaRegistro, UsuarioRegistro, IdSolicitud, BsComision,
		BsComisionGrte, FichaGteProducto,BsComisionGteProducto,		
		FlagParcial, RecNumPadre,IdTipoPago)
		SELECT
		PCComisionesTemporal.IdCliente, WSMY683.Transaccion, WSMY683.Documento,
		WSMY683.Linea, PCComisionesTemporal.Transaccion AS TranCancela, PCComisionesTemporal.Documento AS DocCancela,
		PCComisionesTemporal.Linea AS LineaCancela, PCComisionesTemporal.FechaIngreso, PCComisionesTemporal.IdVendedor,
		PCComisionesTemporal.Monto*-1, PCComisionesTemporal.PorFactura, PCComisionesTemporal.Impuesto,
		PCComisionesTemporal.Orden, PCComisionesTemporal.Producto, PCComisionesTemporal.MontoReal*-1,
		CONVERT(DATETIME,@Desde,102),CONVERT(DATETIME,@Hasta,102),getdate(),
		PCComisionesTemporal.UsuarioRegistro, CASE WHEN PCComisionesTemporal.IdSolicitud = 0 THEN 9999999999 ELSE PCComisionesTemporal.IdSolicitud END,
		PCComisionesTemporal.BsComision * - 1 AS BsComision, BsComisionGrte*-1 as BsComisionGrte, FichaGteProducto,
		BsComisionGteProducto*-1 as BsComisionGteProducto, PCComisionesTemporal.FlagParcial, PCComisionesTemporal.RecNumPadre,2
		FROM WSMY683 INNER JOIN PCComisionesTemporal ON WSMY683.NroRC = PCComisionesTemporal.Documento AND WSMY683.Linea = PCComisionesTemporal.Linea
		WHERE (PCComisionesTemporal.Transaccion IN (N'RC', N'AN'))
		AND (WSMY683.FlagReversado = 0)
		/**********************************************************************************************/
        


	
		UPDATE PCComisionesTemporal SET idgerente=(SELECT supervisor FROM maestros.dbo.CSMY005 WHERE codigo=idvendedor)

        Select 'Proceso ejecutado de manera exitosa' as Mensaje, 1 as Error
        

  

END
GO
