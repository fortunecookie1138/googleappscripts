USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Post_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Post_Insert]
GO

/*===============================================================================
DESCRIPTION: Inserts a new Post

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Post_Insert] 'coolblog.com\rad', 'This blog is rad and you should read it', 'http:\\idkyet.com', 'fortunatoj'
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Post_Insert]
	@Hyperlink [varchar](300),
	@Description [varchar](500),
	@ImagePath [varchar](500),
	@CreatedBy [varchar](50)
AS
BEGIN

-- TODO have this proc also insert the tags for a post so i don't have to call a separate proc to do that

	INSERT INTO [dbo].[MarketingPostManager_ent_Post]
           ([Hyperlink]
           ,[Description]
           ,[ImagePath]
           ,[CreatedBy]
           ,[UpdatedBy])
     VALUES
           (@Hyperlink
           ,@Description
           ,@ImagePath
           ,@CreatedBy
           ,@CreatedBy)

	SELECT CAST(SCOPE_IDENTITY() AS INT)

END

GO