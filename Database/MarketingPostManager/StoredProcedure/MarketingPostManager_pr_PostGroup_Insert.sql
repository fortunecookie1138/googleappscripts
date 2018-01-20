USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_PostGroup_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].MarketingPostManager_pr_PostGroup_Insert
GO

/*===============================================================================
DESCRIPTION: Inserts a new Post Group xref

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] 1, 1
===============================================================================*/
CREATE PROCEDURE [dbo].MarketingPostManager_pr_PostGroup_Insert
	@PostId INT,
	@GroupId INT
AS
BEGIN

	INSERT INTO [dbo].[MarketingPostManager_xref_PostGroup]
			([PostId]
			,[GroupId])
	VALUES
			(@PostId
			,@GroupId)

END

GO