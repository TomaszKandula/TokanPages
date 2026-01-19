IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Players]') AND type in (N'U'))
DROP TABLE [soccer].[Players]
GO

CREATE TABLE [soccer].[Players] (
    [Id] [uniqueidentifier] NOT NULL,
    [UserId] [uniqueidentifier] NOT NULL,
    [PositionId] [uniqueidentifier] NOT NULL,
    [NickName] NVARCHAR(255) NOT NULL,
    [Height] INT NOT NULL,
    [Weight] INT NOT NULL,
    [Birthday] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Players] WITH CHECK ADD CONSTRAINT [FK_Players_Positions] FOREIGN KEY([PositionId])
REFERENCES [soccer].[Positions] ([Id])

ALTER TABLE [soccer].[Players] WITH CHECK ADD CONSTRAINT [FK_Players_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])
