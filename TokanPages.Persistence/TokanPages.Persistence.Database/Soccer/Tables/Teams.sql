IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Teams]') AND type in (N'U'))
DROP TABLE [soccer].[Teams]
GO

CREATE TABLE [soccer].[Teams] (
    [Id] [uniqueidentifier] NOT NULL,
    [PlayerId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Teams] WITH CHECK ADD CONSTRAINT [FK_Teams_Players] FOREIGN KEY([PlayerId])
REFERENCES [soccer].[Players] ([Id])
