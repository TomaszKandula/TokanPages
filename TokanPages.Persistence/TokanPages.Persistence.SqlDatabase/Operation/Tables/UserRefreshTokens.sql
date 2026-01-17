SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[UserRefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](500) NOT NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[CreatedByIp] [nvarchar](15) NOT NULL,
	[Revoked] [datetime2](7) NULL,
	[RevokedByIp] [nvarchar](15) NULL,
	[ReplacedByToken] [nvarchar](500) NULL,
	[ReasonRevoked] [nvarchar](255) NULL,
 CONSTRAINT [PK_UserRefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
