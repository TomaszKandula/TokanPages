IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Views]') AND type in (N'U'))
DROP TABLE [soccer].[Views]
GO

CREATE TABLE [soccer].[Views] (
    [Id] [uniqueidentifier] NOT NULL,
    [PlayerId] [uniqueidentifier] NOT NULL,
    [Count] INT NOT NULL,
    CONSTRAINT [PK_Views] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Views] WITH CHECK ADD CONSTRAINT [FK_Views_Players] FOREIGN KEY([PlayerId])
REFERENCES [soccer].[Players] ([Id])
