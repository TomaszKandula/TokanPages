IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Lineups]') AND type in (N'U'))
DROP TABLE [soccer].[Lineups]
GO

CREATE TABLE [soccer].[Lineups] (
    [Id] [uniqueidentifier] NOT NULL,
    [MatchId] [uniqueidentifier] NOT NULL,
    [PlayerHostId] [uniqueidentifier] NOT NULL,
    [PlayerGuestId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_Lineups] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Lineups] WITH CHECK ADD CONSTRAINT [FK_Lineups_Matches] FOREIGN KEY([MatchId])
REFERENCES [soccer].[Matches] ([Id])

ALTER TABLE [soccer].[Lineups] WITH CHECK ADD CONSTRAINT [FK_Lineups_Host_Players] FOREIGN KEY([PlayerHostId])
REFERENCES [soccer].[Players] ([Id])

ALTER TABLE [soccer].[Lineups] WITH CHECK ADD CONSTRAINT [FK_Lineups_Guest_Players] FOREIGN KEY([PlayerGuestId])
REFERENCES [soccer].[Players] ([Id])
