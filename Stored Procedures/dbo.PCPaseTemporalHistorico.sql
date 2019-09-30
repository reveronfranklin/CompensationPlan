SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[PCPaseTemporalHistorico]
	-- Add the parameters for the stored procedure here
	@pase NVARCHAR(10)
AS
BEGIN
INSERT INTO PCHistorico(id,IdCliente, Transaccion, Documento, Linea, IdVendedor, Orden, Producto, MontoReal, BsComision, PorcFlat, ComisionRangoCumplimientoCuotaGeneral, PorcRangoCumplimientoCuotaGeneral, 
                         PorcCantidadCuotasCumplidas, ComisionCantidadCuotasCumplidas, CantidadCuotasCumplidas, TotalVentasMes, TotalCuotaMes, PeriodoDesde, PeriodoHasta, IdTipoPago, FechaRegistro, DescripcionTipoPago, MontoString, 
                         OrdenString, DocumentoString, MontoRealString, IdPeriodo, ConceptoNomina)

SELECT        NEWID(), IdCliente, Transaccion, Documento, Linea, IdVendedor, Orden, Producto, MontoReal, BsComision, PorcFlat, ComisionRangoCumplimientoCuotaGeneral, PorcRangoCumplimientoCuotaGeneral, 
                         PorcCantidadCuotasCumplidas, ComisionCantidadCuotasCumplidas, CantidadCuotasCumplidas, TotalVentasMes, TotalCuotaMes, PeriodoDesde, PeriodoHasta, IdTipoPago, FechaRegistro, DescripcionTipoPago, MontoString, 
                         OrdenString, DocumentoString, MontoRealString, IdPeriodo,CASE WHEN BsComision>0 THEN (SELECT ConceptoNominaPago FROM dbo.PCTipoPago WHERE id=IdTipoPago) ELSE  (SELECT ConceptoNominaDescuento FROM dbo.PCTipoPago WHERE id=IdTipoPago) END
FROM            PCTemporal 

END
GO
