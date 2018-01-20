USE MarketingPostManager
GO

IF OBJECT_ID('dbo.MarketingPostManager_ent_Post', 'U') IS NOT NULL  
   DROP TABLE MarketingPostManager_ent_Post 
GO  

CREATE TABLE dbo.MarketingPostManager_ent_Post  
(  
	[PostId] INT IDENTITY(1,1) NOT NULL,
	[Hyperlink] varchar(300) NOT NULL,
	[Description] varchar(500) NOT NULL,
	[ImagePath] varchar(500) NOT NULL,
	[CreatedOn] datetime NOT NULL CONSTRAINT [DF__MarketingPostManager_ent_Post__CreatedOn]  DEFAULT (GETUTCDATE()),
	[UpdatedOn] datetime NOT NULL CONSTRAINT [DF__MarketingPostManager_ent_Post__UpdatedOn]  DEFAULT (GETUTCDATE()),
	[CreatedBy] varchar(50) NOT NULL,
	[UpdatedBy] varchar(50) NOT NULL,
	CONSTRAINT [PK__MarketingPostManager_ent_Post__PostId] PRIMARY KEY CLUSTERED 
		([PostId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
) ON [PRIMARY]
GO
