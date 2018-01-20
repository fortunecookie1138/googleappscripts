USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Post_Update]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Post_Update]
GO

/*===============================================================================
DESCRIPTION: Updates the fields of an existing Post

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Post_Update] 
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Post_Update]
	@PostId INT,
	@Hyperlink [varchar](300),
	@Description [varchar](500),
	@ImagePath [varchar](500),
	@UpdatedBy [varchar](50)
AS
BEGIN

	-- TODO update tags and groups

	UPDATE [dbo].[MarketingPostManager_ent_Post]
	SET Hyperlink = @Hyperlink,
		[Description] = @Description,
		ImagePath = @ImagePath,
		UpdatedOn = GETUTCDATE(),
		UpdatedBy = @UpdatedBy
	WHERE PostId = @PostId

END

GO