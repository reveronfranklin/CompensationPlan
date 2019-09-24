SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[PCSpProducto] 
	-- Add the parameters for the stored procedure here
	 @producto as NVARCHAR(12)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 declare @Subcategoria as numeric 
     SET @subcategoria=(select idsubcategoria from maestros.dbo.CSMY036 where codigo_product=@Producto)
  

	IF NOT EXISTS(SELECT * FROM PCProducto WHERE Producto=@Producto) begin
		INSERT INTO PCProducto(Producto,IdSubcategoria)
		SELECT @producto,@subcategoria
	end

END
GO
