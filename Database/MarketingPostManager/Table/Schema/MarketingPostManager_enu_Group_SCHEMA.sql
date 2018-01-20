USE MarketingPostManager
GO

IF OBJECT_ID('dbo.MarketingPostManager_enu_Group', 'U') IS NOT NULL  
   DROP TABLE MarketingPostManager_enu_Group
GO  

CREATE TABLE dbo.MarketingPostManager_enu_Group
(  
	[GroupId] INT IDENTITY(1,1) NOT NULL,
	[GroupName] varchar(50) NOT NULL,
	[CreatedOn] datetime NOT NULL CONSTRAINT [DF__MarketingPostManager_enu_Group__CreatedOn] DEFAULT (GETUTCDATE()),
	[CreatedBy] varchar(50) NOT NULL,
	CONSTRAINT [PK__MarketingPostManager_enu_Group__GroupId] PRIMARY KEY CLUSTERED 
		([GroupId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
)
GO

