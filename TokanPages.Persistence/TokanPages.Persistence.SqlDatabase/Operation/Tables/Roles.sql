SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](60) NULL,
	[Description] [nvarchar](60) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
