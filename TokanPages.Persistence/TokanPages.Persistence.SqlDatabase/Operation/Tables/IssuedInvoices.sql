SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[IssuedInvoices](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[InvoiceNumber] [nvarchar](255) NOT NULL,
	[InvoiceData] [varbinary](max) NOT NULL,
	[ContentType] [nvarchar](100) NOT NULL,
	[GeneratedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IssuedInvoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
