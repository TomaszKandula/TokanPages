IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[FeedImages]') AND type in (N'U'))
DROP TABLE [soccer].[FeedImages]
GO

CREATE TABLE [soccer].[FeedImages] (
    [Id] [uniqueidentifier] NOT NULL,
    [FeedId] [uniqueidentifier] NOT NULL,
    [ImageBlobName] NVARCHAR(255) NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_FeedImages] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[FeedImages] WITH CHECK ADD CONSTRAINT [FK_FeedImages_Feeds] FOREIGN KEY([FeedId])
REFERENCES [soccer].[Feeds] ([Id])
