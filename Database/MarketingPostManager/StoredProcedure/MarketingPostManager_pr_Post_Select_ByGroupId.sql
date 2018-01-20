USE MarketingPostManager
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[MarketingPostManager_pr_Post_Select_ByGroupId]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[MarketingPostManager_pr_Post_Select_ByGroupId]
GO

/*===============================================================================
DESCRIPTION: Selects all Posts that are in the given Group

EXECUTION: EXEC [dbo].[MarketingPostManager_pr_Post_Select_ByGroupId] 1
===============================================================================*/
CREATE PROCEDURE [dbo].[MarketingPostManager_pr_Post_Select_ByGroupId]
	@GroupId INT
AS
BEGIN

	SELECT DISTINCT
		P.PostId
		,P.Hyperlink
		,P.[Description]
		,P.ImagePath
	FROM MarketingPostManager_ent_Post P WITH (NOLOCK)
	INNER JOIN MarketingPostManager_xref_PostGroup PG WITH (NOLOCK)
		ON P.PostId = PG.PostId
	INNER JOIN MarketingPostManager_enu_Group G WITH (NOLOCK)
		ON PG.GroupId = G.GroupId
	WHERE @GroupId = 0 OR G.GroupId = @GroupId

END

GO