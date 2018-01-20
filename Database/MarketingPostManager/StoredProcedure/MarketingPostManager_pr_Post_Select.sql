USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Post_Select]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Post_Select]
GO

/*===============================================================================
DESCRIPTION: Selects a Post by its Id, or gets all Posts if no Id is provided

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Post_Select] 1
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Post_Select]
	@PostId INT = null
AS
BEGIN

	SELECT
		P.PostId
		,P.Hyperlink
		,P.[Description]
		,P.ImagePath
	FROM MarketingPostManager_ent_Post P WITH (NOLOCK)
	WHERE (@PostID IS NULL OR PostId = @PostId)

END

GO