USE [Joost]
GO

ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [DF_Contact_BirthYear]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 2018-01-26 17:08:53 ******/
DROP TABLE [dbo].[Contact]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 2018-01-26 17:08:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[City] [varchar](30) NULL,
	[Mail] [varchar](50) NULL,
	[Phone] [varchar](30) NULL,
	[BirthDate] [date] NULL,
	[BirthYear] [bit] NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_BirthYear]  DEFAULT ((0)) FOR [BirthYear]
GO


