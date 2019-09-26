SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PCLimpiaTemporal]
	-- Add the parameters for the stored procedure here
	@limpia NVARCHAR(10)
AS
BEGIN
	TRUNCATE TABLE dbo.PCTemporal
	TRUNCATE TABLE dbo.PCComisionesTemporal
	TRUNCATE TABLE PCResumenComisionTemporal
	TRUNCATE TABLE PCResumenOficinaTemporal
END
GO
