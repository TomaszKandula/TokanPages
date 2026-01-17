IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Matches]') AND type in (N'U'))
DROP TABLE [soccer].[Matches]
GO

CREATE TABLE [soccer].[Matches] (
    [Id] [uniqueidentifier] NOT NULL,
    [EventDate] DATETIME2 NOT NULL,
    [TeamHostId] [uniqueidentifier] NOT NULL,
    [TeamGuestId] [uniqueidentifier] NULL,
    [FieldId] [uniqueidentifier] NOT NULL,
    [GoalsHost] INT NOT NULL,
    [GoalsGuest] INT NOT NULL,
    [IsInternalGame] BIT NOT NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[Matches] WITH CHECK ADD CONSTRAINT [FK_Matches_Fields] FOREIGN KEY([FieldId])
REFERENCES [soccer].[Fields] ([Id])

ALTER TABLE [soccer].[Matches] WITH CHECK ADD CONSTRAINT [FK_Matches_Host_Teams] FOREIGN KEY([TeamHostId])
REFERENCES [soccer].[Teams] ([Id])

ALTER TABLE [soccer].[Matches] WITH CHECK ADD CONSTRAINT [FK_Matches_Guest_Teams] FOREIGN KEY([TeamGuestId])
REFERENCES [soccer].[Teams] ([Id])
