SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserAlias] [nvarchar](255) NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[CryptedPassword] [nvarchar](100) NOT NULL,
	[ResetId] [uniqueidentifier] NULL,
	[ResetIdEnds] [datetime2](7) NULL,
	[ActivationId] [uniqueidentifier] NULL,
	[ActivationIdEnds] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[HasBusinessLock] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
