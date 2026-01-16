IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N '[soccer].[Qualities]') AND type in (N 'U')) 
DROP TABLE [soccer].[Qualities] 
GO 
  
CREATE TABLE [soccer].[Qualities] (
    [Id] [uniqueidentifier] NOT NULL, 
    [Rate] [nvarchar](255) NOT NULL, 
    [LowerBound] [int] NOT NULL, 
    [UpperBound] [int] NOT NULL, 
    [ColourHex] [nvarchar](6) NOT NULL, 
    CONSTRAINT [PK_Qualities] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
  ) ON [PRIMARY]
