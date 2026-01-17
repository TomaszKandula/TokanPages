SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[UserCompanies](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[VatNumber] [nvarchar](25) NULL,
	[EmailAddress] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[StreetAddress] [nvarchar](255) NOT NULL,
	[PostalCode] [nvarchar](12) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[CurrencyCode] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
 CONSTRAINT [PK_UserCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
