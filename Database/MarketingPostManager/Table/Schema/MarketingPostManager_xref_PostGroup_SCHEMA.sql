USE MarketingPostManager
GO

IF OBJECT_ID('dbo.MarketingPostManager_xref_PostGroup', 'U') IS NOT NULL  
   DROP TABLE [dbo].[MarketingPostManager_xref_PostGroup]
GO

CREATE TABLE dbo.MarketingPostManager_xref_PostGroup
(  
	[PostGroupId] INT IDENTITY(1,1) NOT NULL,
	[PostId] INT NOT NULL,
	[GroupId] INT NOT NULL,
	CONSTRAINT [PK__MarketingPostManager_xref_PostGroup__PostGroupId] PRIMARY KEY CLUSTERED 
		([PostGroupId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
)
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostGroup] WITH CHECK ADD CONSTRAINT [FK_MarketingPostManager_xref_PostGroup__PostId__MarketingPostManager_ent_Post] FOREIGN KEY ([PostId])
REFERENCES [dbo].[MarketingPostManager_ent_Post] ([PostId])
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostGroup] CHECK CONSTRAINT [FK_MarketingPostManager_xref_PostGroup__PostId__MarketingPostManager_ent_Post]
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostGroup] WITH CHECK ADD CONSTRAINT [FK_MarketingPostManager_xref_PostGroup__GroupId__MarketingPostManager_enu_Group] FOREIGN KEY ([GroupId])
REFERENCES [dbo].[MarketingPostManager_enu_Group] ([GroupId])
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostGroup] CHECK CONSTRAINT [FK_MarketingPostManager_xref_PostGroup__GroupId__MarketingPostManager_enu_Group]
GO