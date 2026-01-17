IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Feeds]') AND type in (N'U'))
DROP TABLE [soccer].[Feeds]
GO

CREATE TABLE [soccer].[Feeds] (
    [Id] [uniqueidentifier] NOT NULL,
    [PlayerId] [uniqueidentifier] NOT NULL,
    [Text] NVARCHAR(255) NOT NULL,
    [Published] DATETIME2 NOT NULL,
    [IsVisible] BIT NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Feeds] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Feeds] WITH CHECK ADD CONSTRAINT [FK_Feeds_Players] FOREIGN KEY([PlayerId])
REFERENCES [soccer].[Players] ([Id])
