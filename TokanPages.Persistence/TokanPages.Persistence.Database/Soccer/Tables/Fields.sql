IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[Fields]') AND type in (N'U'))
DROP TABLE [soccer].[Fields]
GO

CREATE TABLE [soccer].[Fields] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(500) NOT NULL,
    [GpsLatitude] FLOAT NOT NULL,
    [GpsLongitude] FLOAT NOT NULL,
    [Published] DATETIME2 NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_Fields] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]
