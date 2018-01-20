USE [MarketingPostManager]
GO

DELETE FROM [MarketingPostManager_enu_Group]

SET IDENTITY_INSERT [MarketingPostManager_enu_Group] ON

INSERT INTO [dbo].[MarketingPostManager_enu_Group] ([GroupId],[GroupName],[CreatedOn],[CreatedBy]) VALUES (0, 'Admin', GETUTCDATE(), 'fortunatoj')
INSERT INTO [dbo].[MarketingPostManager_enu_Group] ([GroupId],[GroupName],[CreatedOn],[CreatedBy]) VALUES (1, 'Industrial', GETUTCDATE(), 'fortunatoj')
INSERT INTO [dbo].[MarketingPostManager_enu_Group] ([GroupId],[GroupName],[CreatedOn],[CreatedBy]) VALUES (2, 'Healthcare', GETUTCDATE(), 'fortunatoj')
INSERT INTO [dbo].[MarketingPostManager_enu_Group] ([GroupId],[GroupName],[CreatedOn],[CreatedBy]) VALUES (3, 'Office', GETUTCDATE(), 'fortunatoj')
INSERT INTO [dbo].[MarketingPostManager_enu_Group] ([GroupId],[GroupName],[CreatedOn],[CreatedBy]) VALUES (4, 'Search', GETUTCDATE(), 'fortunatoj')

SET IDENTITY_INSERT [MarketingPostManager_enu_Group] OFF

SELECT * FROM MarketingPostManager.dbo.MarketingPostManager_enu_Group