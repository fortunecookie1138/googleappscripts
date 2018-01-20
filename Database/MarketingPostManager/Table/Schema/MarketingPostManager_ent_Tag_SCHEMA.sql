USE MarketingPostManager
GO

IF OBJECT_ID('dbo.MarketingPostManager_ent_Tag', 'U') IS NOT NULL  
   DROP TABLE [dbo].[MarketingPostManager_ent_Tag]
GO  

CREATE TABLE [dbo].[MarketingPostManager_ent_Tag]
(  
	[TagId] INT IDENTITY(1,1) NOT NULL,
	[TagName] varchar(50) NOT NULL,
	[CreatedOn] datetime NOT NULL CONSTRAINT [DF__MarketingPostManager_ent_Tag__CreatedOn] DEFAULT (GETUTCDATE()),
	[CreatedBy] varchar(50) NOT NULL,
	CONSTRAINT [PK__MarketingPostManager_ent_Tag__TagId] PRIMARY KEY CLUSTERED 
		([TagId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
)
GO

