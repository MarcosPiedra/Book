USE [Books]
GO

/****** Object:  Table [dbo].[book]    Script Date: 12/02/2019 8:34:31 ******/
DROP TABLE [dbo].[book]
GO

/****** Object:  Table [dbo].[book]    Script Date: 12/02/2019 8:34:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[book](
	[bo_id] [int] IDENTITY(1,1) NOT NULL,
	[bo_title] [nvarchar](50) NOT NULL,
	[bo_description] [nvarchar](255) NOT NULL,
	[bo_author] [nvarchar](50) NOT NULL,
	[bo_publication_date] [datetime] NOT NULL,
	[bo_is_read] [bit] NOT NULL,
 CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED 
(
	[bo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


