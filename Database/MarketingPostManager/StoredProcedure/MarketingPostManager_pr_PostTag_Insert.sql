USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_PostTag_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_PostTag_Insert]
GO

/*===============================================================================
DESCRIPTION: Inserts a new Post Tag xref, along with the tag record itself if it doesn't already exist

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_PostTag_Insert] 1, 'Communication', 'fortunatoj'
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_PostTag_Insert]
	@PostId INT,
	@TagName VARCHAR(50),
	@CreatedBy VARCHAR(50)
AS
BEGIN
	DECLARE @TagId INT
	SELECT @TagId = TagId FROM [dbo].[MarketingPostManager_ent_Tag] WITH (NOLOCK) WHERE TagName = @TagName

	IF (@TagId IS NULL)
	BEGIN	
		EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] @TagName, @CreatedBy
	END

	SELECT @TagId = TagId FROM [dbo].[MarketingPostManager_ent_Tag] WITH (NOLOCK) WHERE TagName = @TagName

	INSERT INTO [dbo].[MarketingPostManager_xref_PostTag]
           ([PostId]
           ,[TagId])
     VALUES
           (@PostId
           ,@TagId)

END

GO