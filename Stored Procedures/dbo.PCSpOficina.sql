SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCSpOficina]
	 @parametro nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	INSERT INTO PCOficina(CodOficina, NombreOficina)
	SELECT cod_oficina, NOM_OFICINA FROM maestros.dbo.wsmy123
	WHERE COD_OFICINA NOT IN (SELECT CodOficina FROM PCOficina)
END
GO
