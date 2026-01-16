IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N '[soccer].[Attributes]') AND type in (N 'U')) 
DROP TABLE [soccer].[Attributes] 
GO 

CREATE TABLE [soccer].[Attributes] (
    [Id] [uniqueidentifier] NOT NULL, 
    [Name] [nvarchar](255) NOT NULL, 
    [Coefficient] [float] NOT NULL, 
    CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
  ) ON [PRIMARY]
