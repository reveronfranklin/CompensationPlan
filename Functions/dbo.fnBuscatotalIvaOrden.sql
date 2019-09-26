SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		Franklyn Reveron
-- Create date: 20/10/2010
-- Description: Busca total Iva en  Orden
-- =============================================
Create FUNCTION [dbo].[fnBuscatotalIvaOrden] 
(
-- Add the parameters for the function here
@Orden as Numeric(10)
)
RETURNS Numeric(18,2)
AS
BEGIN

  Declare @Cliente as Numeric,@Producto as nvarchar(12),@Rif as nvarchar(12),@TipoIva as Numeric,@Porcentaje as numeric(18,2),@valor_venta as numeric(18,2),@TotalIva as numeric(18,2)
  set @TotalIva=0
  DECLARE pendiente_cursor CURSOR FOR
  select Cliente,Producto,isnull((select rif_facturar from planta.dbo.cpry012 where orden=@Orden ),(select rif_fact from pedidos.dbo.ctmy045 where orden=@Orden )) as RifFacturar,sum(valor_venta) from estadisticas.dbo.ventas where orden = @Orden  group by Cliente,Producto
  OPEN pendiente_cursor

  
    -- Perform the first fetch.
  --FETCH NEXT FROM authors_cursor
  FETCH NEXT FROM pendiente_cursor INTO @Cliente ,@Producto ,@Rif,@valor_venta
  -- Check @@FETCH_STATUS to see if there are any more rows to fetch.
  WHILE @@FETCH_STATUS = 0
  BEGIN
     
     set @TipoIva=isnull((select tipo_iva  from dbo.wary255  where cliente =@Cliente and producto = @Producto and eliminado <> 'X'),0)
     if @TipoIva=0 begin
        set @Porcentaje=(select porcentaje from facturacion.dbo.ciny061  where impuesto =(select t_impuesto  from clientes.dbo.csmy0031  where rif = @Rif))
     end 
     if @TipoIva <> 0 begin
        set @Porcentaje=(select porcentaje from facturacion.dbo.ciny061  where impuesto =@TipoIva)
     end 
     set @TotalIva =@TotalIva + @valor_venta*(@Porcentaje/100)

     --select @Cliente ,@Producto ,@Rif,@Tipoiva,@Porcentaje,@valor_venta,@valor_venta*(@Porcentaje/100),@valor_venta + (@valor_venta*(@Porcentaje/100)),@TotalIva

     FETCH NEXT FROM pendiente_cursor INTO @Cliente ,@Producto ,@Rif,@valor_venta
  END
  CLOSE pendiente_cursor
  DEALLOCATE pendiente_cursor

RETURN @TotalIva

END








GO
