ALTER TABLE [dbo].[Resources] DROP CONSTRAINT [FK_Resources_Categories]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 6/5/2022 12:55:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resources]') AND type in (N'U'))
DROP TABLE [dbo].[Resources]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/5/2022 12:55:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
DROP TABLE [dbo].[Categories]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/5/2022 12:55:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](25) NOT NULL,
	[CategoryDescription] [varchar](50) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 6/5/2022 12:55:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[ResourceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[Description] [varchar](50) NULL,
	[Url] [varchar](75) NOT NULL,
	[LinkText] [varchar](25) NOT NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (1, N'C#', N'All things C#')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2, N'HTML', N'Information about HTML')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3, N'CSS', N'Information about CSS')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (4, N'JavaScript', N'Information about JS')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (5, N'ReactJS', N'Information about ReactJS')
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (6, N'ASP.NET MVC', N'Information about ASP.NET MVC')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Resources] ON 

INSERT [dbo].[Resources] ([ResourceId], [Name], [Description], [Url], [LinkText], [CategoryId]) VALUES (1013, N'React Hooks', N'React Hooks Documentation', N'https://reactjs.org/docs/hooks-intro.html', N'React Hooks2', 5)
INSERT [dbo].[Resources] ([ResourceId], [Name], [Description], [Url], [LinkText], [CategoryId]) VALUES (1014, N'React-Bootstrap', N'npm install react-bootstrap', N'https://react-bootstrap.github.io', N'React-Bootstrap', 5)
INSERT [dbo].[Resources] ([ResourceId], [Name], [Description], [Url], [LinkText], [CategoryId]) VALUES (1016, N'React-Router-Dom', N'npm install react-router-dom', N'https://reactrouter.com/web/guides/quick-start', N'React-Router-Dom', 5)
SET IDENTITY_INSERT [dbo].[Resources] OFF
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Categories]
GO
