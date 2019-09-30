SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpResumenComisionHistorico]
	 @IdPeriodo int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
DELETE from PCResumenComisionHistorico WHERE IdPeriodo=@IdPeriodo
DELETE FROM PCResumenOficinaHistorico WHERE IdPeriodo=@IdPeriodo
DECLARE @Periodo AS NVARCHAR(50)
SELECT TOP 1 @Periodo ='Periodo desde: ' +CAST(day(Desde) AS NVARCHAR(4)) +'/' +CAST(month(Desde) AS NVARCHAR(4)) +'/'+ CAST(YEAR(Desde) AS NVARCHAR(4)) + ' Hasta: ' + CAST(day(Hasta) AS NVARCHAR(4)) +'/' +CAST(month(Hasta) AS NVARCHAR(4)) +'/'+ CAST(YEAR(Hasta) AS NVARCHAR(4)) FROM dbo.wsmy686 WHERE id=@IdPeriodo

INSERT INTO PCResumenComisionHistorico(CodigoVendedor,Monto,idperiodo)
SELECT IdVendedor,SUM(BsComision),@IdPeriodo FROM [dbo].[PCHistorico] 
GROUP BY IdPeriodo, IdVendedor
HAVING        (IdPeriodo = @IdPeriodo)
UPDATE PCResumenComisionHistorico SET Periodo=@Periodo WHERE  IdPeriodo = @IdPeriodo
UPDATE PCResumenComisionHistorico SET CodigoOficina=(SELECT CodOficina FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor) WHERE  IdPeriodo = @IdPeriodo
UPDATE PCResumenComisionHistorico SET NombreOficina=(SELECT nombreOficina FROM  [dbo].[PCOficina] WHERE CodOficina=CodigoOficina) WHERE  IdPeriodo = @IdPeriodo
UPDATE PCResumenComisionHistorico SET NombreVendedor=(SELECT nombre FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor) WHERE  IdPeriodo = @IdPeriodo
UPDATE PCResumenComisionHistorico SET Ficha=(SELECT Ficha FROM dbo.PCVendedor WHERE IdVendedor=CodigoVendedor) WHERE  IdPeriodo = @IdPeriodo
UPDATE PCResumenComisionHistorico SET montostring=	[dbo].[FN_FORMAT_NUMBER](monto,'2',',','.','','') WHERE  IdPeriodo = @IdPeriodo

INSERT INTO PCResumenOficinaHistorico(CodigoOficina,NombreOficina,Monto,IdPeriodo)
SELECT CodigoOficina,NombreOficina,SUM(Monto),@IdPeriodo FROM PCResumenComisionHistorico GROUP BY IdPeriodo,CodigoOficina,NombreOficina
HAVING        (IdPeriodo = @IdPeriodo)
UPDATE PCResumenOficinaHistorico SET montostring=	[dbo].[FN_FORMAT_NUMBER](monto,'2',',','.','','') WHERE (IdPeriodo = @IdPeriodo)

END



GO
