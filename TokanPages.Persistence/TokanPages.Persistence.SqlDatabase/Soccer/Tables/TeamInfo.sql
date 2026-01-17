IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[TeamInfo]') AND type in (N'U'))
DROP TABLE [soccer].[TeamInfo]
GO

CREATE TABLE [soccer].[TeamInfo] (
    [Id] [uniqueidentifier] NOT NULL,
    [TeamId] [uniqueidentifier] NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(500) NULL,
    [Avatar] NVARCHAR(255) NULL,
    [ImageBlobName] NVARCHAR(255) NULL,
    CONSTRAINT [PK_TeamInfo] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[TeamInfo] WITH CHECK ADD CONSTRAINT [FK_TeamInfo_Teams] FOREIGN KEY([TeamId])
REFERENCES [soccer].[Teams] ([Id])
