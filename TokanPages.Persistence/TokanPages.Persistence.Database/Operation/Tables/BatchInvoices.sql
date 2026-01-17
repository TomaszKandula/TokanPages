SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[BatchInvoices](
	[Id] [uniqueidentifier] NOT NULL,
	[InvoiceNumber] [nvarchar](255) NOT NULL,
	[VoucherDate] [datetime2](7) NOT NULL,
	[ValueDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[PaymentTerms] [int] NOT NULL,
	[PaymentType] [int] NOT NULL,
	[PaymentStatus] [int] NOT NULL,
	[CustomerName] [nvarchar](255) NOT NULL,
	[CustomerVatNumber] [nvarchar](25) NULL,
	[CountryCode] [int] NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[StreetAddress] [nvarchar](100) NOT NULL,
	[PostalCode] [nvarchar](25) NOT NULL,
	[PostalArea] [nvarchar](150) NOT NULL,
	[ProcessBatchKey] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[InvoiceTemplateName] [nvarchar](255) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserCompanyId] [uniqueidentifier] NOT NULL,
	[UserBankAccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BatchInvoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
