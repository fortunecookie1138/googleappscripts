USE MarketingPostManager
GO

-- temp script for inserting test data
DELETE FROM MarketingPostManager_xref_PostGroup
DELETE FROM MarketingPostManager_xref_PostTag
DELETE FROM MarketingPostManager_ent_Tag
DELETE FROM MarketingPostManager_ent_Post

DECLARE @PostId1 INT, @PostId2 INT, @PostId3 INT, @LeadershipTagId INT, @CommunicationTagId INT

EXEC [dbo].[MarketingPostManager_pr_Post_Insert] 'http://www.alliancesolutionsgrp.com/blog/2016/08/25/5-steps-take-control-work/',
	'5 Steps to Take Control of Your Work', 'tbdImageURL', 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_Post_Insert] 'http://www.alliancesolutionsgrp.com/blog/2016/09/14/leverage-ask-listen-get-yes/', 
	'Leverage, Ask, Listen: How to Get a Yes', 'tbdImageURL', 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_Post_Insert] 'http://www.alliancesolutionsgrp.com/blog/2016/09/07/how-to-ask-for-what-you-want-in-the-workplace/', 
	'How to Ask for What You Want in the Workplace', 'tbdImageURL', 'fortunatoj'
SELECT @PostId1 = PostId FROM MarketingPostManager_ent_Post WHERE Description = '5 Steps to Take Control of Your Work'
SELECT @PostId2 = PostId FROM MarketingPostManager_ent_Post WHERE Description = 'Leverage, Ask, Listen: How to Get a Yes'
SELECT @PostId3 = PostId FROM MarketingPostManager_ent_Post WHERE Description = 'How to Ask for What You Want in the Workplace'

EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] 'Leadership', 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] 'Communication', 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] 'Engagement', 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_Tag_Insert] 'Hiring', 'fortunatoj'
SELECT @LeadershipTagId = TagId FROM MarketingPostManager_ent_Tag WHERE TagName = 'Leadership'
SELECT @CommunicationTagId = TagId FROM MarketingPostManager_ent_Tag WHERE TagName = 'Communication'

EXEC [dbo].[MarketingPostManager_pr_PostTag_Insert] @PostId1, @LeadershipTagId, 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_PostTag_Insert] @PostId2, @LeadershipTagId, 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_PostTag_Insert] @PostId2, @CommunicationTagId, 'fortunatoj'
EXEC [dbo].[MarketingPostManager_pr_PostTag_Insert] @PostId3, @CommunicationTagId, 'fortunatoj'

EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId1, 1 -- Industrial
EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId2, 1 
EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId2, 2 -- Healthcare
EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId3, 1
EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId3, 2
EXEC [dbo].[MarketingPostManager_pr_PostGroup_Insert] @PostId3, 3 -- Office

SELECT * FROM dbo.MarketingPostManager_ent_Post
SELECT * FROM dbo.MarketingPostManager_ent_Tag
SELECT * FROM dbo.MarketingPostManager_enu_Group
SELECT * FROM dbo.MarketingPostManager_xref_PostGroup
SELECT * FROM dbo.MarketingPostManager_xref_PostTag