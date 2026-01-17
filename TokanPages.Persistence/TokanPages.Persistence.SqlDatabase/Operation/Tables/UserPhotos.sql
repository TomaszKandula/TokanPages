SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[UserPhotos](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PhotoGearId] [uniqueidentifier] NOT NULL,
	[PhotoCategoryId] [uniqueidentifier] NOT NULL,
	[Keywords] [nvarchar](500) NULL,
	[PhotoUrl] [nvarchar](255) NOT NULL,
	[DateTaken] [datetime2](7) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserPhotos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
