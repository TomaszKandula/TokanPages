IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[soccer].[FieldImages]') AND type in (N'U'))
DROP TABLE [soccer].[FieldImages]
GO

CREATE TABLE [soccer].[FieldImages] (
    [Id] [uniqueidentifier] NOT NULL,
    [FieldId] [uniqueidentifier] NOT NULL,
    [ImageBlobName] NVARCHAR(255) NOT NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [PK_FieldImages] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
      PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
      IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
      ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [soccer].[FieldImages] WITH CHECK ADD CONSTRAINT [FK_FieldImages_Fields] FOREIGN KEY([FieldId])
REFERENCES [soccer].[Fields] ([Id])
