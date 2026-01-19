SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[PushNotificationLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[RegistrationId] [nvarchar](255) NOT NULL,
	[Handle] [nvarchar](255) NOT NULL,
	[Platform] [nvarchar](6) NOT NULL,
	[Payload] [nvarchar](2048) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_PushNotificationLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
