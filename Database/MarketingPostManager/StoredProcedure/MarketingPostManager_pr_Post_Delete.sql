USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Post_Delete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Post_Delete]
GO

/*===============================================================================
DESCRIPTION: Deletes a Post and its associated xref records. Any Tags that are orphaned
				as a result of deleting the Post are also deleted.

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Post_Delete] 1
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Post_Delete]
	@PostId INT
AS
BEGIN
	
	DELETE FROM MarketingPostManager_xref_PostTag WHERE PostId = @PostId
	DELETE FROM MarketingPostManager_xref_PostGroup WHERE PostId = @PostId
	DELETE FROM MarketingPostManager_ent_Post WHERE PostId = @PostId

	DELETE T
	FROM MarketingPostManager_ent_Tag T
	LEFT JOIN MarketingPostManager_xref_PostTag PT
	ON T.TagId = PT.TagId
	WHERE T.TagId IS NULL
END

GO