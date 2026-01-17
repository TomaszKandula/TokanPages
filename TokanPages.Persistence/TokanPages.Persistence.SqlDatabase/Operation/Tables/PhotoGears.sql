SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [operation].[PhotoGears](
	[Id] [uniqueidentifier] NOT NULL,
	[BodyVendor] [nvarchar](100) NULL,
	[BodyModel] [nvarchar](100) NULL,
	[LensVendor] [nvarchar](100) NULL,
	[LensName] [nvarchar](60) NULL,
	[FocalLength] [int] NOT NULL,
	[ShutterSpeed] [nvarchar](10) NULL,
	[Aperture] [decimal](18, 2) NOT NULL,
	[FilmIso] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PhotoGears] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
