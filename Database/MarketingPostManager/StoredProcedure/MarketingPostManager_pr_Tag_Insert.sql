USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Tag_Insert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Tag_Insert]
GO

/*===============================================================================
DESCRIPTION: Inserts a new Tag

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] 'Workplace', 'fortunatoj'
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Tag_Insert]
	@TagName [varchar](50),
	@CreatedBy [varchar](50)
AS
BEGIN
	
	INSERT INTO [dbo].[MarketingPostManager_ent_Tag]
           ([TagName]
           ,[CreatedBy])
     VALUES
           (@TagName
           ,@CreatedBy)

END

GO