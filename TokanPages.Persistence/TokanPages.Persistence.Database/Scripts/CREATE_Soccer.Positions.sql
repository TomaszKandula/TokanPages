IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N '[soccer].[Positions]') AND type in (N 'U'))
DROP TABLE [soccer].[Positions] 
GO 

CREATE TABLE [soccer].[Positions] (
    [Id] [uniqueidentifier] NOT NULL, 
    [Name] [nvarchar](255) NOT NULL, 
    [AltName] [nvarchar](255) NULL, 
    [TraditionalNumber] [int] NOT NULL, 
    CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
  ) ON [PRIMARY]
