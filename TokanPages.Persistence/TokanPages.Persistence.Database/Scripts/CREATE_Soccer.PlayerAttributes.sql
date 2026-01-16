IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[PlayerAttributes]') AND type in (N'U'))
DROP TABLE [soccer].[PlayerAttributes]
GO

CREATE TABLE [soccer].[PlayerAttributes] (
    [Id] [uniqueidentifier] NOT NULL,
    [PlayerId] [uniqueidentifier] NOT NULL,
    [AttributeId] [uniqueidentifier] NOT NULL,
    [Value] INT NOT NULL,
    CONSTRAINT [PK_PlayerAttributes] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[PlayerAttributes] WITH CHECK ADD CONSTRAINT [FK_PlayerAttributes_Players] FOREIGN KEY([PlayerId])
REFERENCES [soccer].[Players] ([Id])

ALTER TABLE [soccer].[PlayerAttributes] WITH CHECK ADD CONSTRAINT [FK_PlayerAttributes_Attributes] FOREIGN KEY([AttributeId])
REFERENCES [soccer].[Attributes] ([Id])
