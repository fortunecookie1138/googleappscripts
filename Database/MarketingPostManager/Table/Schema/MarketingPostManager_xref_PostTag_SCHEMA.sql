USE MarketingPostManager
GO

IF OBJECT_ID('dbo.MarketingPostManager_xref_PostTag', 'U') IS NOT NULL  
   DROP TABLE [dbo].[MarketingPostManager_xref_PostTag]
GO

CREATE TABLE [dbo].[MarketingPostManager_xref_PostTag]
(  
	[PostTagId] INT IDENTITY(1,1) NOT NULL,
	[PostId] INT NOT NULL,
	[TagId] INT NOT NULL,
	CONSTRAINT [PK__MarketingPostManager_xref_PostTag__PostTagId] PRIMARY KEY CLUSTERED 
		([PostTagId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
)
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostTag] WITH CHECK ADD CONSTRAINT [FK_MarketingPostManager_xref_PostTag__PostId__MarketingPostManager_ent_Post] FOREIGN KEY ([PostId])
REFERENCES [dbo].[MarketingPostManager_ent_Post] ([PostId])
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostTag] CHECK CONSTRAINT [FK_MarketingPostManager_xref_PostTag__PostId__MarketingPostManager_ent_Post]
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostTag] WITH CHECK ADD CONSTRAINT [FK_MarketingPostManager_xref_PostTag__TagId__MarketingPostManager_ent_Tag] FOREIGN KEY ([TagId])
REFERENCES [dbo].[MarketingPostManager_ent_Tag] ([TagId])
GO

ALTER TABLE [dbo].[MarketingPostManager_xref_PostTag] CHECK CONSTRAINT [FK_MarketingPostManager_xref_PostTag__TagId__MarketingPostManager_ent_Tag]
GO