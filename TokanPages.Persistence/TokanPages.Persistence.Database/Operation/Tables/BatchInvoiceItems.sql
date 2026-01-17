SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[BatchInvoiceItems](
	[Id] [uniqueidentifier] NOT NULL,
	[BatchInvoiceId] [uniqueidentifier] NOT NULL,
	[ItemText] [nvarchar](255) NOT NULL,
	[ItemQuantity] [int] NOT NULL,
	[ItemQuantityUnit] [nvarchar](10) NOT NULL,
	[ItemAmount] [decimal](18, 2) NOT NULL,
	[ItemDiscountRate] [decimal](18, 2) NULL,
	[ValueAmount] [decimal](18, 2) NOT NULL,
	[VatRate] [decimal](18, 2) NULL,
	[GrossAmount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [int] NOT NULL,
 CONSTRAINT [PK_BatchInvoiceItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
