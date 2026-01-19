SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[UploadedVideos](
	[Id] [uniqueidentifier] NOT NULL,
	[TicketId] [uniqueidentifier] NOT NULL,
	[SourceBlobUri] [nvarchar](max) NOT NULL,
	[TargetVideoUri] [nvarchar](max) NOT NULL,
	[TargetThumbnailUri] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[IsSourceDeleted] [bit] NOT NULL,
	[ProcessingWarning] [nvarchar](4000) NULL,
	[InputSizeInBytes] [bigint] NOT NULL,
	[OutputSizeInBytes] [bigint] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_UploadedVideos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
