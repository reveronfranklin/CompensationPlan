SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[PaActualizaReactivacionCliente]
	-- Add the parameters for the stored procedure here
	@Año int,
	@Mes int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
Declare @Id as Numeric(18), @Usuario as  nvarchar(40),@IdEvaluacion as numeric(18),@Regs as Int,@Contador as int, @Query as nvarchar(max),@Mensaje as nvarchar(max)
declare @error int, @message varchar(4000), @xstate int;
declare @Orden bigint,@Fecha_Ingreso datetime,@FechaUltimaCompra datetime,@Cliente INT
--delete  FROM      [172.28.107.19].rrd.dbo.EjeucionProceso where query like '%FSVEMCYN03.Mooreve.dbo.PaOfdActualizaDireccion%'
Declare @Valor as numeric(18,2), @DiasNuevo smallint, @DiasReactivado smallint, @Factor numeric(18,3),@UmbralPignorado NUMERIC(18,2),@UsdOrden NUMERIC(18,2),@Cotizacion AS NVARCHAR(20)

Select @DiasNuevo = DiasClienteNuevo, @DiasReactivado = DiasClienteReactivado, @Factor = FactorClienteNuevo From dbo.WSMY678	
SELECT @UmbralPignorado=@UmbralPignorado FROM dbo.PCSysfile

Declare @Evaluaciones Table (
    ID INT Identity(1,1),
	orden  bigint,
	Fecha_Ingreso datetime,
	Cliente int
  )
  INSERT INTO @Evaluaciones
	(
	orden,
	Fecha_Ingreso,
	Cliente
	)
	


	
	SELECT     orden,FECHA_INGRESO,Cliente  FROM      estadisticas.dbo.ventas where ALO_CONTABLE=@Año and MES_CONTABLE=@Mes
   

	SET @Regs = (select count(*) from @Evaluaciones)
    set @Contador=1
 -- Hacemos el Loop
	WHILE @Contador <= @Regs
	BEGIN
		SELECT 
		@Orden=t.orden,
		@Fecha_Ingreso=t.Fecha_Ingreso,
		@Cliente=t.Cliente
		FROM @Evaluaciones t
		WHERE t.ID = @Contador
	
		if exists(select top 1 FECHA_FACTURA from facturacion.dbo.ciny056 where Cliente=@Cliente and FECHA_FACTURA < cast(@Año as nvarchar(4))+'-'+cast(@Mes as nvarchar(2)) +'-01 00:00:00.000' order by FECHA_FACTURA desc) begin
		    select top 1 @FechaUltimaCompra=FECHA_FACTURA  from facturacion.dbo.ciny056 where Cliente=@Cliente and FECHA_FACTURA < cast(@Año as nvarchar(4))+'-'+cast(@Mes as nvarchar(2)) +'-01 00:00:00.000' order by FECHA_FACTURA desc
			IF DATEDIFF(day,@FechaUltimaCompra,@Fecha_Ingreso) >= @DiasReactivado BEGIN
	

					update clientes.dbo.csmy003 set FechaReactivado=@Fecha_Ingreso,F_Ultm_Compra=@FechaUltimaCompra where codigo=@Cliente
					
		
			END	
		end 
		

		SELECT @UsdOrden=Venta_Dol_Ref,@Cotizacion=COTIZACION FROM estadisticas.dbo.VENTAS WHERE orden=@Orden AND CORRELATIVO=0 

		IF @UsdOrden>=@UmbralPignorado BEGIN
			IF NOT EXISTS(SELECT * FROM PCOrdenesPignoradas WHERE orden=@Orden) begin
				INSERT INTO PCOrdenesPignoradas(orden,cotizacion)
				SELECT @Orden,@Cotizacion
			end
		END 

	

		SET @Contador = @Contador + 1
	END

END
GO
