SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
Create FUNCTION [dbo].[FnNpcBuscaPorcComisionClienteNuevo] 
(
@FechaApertura datetime,
@FechaOrden datetime,
@FechaUltimaCompra datetime,
@PorcComision numeric(18,3)
)
RETURNS Numeric(18,2)
AS
BEGIN
	
	Declare @Valor as numeric(18,2), @DiasNuevo smallint, @DiasReactivado smallint, @Factor numeric(18,3)
	
	Select @DiasNuevo = DiasClienteNuevo, @DiasReactivado = DiasClienteReactivado, @Factor = FactorClienteNuevo From dbo.WSMY678	
	
	Set @Valor = 0
	
	--EVALUAMOS LOS DIAS COMO CLIENTE NUEVO
	IF DATEDIFF(day,@FechaApertura,@FechaOrden) <= @DiasNuevo BEGIN
	
		Set @Valor = @Factor * @PorcComision
		
	END
	
	--EVALUAMOS LOS DIAS COMO CLIENTE NUEVO
	IF DATEDIFF(day,@FechaUltimaCompra,@FechaOrden) >= @DiasReactivado BEGIN
	
		Set @Valor = @Factor * @PorcComision
		
	END	
     
	RETURN @Valor

END



GO
